using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskClient.ApplicationLayer.Models
{
    public class ProjectModel
    {
        public int ID { get; set; }
        public string TagLeader { get; set; }
        public string LeaderName { get; set; }
        public string Name { get; set; }
        public List<SprintModel> Sprints { get; set; }
        public List<UserModel> Users { get; set; }


        
    }
}
