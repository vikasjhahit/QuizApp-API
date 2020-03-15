using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizWebAPI.Models
{
    public class GetUserToken
    {
        public Nullable<int> AccountTypeID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}