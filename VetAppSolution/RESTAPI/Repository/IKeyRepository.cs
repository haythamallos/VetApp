
namespace RESTAPI.Repository
{
    public interface IKeyRepository
    {
        bool CheckValidUserKey(string reqkey);
    }
}
