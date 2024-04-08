﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
