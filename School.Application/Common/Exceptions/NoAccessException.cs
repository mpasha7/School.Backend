using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Common.Exceptions
{
    public class NoAccessException : Exception
    {
        public NoAccessException(string name, int key) : base($"No access to '{name}' (id = {key}).")
        {
        }
    }
}
