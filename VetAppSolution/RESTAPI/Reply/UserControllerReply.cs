using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Vetapp.Client.ProxyCore;
using Vetapp.Engine.BusinessFacadeLayer;
using Vetapp.Engine.DataAccessLayer.Data;
using Vetapp.Engine.DataAccessLayer.Enumeration;
using RESTAPI.Utils;

namespace RESTAPI.Reply
{
    public class UserControllerReply : ReplyBase
    {
        public UserControllerReply(AppSettings settings)
        {
            _settings = settings;
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

        public List<UserProxy> Find(IQueryCollection queryString)
        {
            List<UserProxy> lstUserProxy = new List<UserProxy>();
            try
            {
                string email = queryString.FirstOrDefault(x => x.Key == "email").Value;
                string authuserid = queryString.FirstOrDefault(x => x.Key == "authuserid").Value;
                if ((!string.IsNullOrEmpty(email)) || (!string.IsNullOrEmpty(authuserid)))
                {
                    EnumUser enumUser = new EnumUser() { EmailAddress = email, AuthUserid = authuserid};
                    BusFacCore busFacCore = new BusFacCore(_settings.DefaultConnection);
                    ArrayList arUser = busFacCore.UserGetList(enumUser);
                    if ((arUser != null) && (arUser.Count > 0))
                    {
                        UserProxy userProxy = null;
                        foreach(User user in arUser)
                        {
                            userProxy = DataToModelConverter.ConvertToModel(user);
                            lstUserProxy.Add(userProxy);
                        }
                    }
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
