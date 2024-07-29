using System.Collections.Generic;
using System.Linq;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class UserService(IDataContext dataAccess) : IUserService
{
    private readonly IDataContext _dataAccess = dataAccess;

    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>

    // Method to get all users and return based on activity type.
    public IEnumerable<User> FilterByActive(bool isActive)
    {
        return _dataAccess.GetAll<User>().Where(user => user.IsActive == isActive);
    }

    // Method to get all users.
    public IEnumerable<User> GetAll() => _dataAccess.GetAll<User>();

    //Method to Create a user.
    public void CreateUser(User user)
    {
        _dataAccess.Create(user);
    }

    // Method to get user by ID, if the user  exists.
    public User? GetUserById(int userId)
    {
        return _dataAccess.GetAll<User>().FirstOrDefault(user => user.Id == userId);
    }
    

    //Method to Delete a user by given ID.
    public bool DeleteUserById(int userId)
    {
        var user = _dataAccess.GetAll<User>().FirstOrDefault(user => user.Id == userId);
        if (user == null) return false;

        _dataAccess.Delete(user);
        return true;

    }

    //Method to update users details.
    public void UpdateUser(User user)
    {
        var oldUser = _dataAccess.GetAll<User>().FirstOrDefault(user => user.Id == user.Id);
        if (oldUser != null)
        {
            // Update the properties.
            oldUser.Forename = user.Forename;
            oldUser.Surname = user.Surname;
            oldUser.Email = user.Email;
            oldUser.DateOfBirth = user.DateOfBirth;
            oldUser.IsActive = user.IsActive;
            _dataAccess.Update(oldUser);
        }
        else
        {
            throw new KeyNotFoundException("User not found.");
        }
    }
      public IEnumerable<Log> GetLogsForUser(int userId) => _dataAccess.GetAll<Log>().Where(user => user.UserId == userId);
    public void CreateLogEntry(Log entry) => _dataAccess.Create(entry);
}