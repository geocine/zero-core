﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zervo.Core.Models.Contracts;

namespace Zervo.Core.Models
{
    public class Customer : ObjectModel, IObjectModel
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
    }
}