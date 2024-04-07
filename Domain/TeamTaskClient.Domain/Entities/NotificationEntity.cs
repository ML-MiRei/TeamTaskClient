﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Common;
using TeamTaskClient.Domain.Enums;

namespace TeamTaskClient.Domain.Entities
{
    public class NotificationEntity : IBaseEntity
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
    }
}
