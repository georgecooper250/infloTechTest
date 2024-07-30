using System.Linq;
using System;
using System.Text;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("users")]
public class UsersController(IUserService userService) : Controller
{
    private readonly IUserService _userService = userService;

        [BindProperty]
    public User newUser { get; set; } = default!;

    [HttpGet]
    public ViewResult List(string status = "all")
    {
        /*if the user has active value, call filter by action with
        corresponding active value, else call get all*/
        IEnumerable<User> users = status.ToLower() switch
        {
            "active" => _userService.FilterByActive(true),
            "inactive" => _userService.FilterByActive(false),
            _ => _userService.GetAll(),
        };
        var items = users.Select(p => new UserListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            Email = p.Email,
            IsActive = p.IsActive,
            DateOfBirth = p.DateOfBirth
        });

        var model = new UserListViewModel
        {
            Items = items.ToList()
        };
        return View(model);
    }

    [HttpPost("add")]
    public IActionResult AddUser(User user)
    {
        if (ModelState.IsValid)
        {
            var builtLog = BuildLogEntry(user.Id);

            _userService.CreateUser(user);
            ViewBag.Success = "User successfully created!";

        var logEntry = new Log(user.Id, builtLog, DateTime.Now);
             _userService.CreateLogEntry(logEntry);
            return PartialView("_AddUserModal", new User()); 
        }
        return PartialView("_AddUserModal", user);
    }

    [HttpPost("delete")]
    public IActionResult DeleteUser(int userId)
    {
        var isDeleted = _userService.DeleteUserById(userId);
        if (isDeleted)
        {
            ViewBag.Success = "User successfully deleted!";
        }
        else
        {
            ViewBag.ErrorMessage = "User not found.";
        }
        return PartialView("_DeleteUserModal");
    }

    [HttpPost("update")]
    public IActionResult EditUser(User user, int userId)
    {
              var logEntry = new Log(userId, BuildLogEntry(userId), DateTime.Now);
        _userService.CreateLogEntry(logEntry);

        if (ModelState.IsValid)
        {
            var updateUser = _userService.GetUserById(userId);
            if (updateUser != null)
            {
                updateUser.Forename = user.Forename;
                updateUser.Surname = user.Surname;
                updateUser.Email = user.Email;
                updateUser.IsActive = user.IsActive;
                updateUser.DateOfBirth = user.DateOfBirth;
                _userService.UpdateUser(updateUser);
                ViewBag.Success = "User successfully updated!";
                return PartialView("_EditUserModal", updateUser);
            }
            else
            {
                ViewBag.ErrorMessage = "User not found.";
            }
        }
        return PartialView("_EditUserModal", user);
    }
   private string BuildLogEntry(int userId) {
        var log = new StringBuilder();

        var oldUser = _userService.GetUserById(userId);

    if(oldUser != null){
        if(newUser.Forename != oldUser.Forename) {
            log.AppendLine($"Forename changed from {oldUser.Forename} to {newUser.Forename}");
        }
        if(newUser.Surname != oldUser.Surname) {
            log.AppendLine($"Surname changed from {oldUser.Surname} to {newUser.Surname}");
        }
        if(newUser.Email != oldUser.Email) {
            log.AppendLine($"Email changed from {oldUser.Email} to {newUser.Email}");
        }
        if(newUser.IsActive != oldUser.IsActive) {
            log.AppendLine($"Account Active changed from {(oldUser.IsActive ? "Yes" : "No")} to {(newUser.IsActive ? "Yes" : "No")}");
        }
        if(newUser.DateOfBirth != oldUser.DateOfBirth) {
            log.AppendLine($"Date of Birth changed from {oldUser.DateOfBirth.ToString("dd/MM/yyyy")} to {newUser.DateOfBirth.ToString("dd/MM/yyyy")}");
        }
} else {
        log.AppendLine($"New User {newUser.Forename} {newUser.Surname} created.");

}
        return log.ToString();
    }
}