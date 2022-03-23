using DigitusTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigitusTask.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class VerificationAverages : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;        
        public VerificationAverages(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.Where(x=>x.VerificationDate!=null).ToListAsync();
            var userListViewModel = new List<UserListViewModal>();
            List<TimeSpan> verification=new List<TimeSpan>();
            if (users.Count > 0)
            {
                foreach (ApplicationUser user in users)
                {
                    var thisViewModal = new UserListViewModal();
                    thisViewModal.UserId = user.Id;
                    thisViewModal.Email = user.Email;
                    thisViewModal.FirstName = user.FirstName;
                    thisViewModal.LastName = user.LastName;
                    thisViewModal.VerificationDate = (DateTime)user.VerificationDate;
                    thisViewModal.UserVerificationTime = (TimeSpan)(user.VerificationDate - user.RegisterDate);
                    verification.Add(thisViewModal.UserVerificationTime);
                    userListViewModel.Add(thisViewModal);
                }
                AverageTime(verification);
            return View(userListViewModel);
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
        }
        [HttpPost]
        public async Task<IActionResult> Index(DateTime start)
        {
            ViewBag.start = start;
            DateTime betweenDay = start.AddDays(1);
            var users = await _userManager.Users.Where(x => x.VerificationDate<betweenDay&&x.VerificationDate> start).ToListAsync();
            var userListViewModel = new List<UserListViewModal>();
            List<TimeSpan> verification = new List<TimeSpan>();
            if (users.Count>0)
            {
                foreach (ApplicationUser user in users)
                {
                    var thisViewModal = new UserListViewModal();
                    thisViewModal.UserId = user.Id;
                    thisViewModal.Email = user.Email;
                    thisViewModal.FirstName = user.FirstName;
                    thisViewModal.LastName = user.LastName;
                    thisViewModal.VerificationDate = (DateTime)user.VerificationDate;
                    thisViewModal.UserVerificationTime = (TimeSpan)(user.VerificationDate - user.RegisterDate);
                    verification.Add(thisViewModal.UserVerificationTime);
                    userListViewModel.Add(thisViewModal);
                }
            AverageTime(verification);
               
            return View(userListViewModel);
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

        }
        public string AverageTime(List<TimeSpan> verification)
        {
            string sTime = string.Empty;
            sTime=TimeSpan.FromSeconds(verification.Select(s => s.TotalSeconds).Average()).ToString();
            ViewBag.average = sTime;
            return sTime;
        }
    }
}
