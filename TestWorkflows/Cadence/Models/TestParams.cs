namespace TestWorkflows.Cadence.Models
{
    public class TestParams
    {
        /// <summary>
        /// Number of milliseconds to wait before proceeding
        /// </summary>
        public int WaitTimeMs { get; set; } = 5000;
        /// <summary>
        /// Percent chance to throw an exception.  Must be an integer from 0 to 100.
        /// </summary>
        public int ExceptionPercent { get; set; } = 50;
        /// <summary>
        /// Number of times to execute the activity
        /// </summary>
        public int ActivityExecutions { get; set; } = 1;
        /// <summary>
        /// Size in bytes of the data to be returned from the activity
        /// </summary>
        public int ReturnSize { get; set; } = 0;
    }
}
