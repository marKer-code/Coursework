namespace DAL.Configurations
{
    using DAL.Entities;
    using System.Data.Entity.ModelConfiguration;

    class Couple_Configurations : EntityTypeConfiguration<Couple>
    {
        public Couple_Configurations()
        {
            HasKey(c => c.Id);
            Property(c => c.UserId1)
                .IsRequired();
            Property(c => c.UserId2)
                .IsRequired();
        }
    }
}
