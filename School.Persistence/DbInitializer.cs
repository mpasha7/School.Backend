using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(SchoolDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
