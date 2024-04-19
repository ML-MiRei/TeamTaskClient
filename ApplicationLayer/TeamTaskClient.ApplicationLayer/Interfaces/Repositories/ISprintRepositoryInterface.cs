using System;
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
        Task<SprintModel> CreateSprint(SprintEntity sprintEntity); 
        void DeleteSprint(int sprintId); 
        void ChangeDateStartSprint(int sprintId, DateTime dateStart); 
        void ChangeDateEndSprint(int sprintId, DateTime dateEnd); 
        
    }
}
