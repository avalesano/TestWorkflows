using System;
using System.Text;
using System.Threading.Tasks;
using cadence.dotnet;
using Neon.Cadence;
using TestWorkflows.Cadence.Models;

namespace TestWorkflows.Cadence.Activities
{
    [Activity(AutoRegister = true)]
    public class DataReturnActivity : ActivityBase, IDataReturnActivity
    {
        private readonly CorrelationHandler _correlationHandler;

        public DataReturnActivity()
        {
            _correlationHandler = new CorrelationHandler(this);
        }

        /// <summary>
        /// An activity which waits the specified amount of time, and then has a specified percent chance to throw an exception before returning
        /// </summary>
        /// <param name="testParams"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ActivityResult> RunActivity(TestParams testParams)
        {
            Activity.Logger.LogInfo($"{GetType().Name} [{Activity.Task.ActivityId}] started");
            Activity.Logger.LogInfo($"Waiting for {testParams.WaitTimeMs}ms");
            await Task.Delay(testParams.WaitTimeMs);
            var rng = new Random();
            if (rng.Next(100) < testParams.ExceptionPercent)
            {
                throw new Exception("TEST EXCEPTION!");
            }

            var returnData = new byte[testParams.ReturnSize];
            rng.NextBytes(returnData);
            var result = new ActivityResult
            {
                DataBytes = returnData
            };
            Activity.Logger.LogInfo($"{GetType().Name} [{Activity.Task.ActivityId}] ending");
            return result;
        }
    }

    public interface IDataReturnActivity : IActivity
    {
        [ActivityMethod]
        public Task<ActivityResult> RunActivity(TestParams testParams);
    }
}