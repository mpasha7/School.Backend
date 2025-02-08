using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Common.Exceptions
{
    public class ActionAlreadyCompletedException : Exception
    {
        public ActionAlreadyCompletedException(string ownerName, string ownerKey, string action, string name, int key)
            : base($"This {ownerName} (guid = {ownerKey}) {action} this {name} (id = {key})")
        {
        }
    }
}
