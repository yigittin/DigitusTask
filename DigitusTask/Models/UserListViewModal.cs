using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitusTask.Models
{
    public class UserListViewModal 
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }

        public DateTime RegisterDate { get; set; }
        public DateTime VerificationDate { get; set; }
        public TimeSpan UserVerificationTime { get; set; }
    }
}
