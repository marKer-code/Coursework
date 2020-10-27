namespace DAL.Configurations
{
    using DAL.Entities;
    using System.Data.Entity.ModelConfiguration;

    class User_Configurations : EntityTypeConfiguration<User>
    {
        public User_Configurations()
        {
            HasKey(u => u.Id);
            Property(u => u.Login)
                .IsRequired()
                .HasMaxLength(15);
            Property(u => u.HashPassword)
                .IsRequired();
            HasMany(u => u.Groups)
                .WithMany(g => g.Users)
                .Map
                (
                cs =>
                {
                    cs.MapLeftKey("UserRefId");
                    cs.MapRightKey("GroupRefId");
                    cs.ToTable("UserGroup");
                });
        }
    }
}
