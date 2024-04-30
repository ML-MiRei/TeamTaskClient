namespace TeamTaskClient.ApplicationLayer.Models
{
    public class ProjectTaskModel
    {
        public int ProjectTaskId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public int Status { get; set; }

        public string ExecutorName { get; set; }
        public string ExecutorTag { get; set; }

    }
}
