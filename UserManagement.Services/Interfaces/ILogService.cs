using System.Collections.Generic;
using UserManagement.Models;

namespace UserManagement.Services.Domain.Interfaces;

public interface ILogService
{
    IEnumerable<Log> GetLog(int id);
    IEnumerable<Log> GetAll();
}