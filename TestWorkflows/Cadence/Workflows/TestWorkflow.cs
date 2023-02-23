using System.Threading.Tasks;
using cadence.dotnet.Cadence;
using Neon.Cadence;
using TestWorkflows.Cadence.Activities;

namespace TestWorkflows.Cadence.Workflows
{
    [Workflow(AutoRegister = true)]
    public class TestWorkflow : WorkflowBase, ITestWorkflow
    {
        public async Task RunWorkflow(TestParams testParams)
        {
            Workflow.Logger.LogInfo($"Workflow [{Workflow.Execution.WorkflowId}] started");

            var act = new ActivityOptions
            {
                RetryOptions = ActivityRetryOptions.Standard
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
