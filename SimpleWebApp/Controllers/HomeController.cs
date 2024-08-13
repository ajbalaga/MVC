using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleWebApp.core.Interface;
using SimpleWebApp.data.Model;
using SimpleWebApp.data.ViewModel;
using SimpleWebApp.Models;
using System.Diagnostics;

namespace SimpleWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISimpleWebAppService _simpleWebAppService;

        public HomeController(ISimpleWebAppService simpleWebAppService)
        {
            _simpleWebAppService = simpleWebAppService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _simpleWebAppService.ValidateUserCredentials(model);

                if (result.Success)
                {
                    if (result.Data.UserTypeID == 2)
                        return RedirectToAction("ClientView", "Home", new { id = result.Data.ID });

                    else
                        return RedirectToAction("AdminView");
                }

                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    ModelState.AddModelError("Password", result.ErrorMessage);
                }
            }

            return View(model);
        }

        // Create client or admin
        public IActionResult CreateNew(int roleid)
        {
            ViewBag.Roles = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "Admin" },
            new SelectListItem { Value = "2", Text = "Client" }
        };
            var userDetails = new UserDetails { UserTypeID = roleid };
            return View(userDetails);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNew(UserDetails user)
        {
            if (ModelState.IsValid)
            {
                var result = await _simpleWebAppService.RegisterNewUser(user);

                if (result.Success)
                {
                    return RedirectToAction("AdminView");
                }
                else
                {
                    // Add error message to ModelState
                    ModelState.AddModelError("", result.ErrorMessage);
                }
            }

            return View(user);
        }

        // Edit client or admin
        public async Task<IActionResult> UpdateUser(int id)
        {
            var result = await _simpleWebAppService.FetchUserById(id);

            if (result.Success)
            {
                return View(result.Data);
            }
            else
            {
                // Handle the error (e.g., redirect to an error page or show a message)
                ModelState.AddModelError("", result.ErrorMessage);
                return RedirectToAction("AdminView");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserDetails user)
        {
            if (ModelState.IsValid)
            {
                var result = await _simpleWebAppService.ModifyUserDetails(user);

                if (result.Success)
                {
                    return RedirectToAction("AdminView");
                }
                else
                {
                    // Add error message to ModelState
                    ModelState.AddModelError("", result.ErrorMessage);
                }
            }

            return View(user);
        }

        // Deactivate user (not delete)
        public async Task<IActionResult> DeleteUser(int userID)
        {
            var result = await _simpleWebAppService.DeactivateUser(userID);

            if (result.Success)
            {
                return RedirectToAction("AdminView");
            }
            else
            {
                // Handle the error (e.g., show a message or redirect)
                ModelState.AddModelError("", result.ErrorMessage);
                return RedirectToAction("AdminView");
            }
        }

        // Client view
        public async Task<IActionResult> ClientView(int id)
        {
            var result = await _simpleWebAppService.FetchUserById(id);

            if (result.Success)
            {
                return View(result.Data);
            }
            else
            {
                // Handle the error (e.g., redirect to an error page or show a message)
                ModelState.AddModelError("", result.ErrorMessage);
                return RedirectToAction("AdminView");
            }
        }

        // Admin view
        public async Task<IActionResult> AdminView()
        {
            var clientResult = await _simpleWebAppService.RetrieveAllClients();
            var adminResult = await _simpleWebAppService.RetrieveAllAdmins();
            var dashboardVM = new DashboardViewModel();

            if (clientResult.Success)
            {
                dashboardVM.ClientList = clientResult.Data;
            }
            else
            {
                // Handle the error
                ModelState.AddModelError("", clientResult.ErrorMessage);
            }

            if (adminResult.Success)
            {
                dashboardVM.AdminList = adminResult.Data;
            }
            else
            {
                // Handle the error
                ModelState.AddModelError("", adminResult.ErrorMessage);
            }

            return View(dashboardVM);
        }
    }

}
