using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class LogService : ILogService
{
    private readonly IDataContext _dataAccess;
    public LogService(IDataContext dataAccess) => _dataAccess = dataAccess;

    public IEnumerable<Log> GetLog(int id) => _dataAccess.GetAll<Log>().AsNoTracking().Where(log => log.LogId == id);

    public IEnumerable<Log> GetAll() => _dataAccess.GetAll<Log>();

}