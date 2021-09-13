using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neon.Cadence;
using WorkflowConcurrencyTest.Cadence.Activities;

namespace WorkflowConcurrencyTest.Cadence.Workflows
{
    [Workflow(AutoRegister = true)]
    public class TestWorkflow : WorkflowBase, ITestWorkflow
    {
        public async Task RunWorkflow()
        {
            Workflow.Logger.LogInfo($"Workflow [{Workflow.Execution.WorkflowId}] started");
            await Workflow.NewActivityStub<ITestActivity>().RunActivity();
            Workflow.Logger.LogInfo($"Workflow [{Workflow.Execution.WorkflowId}] ending");
        }
    }
    [WorkflowInterface]
    public interface ITestWorkflow : IWorkflow
    {
        [WorkflowMethod]
        public Task RunWorkflow();
    }
}
