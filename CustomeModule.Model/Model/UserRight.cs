using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CustomeModule.Model.Model
{
    [Table("user_rights")]
    public class UserRight
    {
        [Key]
        public int userrightid { get; set; }
        public int moduleid { get; set; }
        public int userid { get; set; }        
        public DateTime createddate { get; set; }
        public int createdby { get; set; }
        public int nactive { get; set; }
    }
}