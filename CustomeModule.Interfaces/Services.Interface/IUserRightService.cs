using CustomeModule.Model.Dto;
using CustomeModule.Model.Model;
using System.Collections.Generic;

namespace CustomeModule.Interfaces.Services.Interface
{
    public interface IUserRightService
    {
        IEnumerable<UserRight> GetAllUserRights();
        UserRight GetUserRightById(int id);
        void Remove(UserRight data);
        void Commit();
        void Add(UserRight data);
        void Update(UserRight data);
        IEnumerable<UserRightListVM> GetAllUserRightLists();
        bool UpdateUserRightData(IEnumerable<UserRight> data);
        bool AddUserRightData(IEnumerable<UserRight> data);
        void DeleteUserRight(int id);
        IEnumerable<UserRight> GetUserRightPerUser(int id);
    }
}
