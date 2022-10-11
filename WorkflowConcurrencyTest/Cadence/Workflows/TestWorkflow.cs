using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neon.Cadence;
using Neon.Retry;
using WorkflowConcurrencyTest.Cadence.Activities;

namespace WorkflowConcurrencyTest.Cadence.Workflows
{
    [Workflow(AutoRegister = true)]
    public class TestWorkflow : WorkflowBase, ITestWorkflow
    {
        public async Task RunWorkflow(TestParams testParams)
        {
            Workflow.Logger.LogInfo($"Workflow [{Workflow.Execution.WorkflowId}] started");

            // Default linear policy
            var act = new ActivityOptions
            {
                RetryOptions = new RetryOptions(new LinearRetryPolicy()),
                //ScheduleToCloseTimeout = TimeSpan.FromSeconds(5),
                //StartToCloseTimeout = TimeSpan.FromSeconds(3),
            };

            await Workflow.NewActivityStub<ITestActivity>(act).RunActivity(testParams);
            Workflow.Logger.LogInfo($"Workflow [{Workflow.Execution.WorkflowId}] ending");
        }
    }
    [WorkflowInterface]
    public interface ITestWorkflow : IWorkflow
    {
        [WorkflowMethod]
        public Task RunWorkflow(TestParams testParams);
    }
}
