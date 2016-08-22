
namespace RESTAPI.Repository
{
    public interface IKeyRepository
    {
        bool CheckValidApiKey(string apikey);
    }
}
