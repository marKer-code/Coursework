namespace DAL.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserInfo
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public string Nickname { get; set; }
        public bool Online { get; set; }
        public DateTime LastOnline { get; set; }
        public byte[] Photo { get; set; }

        public virtual User User { get; set; }
    }
}
