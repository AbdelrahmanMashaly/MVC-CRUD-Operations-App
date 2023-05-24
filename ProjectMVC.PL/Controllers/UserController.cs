using AutoMapper;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectMVC.PL.Helpers;
using ProjectMVC.PL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.PL.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper mapper;

        public UserController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,IMapper mapper)
		{
			_userManager = userManager;
			_signInManager = signInManager;
            this.mapper = mapper;
        }
		public async Task<IActionResult> Index(string Email)
		{
			if (string.IsNullOrEmpty(Email))
			{
				var user = await _userManager.Users.
					Select(U => new UserViewModel()
					{
						Id = U.Id,
						FName = U.Fname,
						LName = U.Lname,
						Phone = U.PhoneNumber,
						Email = U.Email,
						Roles = _userManager.GetRolesAsync(U).Result
					}).ToListAsync();
				return View(user);
			}
			else
			{
				var user = await _userManager.FindByEmailAsync(Email);
				var MappedUser = new UserViewModel()
				{
					Id = user.Id,
					FName = user.Fname,
					LName = user.Lname,
					Phone = user.PhoneNumber,
					Email = user.Email,
					Roles = _userManager.GetRolesAsync(user).Result
				};
				return View(new List<UserViewModel>() { MappedUser});
			}
            
        }
        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");     
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id,UserViewModel userVM)
        {
            if (id != userVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);
                    user.PhoneNumber = userVM.Phone;
                    user.Fname = userVM.FName;
                    user.Lname = userVM.LName;
                   await _userManager.UpdateAsync(user);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(userVM);
        }
        public async Task<IActionResult> Details(string? id, string ViewName = "Details")
            {
                if (id is null)
                    return BadRequest();
				var user = await _userManager.FindByIdAsync(id);

                if (user is null)
                    return NotFound();
                var MappedEmp = mapper.Map<ApplicationUser, UserViewModel>(user);
                return View(ViewName, MappedEmp);
            }
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed( string id)
        {
            
            try
            {
				var user = await _userManager.FindByIdAsync(id);
			 await	_userManager.DeleteAsync(user);
			
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
				return RedirectToAction("Error", "Home");
            }
        }
    }
}
