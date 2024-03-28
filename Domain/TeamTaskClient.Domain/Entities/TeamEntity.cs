using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Common;

namespace TeamTaskClient.Domain.Entities
{
    public class TeamEntity : IBaseEntity, IExecutorProjectTask
    {
        public int ID { get; set; }

        [MaxLength(30)]
        public string Tag { get; set; }
        public string Name { get; set; }

        public UserEntity Leader { get; set; }

    }
}
