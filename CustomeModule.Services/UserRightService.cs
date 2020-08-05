using CustomeModule.Model.Model;
using CustomeModule.Interfaces.Services.Interface;
using System.Collections.Generic;
using CustomeModule.Model.Dto;
using System.Linq;

namespace CustomeModule.Services
{
    public class UserRightService : IUserRightService
    {
        private readonly IRepositoryBase<UserRight> _userRight;
        private readonly IRepositoryBase<User> _user;
        private readonly IRepositoryBase<Module> _module;

        public UserRightService(IRepositoryBase<UserRight> userRight, IRepositoryBase<User> user, IRepositoryBase<Module> module)
        {
            _userRight = userRight;
            _user = user;
            _module = module;
        }
        public IEnumerable<UserRight> GetAllUserRights()
        {
            return _userRight.Get().Where(x => x.nactive == 1).ToList();
        }

        public UserRight GetUserRightById(int id)
        {
            return _userRight.GetByID(id);
        }

        public void Remove(UserRight data)
        {
            _userRight.Delete(data);
        }

        public void Commit()
        {
            _userRight.Save();
        }

        public void Add(UserRight data)
        {
            _userRight.Insert(data);
        }

        public void Update(UserRight data)
        {
            _userRight.Update(data);
        }

        public IEnumerable<UserRightListVM> GetAllUserRightLists()
        {
            var data = (from a in _user.Get().Where(row=>row.nactive.Equals(1))
                        join b in _userRight.Get().Where(row=>row.nactive.Equals(1)) on a.userid equals b.userid
                        join c in _module.Get() on b.moduleid equals c.moduleid 
                        join d in _user.Get() on b.createdby equals d.userid
                        select new UserModuleListVM()
                        {
                            userid = a.userid,
                            name = a.name,
                            createddate = a.createddate,
                            createdby = a.createdby,
                            nactive = a.nactive,
                            userrightid = b.userrightid,
                            moduleid = b.moduleid,
                            modulename = c.modulename,
                            screatedby = d.name
                        }).GroupBy(a => new
                        {
                            a.userid,
                            a.name,
                            a.createdby,
                            a.createddate,
                            a.nactive,
                            a.screatedby
                        }).Select(k => new UserRightListVM()
                        {
                            userid = k.Key.userid,
                            name = k.Key.name,
                            createddate = k.Key.createddate,
                            createdby = k.Key.createdby,
                            nactive = k.Key.nactive,
                            screatedby = k.Key.screatedby,
                            rights = k.ToList()
                        });
                        
                        
            return data;
        }

        public IEnumerable<ModuleListVM> GetAllModuleByUser(int id)
        {
            var data = (from a in _userRight.Get().Where(x => x.userid == id)
                        select new ModuleListVM()
                        {
                            userrightid = a.userrightid,
                            moduleid = a.moduleid,
                            modulename = _module.Get().Where(y => y.moduleid == a.moduleid).Select(z => z.modulename).FirstOrDefault(),
                            userid = a.userid,
                            createddate = a.createddate,
                            createdby = a.createdby,
                            nactive = a.nactive
                        });
            return data.ToList();
        }

        public bool UpdateUserRightData(IEnumerable<UserRight> data)
        {
            var isSuccess = false;
            try
            {
                foreach (var item in data)
                {
                    item.userrightid = _userRight.Get().Where(s => s.userid == item.userid && s.moduleid == item.moduleid).Select(ss => ss.userrightid).FirstOrDefault();
                    int count = _userRight.Get().Where(p => p.userid == item.userid).Count();
                    if (count > data.Count())
                    {
                        var id = _userRight.Get()
                            .Where(p => p.userid == item.userid)
                            .Select(aa => aa.userrightid)
                            .ToArray();
                        var ids = new List<int>();
                        foreach (var i in id)
                        {
                            if (!data.Any(x => x.userrightid == i))
                                ids.Add(i);
                        }
                        foreach (var a in ids)
                        {
                            var toDeleted = _userRight.Get()
                                .Where(p => p.userrightid == a && p.userid == item.userid)
                                .FirstOrDefault();
                            _userRight.Delete(toDeleted);
                            _userRight.Save();
                        }
                    }
                    else
                    {
                        var selectedItem = _userRight.Get().Where(p => p.userid == item.userid && p.moduleid == item.moduleid).ToList();
                        if (selectedItem.Count() == 0)
                        {
                            var module = new UserRight()
                            {
                                userrightid = 0,
                                userid = item.userid,
                                moduleid = item.moduleid,
                                createddate = item.createddate,
                                createdby = item.createdby,
                                nactive = item.nactive
                            };
                            _userRight.Insert(module);
                            _userRight.Save();
                        }
                    }
                }
                isSuccess = true;
            }
            catch (System.Exception)
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        public bool AddUserRightData(IEnumerable<UserRight> data)
        {
            var isSuccess = false;
            try
            {
                foreach (var item in data)
                {
                    var module = new UserRight()
                    {
                        userrightid = 0,
                        userid = item.userid,
                        moduleid = item.moduleid,
                        createddate = item.createddate,
                        createdby = item.createdby,
                        nactive = item.nactive
                    };
                    _userRight.Insert(module);
                    _userRight.Save();
                }
                isSuccess = true;
            }
            catch (System.Exception)
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        public void DeleteUserRight(int id)
        {
            var data = _userRight.Get().Where(x => x.userid == id).ToList();
            foreach (var item in data)
            {
                _userRight.Delete(item);
                _userRight.Save();
            }
        }
        public IEnumerable<UserRight> GetUserRightPerUser(int id)
        {
            return _userRight.Get().Where(row => row.userid.Equals(id));
        }
    }
}
