namespace DAL.Entities
{
    using System;

    public class Request
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public DateTime SendTime { get; set; }
    }
}
