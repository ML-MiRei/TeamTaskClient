using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Common;

namespace TeamTaskClient.Domain.Entities
{
    public class ProjectEntity : IBaseEntity
    {
        public int ID { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }
        public UserEntity Leader { get; set; }
    }
}
