namespace TeamTaskClient.ApplicationLayer.Models
{
    public class TeamModel
    {
        public int TeamId { get; set; }
        public string TeamTag { get; set; }
        public int UserRole { get; set; }
        public string TeamName { get; set; }
        public string TeamLeadName { get; set; }
        public List<UserModel> Users { get; set; }
    }
}
