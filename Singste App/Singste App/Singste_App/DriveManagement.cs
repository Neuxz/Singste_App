using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Singste_App
{
    public abstract class DriveManagement
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