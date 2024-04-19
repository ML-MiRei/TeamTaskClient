namespace TeamTaskClient.ApplicationLayer.Models
{
    public class ProjectModel
    {
        public int ProjectId { get; set; }
        public int UserRole { get; set; }
        public string ProjectLeaderName { get; set; }
        public string ProjectName { get; set; }
        public List<SprintModel> Sprints { get; set; }
        public List<UserModel> Users { get; set; }
        public List<ProjectTaskModel> Tasks { get; set; }


        public int AmountTasks => (Sprints == null ? 0 : Sprints.Sum(s => s.Tasks.Count)) + (Tasks == null? 0 : Tasks.Count);
        
    }
}
