using System.Linq;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("logs")]
public class LogsController : Controller
{
    private readonly ILogService _logService;

    public LogsController(ILogService logService) => _logService = logService;


    [HttpGet("/listlogs")]
    public ViewResult ListLogs()
    {
        var items = _logService.GetAll().Select(p => new LogListItemViewModel
        {
            LogId = p.LogId,
            UserId = p.UserId,
            Info = p.Info,
            TimeStamp = p.TimeStamp
        });

        var model = new LogListViewModel{
            Items = items.ToList()
        };

        return View(model);
    }
}