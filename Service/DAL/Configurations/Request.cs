namespace DAL.Configurations
{
    using DAL.Entities;
    using System.Data.Entity.ModelConfiguration;

    class Request_Configurations : EntityTypeConfiguration<Request>
    {
        public Request_Configurations()
        {
            HasKey(r => r.Id);
            Property(r => r.ReceiverId)
                .IsRequired();
            Property(r => r.SenderId)
                .IsRequired();
            Property(r => r.ReceiverId)
                .IsRequired();
            Property(r => r.SendTime)
                .IsRequired();
        }
    }
}
