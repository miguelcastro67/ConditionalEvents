using System;
using System.Collections.Generic;
using System.Linq;

namespace ConditionalEvents.Lib
{
    public class ReportValueEventArgs : EventArgs
    {
        public ReportValueEventArgs(int value)
        {
            Value = value;
        }

        public int Value { get; set; }
    }
}
