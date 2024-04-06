using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskClient.ApplicationLayer.Models
{
    public class TeamModel
    {
        public int ID { get; set; }
        public string Tag { get; set; }
        public string Name { get; set; }
        public string LeadTag { get; set; }
        public string LeadName { get; set; }
        public List<UserModel> Users { get; set; }
    }
}
