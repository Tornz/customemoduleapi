using System;
using System.Collections.Generic;

namespace CustomeModule.Model.Dto
{
    public class UserRightListVM
    {
        public int userid { get; set; }
        public string name { get; set; }
        public DateTime createddate { get; set; }
        public int createdby { get; set; }
        public string screatedby { get; set; }
        public int nactive { get; set; }
        public IEnumerable<UserModuleListVM> rights { get; set; }
    }

    public class ModuleListVM
    {
        public int userrightid { get; set; }
        public int moduleid { get; set; }
        public string modulename { get; set; }
        public int userid { get; set; }
        public DateTime createddate { get; set; }
        public int createdby { get; set; }
        public int nactive { get; set; }
    }


    public class UserModuleListVM
    {

        public int userid { get; set; }
        public string name { get; set; }
        public DateTime createddate { get; set; }
        public int createdby { get; set; }
        public int nactive { get; set; }

        public int userrightid { get; set; }
        public int moduleid { get; set; }
        public string modulename { get; set; }
        public string screatedby { get; set; }



    }

}
