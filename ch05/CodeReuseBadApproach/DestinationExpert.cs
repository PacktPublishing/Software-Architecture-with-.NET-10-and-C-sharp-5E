using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeReuseBadApproach
{
    public class DestinationExpert
    {
        public List<Evaluation> Evaluations { get; set; } = [];

        public double CalculateEvaluationAverage()
        {
            if (Evaluations == null || Evaluations.Count == 0)
            {
                return 0;
            }
            double total = 0;
            foreach (var evaluation in Evaluations)
            {
                total += evaluation.Grade;
            }
            return total / Evaluations.Count;
        }
    }
}