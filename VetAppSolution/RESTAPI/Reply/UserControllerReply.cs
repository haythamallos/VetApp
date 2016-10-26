using System;
using RESTAPI.Models;
using Vetapp.Engine.BusinessFacadeLayer;
using Vetapp.Engine.DataAccessLayer.Data;
using RESTAPI.Utils;

namespace RESTAPI.Reply
{
    public class UserControllerReply : ReplyBase
    {
        public UserControllerReply(AppSettings settings)
        {
            _settings = settings;
        }
        public UserModel Create(UserModel bodyUserModel)
        {
            UserModel userModel = null;
            try
            {
                if (!string.IsNullOrEmpty(bodyUserModel.AuthUserid))
                {
                    BusFacCore busFacCore = new BusFacCore(_settings.DefaultConnection);
                    User user = busFacCore.UserEnumByAuthUserid(bodyUserModel.AuthUserid);
                    if ((user == null) && (!busFacCore.HasError))
                    {
                        // create the member
                        user = new User()
                        {
                            AuthAccessToken = bodyUserModel.AuthAccessToken,
                            AuthConnection = bodyUserModel.AuthConnection,
                            AuthIdToken = bodyUserModel.AuthIdToken,
                            AuthName = bodyUserModel.AuthName,
                            AuthNickname = bodyUserModel.AuthNickname,
                            AuthProvider = bodyUserModel.AuthProvider,
                            AuthUserid = bodyUserModel.AuthUserid,
                            CanTextMsg = bodyUserModel.CanTextMsg,
                            EmailAddress = bodyUserModel.EmailAddress,
                            Firstname = bodyUserModel.Firstname,
                            Lastname = bodyUserModel.Lastname,
                            Middlename = bodyUserModel.Middlename,
                            PhoneNumber = bodyUserModel.PhoneNumber,
                            Profileimageurl = bodyUserModel.Profileimageurl
                        };

                        long lID = busFacCore.UserCreateOrModify(user);
                    }
                    if ((user != null) && (user.UserID > 0))
                    {
                        userModel = DataToModelConverter.ConvertToModel(user);
                    }
                    else
                    {
                        HasError = true;
                        ErrorMessage = "General error in creating or getting existing user.";
                    }
                }
                else
                {
                    HasError = true;
                    ErrorMessage = "AuthUserid in input parameter not specified";
                }
            }
            catch (Exception ex)
            {
                HasError = true;
                ErrorStacktrace = ex.StackTrace;
                ErrorMessage = ex.Message;
            }
            return userModel;
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
