using System.Collections.Generic;
using UserManagement.Models;

namespace UserManagement.Services.Domain.Interfaces;

public interface IUserService 
{
    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
     void CreateUser(User user);
    IEnumerable<User> FilterByActive(bool isActive);
    IEnumerable<User> GetAll();
    User? GetUserById(int userId);
    bool DeleteUserById(int userId);
    void UpdateUser(User user);
}
