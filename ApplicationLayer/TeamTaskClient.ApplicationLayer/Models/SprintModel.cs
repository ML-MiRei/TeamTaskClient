namespace TeamTaskClient.ApplicationLayer.Models
{
    public class SprintModel
    {
        public int SprintId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public List<ProjectTaskModel> Tasks { get; set; }

    }
}
