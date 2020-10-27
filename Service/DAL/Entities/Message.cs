namespace DAL.Entities
{
    using System;

    public class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Text { get; set; }
        public string ImageName { get; set; }
        public byte[] Image { get; set; }
        public string FileName { get; set; }
        public byte[] File { get; set; }
        public DateTime SendTime { get; set; }
        public DateTime ReadTime { get; set; }
    }
}
