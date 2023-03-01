using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWorkflows.Cadence.Models
{
    public class LargeHistoryTestParams : TestParams
    {
        public new int WaitTimeMs { get; set; } = 500;
        public new int ExceptionPercent { get; set; } = 0;
        public new int ReturnSize { get; set; } = 512;
    }
}
