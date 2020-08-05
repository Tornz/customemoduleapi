using CustomeModule.Model.Model;
using System.Collections.Generic;

namespace CustomeModule.Interfaces.Services.Interface
{
    public interface IUserTypeService
    {
        IEnumerable<UserType> GetAllUserTypes();
        UserType GetUserTypeById(int id);
        void Remove(UserType data);
        void Commit();
        void Add(UserType data);
        void Update(UserType data);
    }
}
