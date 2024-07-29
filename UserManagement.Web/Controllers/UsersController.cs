using System.Linq;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("users")]
public class UsersController(IUserService userService) : Controller
{
    private readonly IUserService _userService = userService;

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
            _userService.CreateUser(user);
            ViewBag.Success = "User successfully created!";
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

}