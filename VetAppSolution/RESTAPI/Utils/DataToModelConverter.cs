using Vetapp.Engine.DataAccessLayer.Data;
using Vetapp.Client.ProxyCore;
namespace RESTAPI.Utils
{
    public class DataToModelConverter
    {
        public static UserProxy ConvertToProxy(User pUser)
        {
            UserProxy model = new UserProxy()
            {
                UserID = pUser.UserID,
                Firstname = pUser.Firstname,
                Lastname = pUser.Lastname,
                Middlename = pUser.Middlename,
                PhoneNumber = pUser.PhoneNumber,
                Username = pUser.Username,
                IsDisabled = (pUser.IsDisabled == null) ? false : (bool)pUser.IsDisabled
            };

            return model;
        }
    }
}
