﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Common.Exceptions
{
    public class NotContainsException : Exception
    {
        public NotContainsException(string containerName, int containerKey, string name, int key) 
            : base($"'{name}' (id = {key}) is not part of the '{containerName}' (id = {containerKey}).")
        {
        }
    }
}
