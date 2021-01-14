using System;
using $ext_projectname$.Common;

namespace $safeprojectname$
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime GetUtcTime()
            => DateTime.UtcNow;
    }
}
