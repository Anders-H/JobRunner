using System;
using System.Collections.Generic;

namespace JobRunner.ObjectModel
{
    public class TimeSpanList : List<TimeSpan>
    {
        public TimeSpanList()
        {
            var timeout = new TimeSpan(0, 0, 0, 30, 0);
            for (var i = 0; i < 30; i++)
            {
                Add(timeout);
                if (i < 29)
                    timeout = timeout.Add(new TimeSpan(0, 0, 0, 30, 0));
            }
            for (var i = 0; i < 30; i++)
            {
                timeout = timeout.Add(new TimeSpan(0, 0, 1, 0, 0));
                Add(timeout);
            }
            timeout = new TimeSpan(0, 1, 0, 0, 0);
            for (var i = 0; i < 20; i++)
            {
                Add(timeout);
                timeout = timeout.Add(new TimeSpan(0, 1, 0, 0, 0));
            }
        }
    }
}