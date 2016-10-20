using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace RESTAPI.Models
{
    public class UserRepository : IUserRepository
    {
        private static ConcurrentDictionary<string, UserItem> _users =
              new ConcurrentDictionary<string, UserItem>();

        public UserRepository()
        {
            //Add(new UserItem { Name = "Item1" });
        }

        public IEnumerable<UserItem> GetAll()
        {
            return _users.Values;
        }

        public void Add(UserItem item)
        {
            //item.Key = Guid.NewGuid().ToString();
            //_users[item.Key] = item;
        }

        public UserItem Find(string key)
        {
            UserItem item;
            _users.TryGetValue(key, out item);
            return item;
        }

        public UserItem Remove(string key)
        {
            UserItem item;
            _users.TryRemove(key, out item);
            return item;
        }

        public void Update(UserItem item)
        {
            //_users[item.Key] = item;
        }
    }
}
