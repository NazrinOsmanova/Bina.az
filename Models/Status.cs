﻿using System;
using System.Collections.Generic;

namespace Proje1.Models
{
    public partial class Status
    {
        public Status()
        {
            Users = new HashSet<User>();
        }

        public int StatusId { get; set; }
        public string? StatusName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
