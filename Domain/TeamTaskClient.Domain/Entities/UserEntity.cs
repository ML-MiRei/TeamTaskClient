﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Domain.Common;

namespace TeamTaskClient.Domain.Entities
{
    public class UserEntity : IBaseEntity, IExecutorProjectTask
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Tag { get; set; }
        public string Phone { get; set; }
    }
}