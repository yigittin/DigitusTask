using DigitusTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitusTask.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class UserList : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserList(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.Where(x=>x.EmailConfirmed==true).ToListAsync();
            var userListViewModel = new List<UserListViewModal>();
            if (users.Count > 0)
            {
                ViewBag.Count = users.Count();
                foreach (ApplicationUser user in users)
                {
                    var thisViewModal = new UserListViewModal();
                    thisViewModal.UserId = user.Id;
                    thisViewModal.Email = user.Email;
                    thisViewModal.FirstName = user.FirstName;
                    thisViewModal.LastName = user.LastName;
                    if (user.RegisterDate != null)
                    {
                        thisViewModal.RegisterDate = (DateTime)user.RegisterDate;
                    }
                    if (user.VerificationDate != null)
                    {
                        thisViewModal.VerificationDate = (DateTime)user.VerificationDate;
                    }
                    thisViewModal.Roles = await GetUserRoles(user);
                    userListViewModel.Add(thisViewModal);

                }
            }            
            else
            {
                var thisViewModal = new UserListViewModal();
                thisViewModal.UserId = "Aranan";
                thisViewModal.Email = "Bulunamadı";
                thisViewModal.FirstName = "Tarihte";
                thisViewModal.LastName = "Değer";
                thisViewModal.VerificationDate = DateTime.MinValue;
                thisViewModal.UserVerificationTime = (TimeSpan)(DateTime.MinValue - DateTime.MinValue);
                userListViewModel.Add(thisViewModal);

                return View(userListViewModel);
            }
            return View(userListViewModel);
        }
        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }       
    }
}
