namespace DAL.Configurations
{
    using DAL.Entities;
    using System.Data.Entity.ModelConfiguration;

    class Group_Configurations : EntityTypeConfiguration<Group>
    {
        public Group_Configurations()
        {
            HasKey(g => g.Id);
            Property(g => g.Title)
                .IsRequired();
            Property(g => g.Image)
                .IsRequired();
        }
    }
}