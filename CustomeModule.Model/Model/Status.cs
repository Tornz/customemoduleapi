using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CustomeModule.Model.Model
{
    [Table("ref_status")]
    public class Status
    {
        [Key]
        public int statusid { get; set; }
        public string status { get; set; }
        public DateTime createddate { get; set; }
        public int createdby { get; set; }
        public DateTime lastupdateddate { get; set; }
        public int lastupdatedby { get; set; }
        public int nactive { get; set; }     
    }
}