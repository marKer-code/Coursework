namespace DAL.Configurations
{
    using DAL.Entities;
    using System.Data.Entity.ModelConfiguration;

    class Message_Configurations : EntityTypeConfiguration<Message>
    {
        public Message_Configurations()
        {
            HasKey(m => m.Id);
            Property(m => m.SenderId)
                .IsRequired();
            Property(m => m.ReceiverId)
                .IsRequired();
            Property(m => m.SendTime)
                .IsRequired();
        }
    }
}
