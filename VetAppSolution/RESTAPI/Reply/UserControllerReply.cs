using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Vetapp.Client.ProxyCore;
using Vetapp.Engine.BusinessFacadeLayer;
using Vetapp.Engine.DataAccessLayer.Data;
using Vetapp.Engine.DataAccessLayer.Enumeration;
using Vetapp.Engine.Common;
using RESTAPI.Utils;

namespace RESTAPI.Reply
{
    public class UserControllerReply : ReplyBase
    {
        public UserControllerReply(AppSettings settings)
        {
            _settings = settings;
        }

        public UserProxy Create(UserProxy bodyUserProxy)
        {
            UserProxy userProxy = null;
            try
            {
                if ( (!string.IsNullOrEmpty(bodyUserProxy.Username)) && (!string.IsNullOrEmpty(bodyUserProxy.Passwd)))
                {
                    BusFacCore busFacCore = new BusFacCore(_settings.DefaultConnection);
                    bool bExist = Exist(bodyUserProxy.Username);
                    if (!bExist)
                    {
                        string encryptedPasswd = UtilsSecurity.encrypt(bodyUserProxy.Passwd);
                        User user = new User() { Username = bodyUserProxy.Username, Passwd = encryptedPasswd, UserRoleID = 1, IsDisabled = false };
                        long UserID = busFacCore.UserCreateOrModify(user);
                        if ((!busFacCore.HasError) && (UserID > 0))
                        {
                            //successfully created user
                            user = busFacCore.UserGet(UserID);
                            userProxy = DataToModelConverter.ConvertToProxy(user);
                        }
                        else
                        {
                            HasError = true;
                            ErrorMessage = "Error in create new user.";
                        }
                    }
                    else
                    {
                        HasError = true;
                        ErrorMessage = "User exists.";
                    }
                }
                else
                {
                    HasError = true;
                    ErrorMessage = "Username in input parameter not specified";
                }
            }
            catch (Exception ex)
            {
                HasError = true;
                ErrorStacktrace = ex.StackTrace;
                ErrorMessage = ex.Message;
            }
            return userProxy;
        }

        public UserProxy Authenticate(UserProxy bodyUserProxy)
        {
            UserProxy userProxy = null;
            try
            {
                if ((!string.IsNullOrEmpty(bodyUserProxy.Username)) && (!string.IsNullOrEmpty(bodyUserProxy.Passwd)))
                {
                    BusFacCore busFacCore = new BusFacCore(_settings.DefaultConnection);
                    string encryptedPassword = UtilsSecurity.encrypt(bodyUserProxy.Passwd);
                    EnumUser enumUser = new EnumUser() { Username = bodyUserProxy.Username, Passwd = encryptedPassword };
                    ArrayList arUser = busFacCore.UserGetList(enumUser);
                    if ((arUser != null) && (arUser.Count == 1))
                    {
                        // user is authenticated
                        User user = (User) arUser[0];
                        userProxy = DataToModelConverter.ConvertToProxy(user);
                    }
                    else
                    {
                        HasError = true;
                        ErrorMessage = "Error in retrieving user accounts";
                    }
                }
                else
                {
                    HasError = true;
                    ErrorMessage = "Username in input parameter not specified";
                }
            }
            catch (Exception ex)
            {
                HasError = true;
                ErrorStacktrace = ex.StackTrace;
                ErrorMessage = ex.Message;
            }
            return userProxy;
        }

        //public UserModel Create(UserModel bodyUserModel)
        //{
        //    UserModel userModel = null;
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(bodyUserModel.AuthUserid))
        //        {
        //            BusFacCore busFacCore = new BusFacCore(_settings.DefaultConnection);
        //            User user = busFacCore.UserEnumByAuthUserid(bodyUserModel.AuthUserid);
        //            if ((user == null) && (!busFacCore.HasError))
        //            {
        //                // create the member
        //                user = new User()
        //                {
        //                    AuthAccessToken = bodyUserModel.AuthAccessToken,
        //                    AuthConnection = bodyUserModel.AuthConnection,
        //                    AuthIdToken = bodyUserModel.AuthIdToken,
        //                    AuthName = bodyUserModel.AuthName,
        //                    AuthNickname = bodyUserModel.AuthNickname,
        //                    AuthProvider = bodyUserModel.AuthProvider,
        //                    AuthUserid = bodyUserModel.AuthUserid,
        //                    CanTextMsg = bodyUserModel.CanTextMsg,
        //                    EmailAddress = bodyUserModel.EmailAddress,
        //                    Firstname = bodyUserModel.Firstname,
        //                    Lastname = bodyUserModel.Lastname,
        //                    Middlename = bodyUserModel.Middlename,
        //                    PhoneNumber = bodyUserModel.PhoneNumber,
        //                    Profileimageurl = bodyUserModel.Profileimageurl
        //                };

        //                long lID = busFacCore.UserCreateOrModify(user);
        //            }
        //            if ((user != null) && (user.UserID > 0))
        //            {
        //                userModel = DataToModelConverter.ConvertToModel(user);
        //            }
        //            else
        //            {
        //                HasError = true;
        //                ErrorMessage = "General error in creating or getting existing user.";
        //            }
        //        }
        //        else
        //        {
        //            HasError = true;
        //            ErrorMessage = "AuthUserid in input parameter not specified";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        HasError = true;
        //        ErrorStacktrace = ex.StackTrace;
        //        ErrorMessage = ex.Message;
        //    }
        //    return userModel;
        //}

        public bool Exist(string username)
        {
            bool bExist = true;
            List<UserProxy> lstUserProxy = Find(username);
            if ((!HasError) && (lstUserProxy.Count == 0))
            {
                bExist = false;
            }
            return bExist;
        }
        public List<UserProxy> Find(string username)
        {
            List<UserProxy> lstUserProxy = new List<UserProxy>();
            try
            {
                if ((!string.IsNullOrEmpty(username)))
                {
                    EnumUser enumUser = new EnumUser() { Username = username };
                    BusFacCore busFacCore = new BusFacCore(_settings.DefaultConnection);
                    ArrayList arUser = busFacCore.UserGetList(enumUser);
                    if ((arUser != null) && (arUser.Count > 0))
                    {
                        UserProxy userProxy = null;
                        foreach(User user in arUser)
                        {
                            userProxy = DataToModelConverter.ConvertToProxy(user);
                            lstUserProxy.Add(userProxy);
                        }
                    }
                }
                else
                {
                    HasError = true;
                    StatusErrorMessage = "Username is null";
                }
            }
            catch (Exception ex)
            {
                HasError = true;
                ErrorMessage = ex.Message;
                ErrorStacktrace = ex.StackTrace;
                StatusErrorMessage = "General find error";
            }
            return lstUserProxy;
        }

        //public Member FindbyAuthtoken(string authtoken)
        //{
        //    Member member = null;
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(key))
        //        {
        //            BusFacCore busFacCore = new BusFacCore(_settings.DefaultConnection);

        //        }
        //        else
        //        {
        //            HasError = true;
        //            ErrorMessage = "Authtoken in input parameter not specified";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        HasError = true;
        //        ErrorStacktrace = ex.StackTrace;
        //        ErrorMessage = ex.Message;
        //    }
        //    return member;
        //}
    }
}
