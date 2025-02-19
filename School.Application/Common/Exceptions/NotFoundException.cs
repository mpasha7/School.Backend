﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, int key) : base($"Entity '{name}' (id = {key}) not found.")
        {
        }
    }
}
