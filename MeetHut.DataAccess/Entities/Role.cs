﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetHut.DataAccess.Entities
{
    public class Role : IdentityRole<int>, IEntity
    {
        public Dictionary<string, string> GetKeyValuePairs()
        {
            return new()
            {
               
            };
        }
    }
}
