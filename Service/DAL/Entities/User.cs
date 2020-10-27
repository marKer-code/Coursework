namespace DAL.Entities
{
    using System.Collections.Generic;

    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string HashPassword { get; set; }

        public virtual UserIP UserIP { get; set; }
        public virtual UserInfo UserInfo { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
