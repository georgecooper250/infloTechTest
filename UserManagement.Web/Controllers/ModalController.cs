using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.WebMS.Controllers;

[Route("modals")]
public class ModalController(IUserService userService) : Controller
{
    private readonly IUserService _userService = userService;


    [HttpGet("addUserModal")]
    public IActionResult AddUserModalContent()
    {
        return PartialView("_AddUserModal");
    }

    [HttpGet("viewUserModal")]
    public IActionResult ViewUserModalContent(int userId)
    {
        var user = _userService.GetUserById(userId);
        if (user == null)
        {
            ViewBag.ErrorMessage = "User not found.";
            return PartialView("_ViewUserModal");
        }

        return PartialView("_ViewUserModal", user);
    }

    [HttpGet("editUserModal")]
    public IActionResult EditUserModalContent(int userId)
    {
        var user = _userService.GetUserById(userId);
        if (user == null)
        {
            ViewBag.ErrorMessage = "User not found.";
            return PartialView("_EditUserModal");
        }
        return PartialView("_EditUserModal", user);
    }

    [HttpGet("deleteUserModal")]
    public IActionResult DeleteUserModalContent(int userId)
    {
        var user = _userService.GetUserById(userId);
        if (user == null)
        {
            ViewBag.ErrorMessage = "User not found.";
            return PartialView("_DeleteUserModal");
        }
        return PartialView("_DeleteUserModal", user);
    }
}