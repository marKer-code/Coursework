namespace DAL.Configurations
{
    using DAL.Entities;
    using System.Data.Entity.ModelConfiguration;

    class UserIP_Configurations : EntityTypeConfiguration<UserIP>
    {
        public UserIP_Configurations()
            => Property(uIP => uIP.IP)
                .IsRequired();
    }
}
