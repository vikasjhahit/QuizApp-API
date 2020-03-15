using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizWebAPI.Models
{
    public class UserGradebook
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Score { get; set; }
        public int TimeSpent { get; set; }
        public int Attempts { get; set; } 
    }
}