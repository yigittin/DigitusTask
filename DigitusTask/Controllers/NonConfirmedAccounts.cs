﻿using DigitusTask.Models;
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
    public class NonConfirmedAccounts : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public NonConfirmedAccounts(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var regLastDay = DateTime.Now.AddDays(-1);
            var users = await _userManager.Users.Where(x => x.VerificationDate == null&&x.RegisterDate<regLastDay).ToListAsync();
            var userListViewModel = new List<UserListViewModal>();
            List<TimeSpan> verification = new List<TimeSpan>();
            if (users.Count > 0)
            {
                foreach (ApplicationUser user in users)
                {
                    var thisViewModal = new UserListViewModal();
                    thisViewModal.UserId = user.Id;
                    thisViewModal.Email = user.Email;
                    thisViewModal.FirstName = user.FirstName;
                    thisViewModal.LastName = user.LastName;
                    thisViewModal.RegisterDate = (DateTime)user.RegisterDate;
                    verification.Add(thisViewModal.UserVerificationTime);
                    userListViewModel.Add(thisViewModal);
                }
                
                return View(userListViewModel);
            }
            else
            {
                var thisViewModal = new UserListViewModal();
                thisViewModal.UserId = "Aranan";
                thisViewModal.Email = "Bulunamadı";
                thisViewModal.FirstName = "Tarihte";
                thisViewModal.LastName = "Değer";
                userListViewModel.Add(thisViewModal);

                return View(userListViewModel);
            }
        }
        
    }
}
