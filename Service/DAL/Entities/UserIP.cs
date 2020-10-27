using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class UserIP
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public string IP { get; set; }

        public virtual User User { get; set; }
    }
}
