using CustomeModule.Model.Model;
using CustomeModule.Interfaces.Services.Interface;
using System.Collections.Generic;
using CustomeModule.Model.Dto;
using System.Linq;

namespace CustomeModule.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryBase<User> _user;

        private readonly IRepositoryBase<UserType> _userType;

        public UserService(IRepositoryBase<User> user, IRepositoryBase<UserType> userType)
        {
            _user = user;

            _userType = userType;
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _user.Get().Where(x => x.nactive == 1);
        }

        public User GetUserById(int id)
        {
            return _user.GetByID(id);
        }

        public void Remove(User data)
        {
            data.nactive = 0;
            _user.Delete(data);
        }

        public void Commit()
        {
            _user.Save();
        }

        public void Add(User data)
        {
            _user.Insert(data);
        }

        public void Update(User data)
        {
            _user.Update(data);
        }

        public IEnumerable<UserListVM> GetAllUserLists()
        {
            var data = (from a in _user.Get().Where(x => x.nactive == 1)                       
                        join c in _userType.Get() on a.usertypeid equals c.usertypeid
                        select new UserListVM()
                        {
                            userid = a.userid,
                            branchid = a.branchid,
          
                            name = a.name,
                            email = a.email,
                            telno = a.telno,
                            address = a.address,
                            username = a.username,
                            password = a.password,
                            userprofile = a.userprofile,
                            usertypeid = a.usertypeid,
                            createddate = a.createddate,
                            createdby = a.createdby,
                            lastupdateddate = a.lastupdateddate,
                            lastupdatedby = a.lastupdatedby,
                            nactive = a.nactive,
                            signaturepath = a.signaturepath
                           
                        }).ToList();
            return data;
        }

        public User AddUserData(User data)
        {
            _user.Insert(data);
            _user.Save();
            return data;
        }

        public bool UpdateUserData(User data)
        {
            _user.Update(data);
            _user.Save();
            return true;
        }

        public void updateImagePath(int id, string path)
        {
            var users = _user.GetByID(id);
            users.userprofile = path;
            _user.Update(users);
        }

        public User CheckAccess(string email, string password)
        {
            var data = new User();
            var userList = _user.Get().Where(row => row.email.Trim().Equals(email.Trim()) && row.password.Equals(password) && row.nactive.Equals(1));
            if (userList.ToList().Count > 0)
            {
                data = userList.FirstOrDefault();
            }
            return data;
        }

        public UserListVM GetUserListsById(int id)
        {
            var data = (from a in _user.Get().Where(x => x.userid.Equals(id))                      
                        join c in _userType.Get() on a.usertypeid equals c.usertypeid
                        select new UserListVM()
                        {
                            userid = a.userid,
                            branchid = a.branchid,
        
                            name = a.name,
                            email = a.email,
                            telno = a.telno,
                            address = a.address,
                            username = a.username,
                            password = a.password,
                            userprofile = a.userprofile,
                            usertypeid = a.usertypeid,
                            createddate = a.createddate,
                            createdby = a.createdby,
                            lastupdateddate = a.lastupdateddate,
                            lastupdatedby = a.lastupdatedby,
                            nactive = a.nactive,
                            signaturepath = a.signaturepath
                        }).ToList();
            return data.FirstOrDefault();
        }

        public void updateSignature(int id, string path)
        {
            var users = _user.GetByID(id);
            users.signaturepath = path;
            _user.Update(users);
        }

        public bool changePassword(int id, string oldPassword, string newPassword)
        {
            var result = false;
            var data = _user.Get().Where(row => row.userid.Equals(id) && row.password.Equals(oldPassword));
            if (data.ToList().Count > 0)
            {
                result = true;
            }
            return result;
        }
    }
}
