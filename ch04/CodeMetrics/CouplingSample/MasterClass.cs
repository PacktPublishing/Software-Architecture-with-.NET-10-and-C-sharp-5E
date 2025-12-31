using CodeMetricsBadCode.CouplingSample.Execution;

namespace CodeMetricsBadCode.CouplingSample
{
    class MasterClass
    {
        private ExecutionTypeA executionTypeA = new ExecutionTypeA();

        private ExecutionTypeB executionTypeB = new ExecutionTypeB();

        private ExecutionTypeC executionTypeC = new ExecutionTypeC();
        public void ProcessData()
        {
            executionTypeA.ExecuteTypeA();
            executionTypeB.ExecuteTypeB();
            executionTypeC.ExecuteTypeC();

        }
    }
}
