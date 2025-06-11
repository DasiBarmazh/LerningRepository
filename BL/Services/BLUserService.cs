using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Api;
using BL.Models;
using Dal.Api;
using Dal.Entities;

namespace BL.Services
{
    public class BLUserService : IBLUser
    {
        IUser dalUser;
        public BLUserService(IDal dal)
        {
            dalUser = dal.user;

        }
        public async Task SignUp(User user)
        {
            await dalUser.Create(user);
        }
        public async Task<User?> Login(string username, string phone)
        {
            Console.WriteLine("1");
            return await dalUser.GetUserById(username,  phone);
        }


    }
}
