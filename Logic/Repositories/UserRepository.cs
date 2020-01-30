using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DAL.Services;
using Logic.Models;

namespace Logic.Repositories
{
    public class UserRepository: BaseRepository<User>
    {
        public UserRepository(DataResolver dataResolver) : base(dataResolver)
        {
        }

        public async Task<int> Save(User user)
        {
            return await DataResolver.AddOne(new User(){Email = user.Email,Name = user.Name, Password = user.Password.ToHash()});
        }

        public async Task<User> CheckLogin(User user)
        {
            return await DataResolver.GetSingle<User>(
                $"{nameof(User.Email)} = @{nameof(User.Email)} and {nameof(User.Password)} = @{nameof(User.Password)}", 
                new {user.Email, Password = user.Password.ToHash() });
        }
    }
}
