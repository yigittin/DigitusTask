using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitusTask.Models
{
    public class SiteOperations
    {
        public DateTime? StartDate { get; set; }
    }
    public enum Roles
    {
        SuperAdmin,
        Admin,
        Moderator,
        Basic
    }
}
