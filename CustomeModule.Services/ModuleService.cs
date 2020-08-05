using CustomeModule.Interfaces.Services.Interface;
using CustomeModule.Model.Model;
using CustomeModule.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomeModule.Services
{
    public class ModuleService: IModuleService
    {
        private readonly IRepositoryBase<Module> _module;

        public ModuleService(IRepositoryBase<Module> module)
        {
            _module = module;
        }

        public void Add(Module data)
        {
            _module.Insert(data);
        }

        public void Commit()
        {
            _module.Save();
        }

        public IEnumerable<Module> GetAllModules()
        {
            return _module.Get();
        }

        public Module GetModuleById(int id)
        {
            return _module.GetByID(id);
        }

        public void Remove(Module data)
        {
            _module.Delete(data);
        }

        public void Update(Module data)
        {
            _module.Update(data);
        }
    }
}
