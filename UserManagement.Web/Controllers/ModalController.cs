using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.WebMS.Controllers;

[Route("modals")]
public class ModalController(IUserService userService, ILogService logService) : Controller
{
    private readonly IUserService _userService = userService;
    private readonly ILogService _logService = logService;

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

       [HttpGet("viewUserLogs")]
    public IActionResult ViewUserLogsModal(int logId)
    {
        var log = _logService.GetLog(logId);
        if (log == null)
        {
            ViewBag.ErrorMessage = "Log not found.";
            return PartialView("_ViewUserLogs");
        }
        return PartialView("_ViewUserLogs", log);
    }
}