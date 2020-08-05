
using System;

namespace CustomeModule.Model.Dto
{
    public class UserListVM
    {
        public int userid { get; set; }
        public int branchid { get; set; }
        public string branchname { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string telno { get; set; }
        public string address { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string userprofile { get; set; }
        public int usertypeid { get; set; }
        public DateTime createddate { get; set; }
        public int createdby { get; set; }
        public DateTime lastupdateddate { get; set; }
        public int lastupdatedby { get; set; }
        public int nactive { get; set; }
        public string signaturepath { get; set; }
    }
}
