using CinemaTicketingSystem.Domain.DomainModels;
using CinemaTicketingSystem.Domain.Identity;
using CinemaTicketingSystem.Service.Interface;
using CinemaTicketingSystem.Domain.DTO;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketingSystem.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<CinemaTicketsAppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(IUserService userService, UserManager<CinemaTicketsAppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userService = userService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View(this._userService.GetAllUsers());
        }


        [HttpGet]
        public async Task<IActionResult> ManageUserRole(string userId)
        {
            ViewBag.userId = userId;

            var user = await _userManager.FindByIdAsync(userId);

            if(user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("Not Found");
            }

            var model = new List<UserRoles>();

            foreach(var role in _roleManager.Roles)
            {
                var userRoleModel = new UserRoles
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if(await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleModel.IsSelected = true;
                }
                else
                {
                    userRoleModel.IsSelected = false;
                }

                model.Add(userRoleModel);

            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUserRole(List<UserRoles> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if(user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("Not Found");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove users existing roles");
                return View(model);
            }

            result = await _userManager.AddToRolesAsync(user,
                model.Where(z => z.IsSelected).Select(y => y.RoleName));

            this._userService.UpdateUserRole(userId, model);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }

            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public IActionResult ImportUsers()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ImportUsers(IFormFile file)
        {

            //make a copy
            string pathToUpload = $"{Directory.GetCurrentDirectory()}\\files\\{file.FileName}";


            using (FileStream fileStream = System.IO.File.Create(pathToUpload))
            {
                file.CopyTo(fileStream);

                fileStream.Flush();
            }

            //read data from uploaded file

            List<UserRegistrationDto> users = getUsersFromExcelFile(file.FileName);


            foreach (var item in users)
            {
                var userCheck = _userManager.FindByEmailAsync(item.Email).Result;
                if (userCheck == null)
                {
                    var user = new CinemaTicketsAppUser
                    {
                        FirstName = item.Name,
                        LastName = item.LastName,
                        UserName = item.Email,
                        NormalizedUserName = item.Email,
                        Email = item.Email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        Role = _roleManager.FindByIdAsync(item.Role).Result.Name,
                        UserCart = new ShoppingCart()
                    };
                    var result = _userManager.CreateAsync(user, item.Password).Result;

                    if (result.Succeeded)
                    {
                        var role = _roleManager.FindByIdAsync(item.Role).Result;

                        if (role != null)
                        {
                            IdentityResult roleresult = await _userManager.AddToRoleAsync(user, role.Name);

                        }

                    }

                    //status = status & result.Succeeded;
                }
                else
                {
                    continue;
                }
            }


            return RedirectToAction("Index", "User");
        }

        private List<UserRegistrationDto> getUsersFromExcelFile(string fileName)
        {

            string pathToFile = $"{Directory.GetCurrentDirectory()}\\files\\{fileName}";

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            List<UserRegistrationDto> userList = new List<UserRegistrationDto>();

            using (var stream = System.IO.File.Open(pathToFile, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        userList.Add(new UserRegistrationDto
                        {
                            Email = reader.GetValue(0).ToString(),
                            Password = reader.GetValue(1).ToString(),
                            ConfirmPassword = reader.GetValue(2).ToString(),
                            Role = reader.GetValue(3).ToString()
                        });
                    }
                }
            }

            return userList;

        }

    }
}
