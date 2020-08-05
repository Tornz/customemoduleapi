using CustomeModule.Model.Dto;
using CustomeModule.Model.Model;
using System.Collections.Generic;

namespace CustomeModule.Interfaces.Services.Interface
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        void Remove(User data);
        void Commit();
        void Add(User data);
        void Update(User data);
        IEnumerable<UserListVM> GetAllUserLists();
        User AddUserData(User data);
        bool UpdateUserData(User data);
        void updateImagePath(int id, string path);
        void updateSignature(int id, string path);
        User CheckAccess(string email, string password);
        UserListVM GetUserListsById(int id);
        bool changePassword(int id, string oldPassword, string newPassword);

    }
}
