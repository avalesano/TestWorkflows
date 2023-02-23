using System;
using System.Threading.Tasks;
using Neon.Cadence;

namespace TestWorkflows.Cadence.Activities
{
    [Activity(AutoRegister = true)]
    public class TestActivity : ActivityBase, ITestActivity
    {
        /// <summary>
        /// An activity which waits the specified amount of time, and then has a specified percent chance to throw an exception before returning
        /// </summary>
        /// <param name="waitTimeMs">Number of milliseconds to wait before proceeding</param>
        /// <param name="exceptionPercent">Percent chance to throw an exception.  Must be an integer from 0 to 100.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task RunActivity(TestParams testParams)
        {
            Activity.Logger.LogInfo($"TestActivity [{Activity.Task.ActivityId}] started");
            Activity.Logger.LogInfo($"Waiting for {testParams.WaitTimeMs}ms");
            await Task.Delay(testParams.WaitTimeMs);
            Random rng = new Random();
            if (rng.Next(100) < testParams.ExceptionPercent)
            {
                throw new Exception("TEST EXCEPTION!");
            }
            Activity.Logger.LogInfo($"TestActivity [{Activity.Task.ActivityId}] ending");
        }
    }

    public interface ITestActivity : IActivity
    {
        [ActivityMethod]
        public Task RunActivity(TestParams testParams);
    }
}