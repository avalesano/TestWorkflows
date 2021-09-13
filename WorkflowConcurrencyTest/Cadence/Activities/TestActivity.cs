using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neon.Cadence;

namespace WorkflowConcurrencyTest.Cadence.Activities
{
    [Activity(AutoRegister = true)]
    public class TestActivity : ActivityBase, ITestActivity
    {
        public async Task RunActivity()
        {
            Activity.Logger.LogInfo($"Activity [{Activity.Task.ActivityId}] started");
            await Task.Delay(5000);
            Random rng = new Random();
            if (rng.Next(0, 2) == 1)
            {
                throw new Exception("TEST EXCEPTION!");
            }
            Activity.Logger.LogInfo($"Activity [{Activity.Task.ActivityId}] ending");
        }
    }

    public interface ITestActivity : IActivity
    {
        [ActivityMethod]
        public Task RunActivity();
    }
}