using CustomeModule.Interfaces.Services.Interface;
using CustomeModule.Model.Model;
using CustomeModule.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomeModule.Services
{
    public class UserTypeService : IUserTypeService
    {
        private readonly IRepositoryBase<UserType> _userType;

        public UserTypeService(IRepositoryBase<UserType> userType)
        {
            _userType = userType;
        }

        public void Add(UserType data)
        {
            _userType.Insert(data);
        }

        public void Commit()
        {
            _userType.Save();
        }

        public IEnumerable<UserType> GetAllUserTypes()
        {
            return _userType.Get();
        }

        public UserType GetUserTypeById(int id)
        {
            return _userType.GetByID(id);
        }

        public void Remove(UserType data)
        {
            _userType.Delete(data);
        }

        public void Update(UserType data)
        {
            _userType.Update(data);
        }
    }
}
