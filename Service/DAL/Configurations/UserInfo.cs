namespace DAL.Configurations
{
    using DAL.Entities;
    using System.Data.Entity.ModelConfiguration;

    class UserInfo_Configurations : EntityTypeConfiguration<UserInfo>
    {
        public UserInfo_Configurations()
        {
            Property(uI => uI.Nickname)
                .IsRequired()
                .HasMaxLength(15);
            Property(uI => uI.Online)
                    .IsRequired();
            Property(uI => uI.LastOnline)
                    .IsRequired();
            Property(uI => uI.Photo)
                    .IsRequired();
        }
    }
}
