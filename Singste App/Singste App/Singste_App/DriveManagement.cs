using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SingsteApp
{
    abstract class DriveManagement
    {
        public virtual bool createDatabase(User current)
        {
            return false;
        }
        public virtual User getDatabase()
        {
            return null;
        }
    }
}