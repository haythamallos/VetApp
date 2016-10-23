using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vetapp.Engine.DataAccessLayer.Data;
using RESTAPI.Models;

namespace RESTAPI.Utils
{
    public class DataToModelConverter
    {
        public UserModel Convert(User pUser)
        {
            UserModel model = new UserModel()
            {
                UserID = pUser.UserID,
                AuthUserid = pUser.AuthUserid,
                AuthAccessToken = pUser.AuthAccessToken,
                AuthConnection = pUser.AuthConnection,
                AuthIdToken = pUser.AuthIdToken,
                AuthProvider = pUser.AuthProvider,
                EmailAddress = pUser.EmailAddress,
                Firstname = pUser.Firstname,
                Lastname = pUser.Lastname,
                Middlename = pUser.Middlename,
                PhoneNumber = pUser.PhoneNumber,
                Profileimageurl = pUser.AuthProvider,
                CanTextMsg = (bool)pUser.CanTextMsg,
                IsDisabled = (bool)pUser.IsDisabled
            };

            return model;
        }
    }
}
