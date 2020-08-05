using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CustomeModule.Model.Model
{
    [Table("user_type")]
    public class UserType
    {
        [Key]
        public int usertypeid { get; set; }
        public string name { get; set; }
    }
}
