﻿using System.Threading.Tasks;
using cadence.dotnet;
using cadence.dotnet.Cadence;
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

        public async Task RunWorkflow(TestParams testParams)
        {
            _correlationHandler.LogStart($"{GetType().Name}");

            var act = new ActivityOptions
            {
                RetryOptions = ActivityRetryOptions.Standard
            };

            for (var i = 0; i < testParams.ActivityExecutions; i++)
            {
                _correlationHandler.LogInformation($"Executing Activity [{i + 1}/{testParams.ActivityExecutions}]");
                await Workflow.NewActivityStub<ITestActivity>(act).RunActivity(testParams);
            }

            _correlationHandler.LogComplete($"{GetType().Name}");
        }
    }

    [WorkflowInterface]
    public interface ITestWorkflow : IWorkflow
    {
        [WorkflowMethod]
        public Task RunWorkflow(TestParams testParams);
    }
}