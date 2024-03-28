using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskClient.Domain.Common
{
    public interface IBaseEntity
    {
        int ID { get; set; }
    }
}
