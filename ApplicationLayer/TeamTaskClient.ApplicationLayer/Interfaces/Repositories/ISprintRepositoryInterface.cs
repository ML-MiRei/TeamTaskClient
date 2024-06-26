﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.ApplicationLayer.Models;
using TeamTaskClient.Domain.Entities;

namespace TeamTaskClient.ApplicationLayer.Interfaces.Repositories
{
    public interface ISprintRepositoryInterface
    {
        Task CreateSprint(int projectId, SprintModel sprintModel);
        Task DeleteSprint(int projectId, int sprintId);
        Task ChangeDateStartSprint(int projectId, int sprintId, DateTime dateStart);
        Task ChangeDateEndSprint(int projectId, int sprintId, DateTime dateEnd); 
        
    }
}
