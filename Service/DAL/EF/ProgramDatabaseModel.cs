namespace DAL
{
    using DAL.EF;
    using DAL.Entities;
    using System.Data.Entity;

    public class ProgramDatabaseModel : DbContext
    {
        public ProgramDatabaseModel()
            : base("name=ProgramDatabaseModel")
            => Database.SetInitializer(new Initializer());

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserIP> UserIPs { get; set; }
        public virtual DbSet<UserInfo> UserInfos { get; set; }
        public virtual DbSet<Couple> Couples { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
    }
}