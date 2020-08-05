using CustomeModule.Model.Model;
using System.Collections.Generic;

namespace CustomeModule.Interfaces.Services.Interface
{
    public interface IModuleService
    {
        IEnumerable<Module> GetAllModules();
        Module GetModuleById(int id);
        void Remove(Module data);
        void Commit();
        void Add(Module data);
        void Update(Module data);
    }
}
