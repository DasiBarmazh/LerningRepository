﻿using Dal.Entities;

namespace BL.Models;

public class LoginResponse
{
    public User User { get; set; }
    public string Token { get; set; }
    
}
