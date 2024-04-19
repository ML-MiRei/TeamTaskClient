namespace TeamTaskClient.ApplicationLayer.DTOs
{
    public class ChatDTO
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public int? AdminId { get; set; }
        public string? AdminTag { get; set; }
        public int ChatType { get; set; }

    }
}
