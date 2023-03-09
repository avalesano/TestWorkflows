using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using cadence.dotnet;
using cadence.dotnet.Cadence;
using cadence.dotnet.Cadence.Workflow;
using cadence.dotnet.Interfaces;
using Neon.Cadence;
using TestWorkflows.Cadence.Activities;
using TestWorkflows.Cadence.Models;

namespace TestWorkflows.Cadence.Workflows
{
    [Workflow(AutoRegister = true)]
    public class TestWorkflow : WorkflowBase, ITestWorkflow
    {
        private readonly ICorrelationHandler _correlationHandler;

        public TestWorkflow()
        {
            _correlationHandler = new CorrelationHandler(this);
        }

        public async Task<WorkflowResult> RunWorkflow(TestParams testParams)
        {
            _correlationHandler.LogStart($"{GetType().Name}");

            var act = new ActivityOptions
            {
                RetryOptions = ActivityRetryOptions.Standard
            };

            if (await Workflow.NextRandomAsync(100) < testParams.WorkflowExceptionPercent)
            {
                throw new Exception("WORKFLOW EXCEPTION!");
            }

            for (var i = 0; i < testParams.ActivityExecutions; i++)
            {
                _correlationHandler.LogInformation($"Executing Activity [{i + 1}/{testParams.ActivityExecutions}]");
                await Workflow.NewActivityStub<ITestActivity>(act).RunActivity(testParams);
            }

            _correlationHandler.LogComplete($"{GetType().Name}");

            return new WorkflowResult
            {
                Steps = new List<WorkflowStep>
                {
                    new WorkflowStep
                    {
                        Success = true,
                        WorkflowStepName = "TestStep",
                        WorkflowStepDescription = "TestStepDescription",
                    }
                }
            };
        }
    }

    [WorkflowInterface]
    public interface ITestWorkflow : IWorkflow
    {
        [WorkflowMethod]
        public Task<WorkflowResult> RunWorkflow(TestParams testParams);
    }
}