using Vetapp.Engine.DataAccessLayer.Data;
using Vetapp.Client.ProxyCore;
namespace RESTAPI.Utils
{
    public class DataToModelConverter
    {
        public static UserProxy ConvertToModel(User pUser)
        {
            UserProxy model = new UserProxy()
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
                CanTextMsg = (pUser.CanTextMsg == null) ? false : (bool)pUser.CanTextMsg,
                IsDisabled = (pUser.IsDisabled == null) ? false : (bool)pUser.IsDisabled,
                AuthName = pUser.AuthName,
                AuthNickname = pUser.AuthNickname
            };

            return model;
        }
    }
}
