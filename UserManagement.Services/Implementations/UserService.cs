using System.Collections.Generic;
using System.Linq;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class UserService : IUserService
{
    private readonly IDataContext _dataAccess;
    public UserService(IDataContext dataAccess) => _dataAccess = dataAccess;

    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    public IEnumerable<User> FilterByActive(bool isActive)
    {
        return _dataAccess.GetAll<User>().Where(user => user.IsActive == isActive);
    }
    public void CreateUser(User user) 
    {
         _dataAccess.Create(user);
    }
    public bool DeleteUserById(int userId)
    {
        var user = _dataAccess.GetAll<User>().FirstOrDefault(user => user.Id == userId);
        if (user == null) return false;
        else _dataAccess.Delete(user);
        return true;
    }
    public IEnumerable<User> GetAll() => _dataAccess.GetAll<User>();
    public User? GetUserById(int userId) 
    {
        return _dataAccess.GetAll<User>().FirstOrDefault(user => user.Id == userId);
    }
    public void UpdateUser(User user)
        {
        var existingUser = _dataAccess.GetAll<User>().FirstOrDefault(user => user.Id == user.Id);
        if (existingUser != null)
        {
            existingUser.Forename = user.Forename;
            existingUser.Surname = user.Surname;
            existingUser.Email = user.Email;
            existingUser.DateOfBirth = user.DateOfBirth;
            existingUser.IsActive = user.IsActive;
            _dataAccess.Update(existingUser);
        }
        else
        {
            throw new KeyNotFoundException("User not found.");
        }
    }
}
