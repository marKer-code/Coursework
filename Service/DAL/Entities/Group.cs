namespace DAL.Entities
{
    using System.Collections.Generic;

    public class Group
    {
        public int Id { get; set; }
        public int CreatorId { get; set; }
        public string Title { get; set; }
        public byte[] Image { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}