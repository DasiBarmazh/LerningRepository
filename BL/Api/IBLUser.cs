using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Models;
using Dal.Entities;

namespace BL.Api;

public interface IBLUser
{
    Task<User?> Login(string username, string phone);
    public Task SignUp(User user);
}
