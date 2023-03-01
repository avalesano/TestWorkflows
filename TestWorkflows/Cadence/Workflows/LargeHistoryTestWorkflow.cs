using cadence.dotnet;
using cadence.dotnet.Cadence;
using cadence.dotnet.Interfaces;
using Neon.Cadence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWorkflows.Cadence.Activities;
using TestWorkflows.Cadence.Models;

namespace TestWorkflows.Cadence.Workflows
{
    [Workflow(AutoRegister = true)]
    public class LargeHistoryTestWorkflow : WorkflowBase, ILargeHistoryTestWorkflow
    {
        private readonly ICorrelationHandler _correlationHandler;

        public LargeHistoryTestWorkflow()
        {
            _correlationHandler = new CorrelationHandler(this);
        }

        public async Task RunWorkflow(LargeHistoryTestParams testParams)
        {
            _correlationHandler.LogStart($"{GetType().Name}");

            var act = new ActivityOptions
            {
                RetryOptions = ActivityRetryOptions.Standard
            };

            for (var i = 0; i < testParams.ActivityExecutions; i++)
            {
                _correlationHandler.LogInformation($"Executing Activity [{i + 1}/{testParams.ActivityExecutions}]");
                await Workflow.NewActivityStub<IDataReturnActivity>(act).RunActivity(testParams);
            }

            _correlationHandler.LogComplete($"{GetType().Name}");
        }
    }
    [WorkflowInterface]
    public interface ILargeHistoryTestWorkflow : IWorkflow
    {
        [WorkflowMethod]
        public Task RunWorkflow(LargeHistoryTestParams testParams);
    }
}
