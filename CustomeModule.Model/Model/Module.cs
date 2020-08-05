using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CustomeModule.Model.Model
{
    [Table("ref_module")]
    public class Module
    {
        [Key]
        public int moduleid { get; set; }
        public string modulename { get; set; }
    }
}
