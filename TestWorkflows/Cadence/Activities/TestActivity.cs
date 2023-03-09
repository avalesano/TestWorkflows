using System;
using System.Threading.Tasks;
using cadence.dotnet;
using Neon.Cadence;
using TestWorkflows.Cadence.Models;

namespace TestWorkflows.Cadence.Activities
{
    [Activity(AutoRegister = true)]
    public class TestActivity : ActivityBase, ITestActivity
    {
        private readonly CorrelationHandler _correlationHandler;

        public TestActivity()
        {
            _correlationHandler = new CorrelationHandler(this);
        }

        /// <summary>
        /// An activity which waits the specified amount of time, and then has a specified percent chance to throw an exception before returning
        /// </summary>
        /// <param name="testParams"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task RunActivity(TestParams testParams)
        {
            _correlationHandler.LogStart(GetType().Name);
            _correlationHandler.LogInformation($"Waiting for {testParams.WaitTimeMs}ms");
            await Task.Delay(testParams.WaitTimeMs);
            var rng = new Random();
            if (rng.Next(100) < testParams.ExceptionPercent)
            {
                throw new Exception("ACTIVITY EXCEPTION!");
            }
            _correlationHandler.LogComplete(GetType().Name);
        }
    }

    public interface ITestActivity : IActivity
    {
        [ActivityMethod]
        public Task RunActivity(TestParams testParams);
    }
}