using System;

namespace CodeMetricsBadCode
{
    class Logger
    {
        internal static void GenerateLog(Exception err)
        {
            Console.WriteLine(err.ToString());
        }
    }
}
