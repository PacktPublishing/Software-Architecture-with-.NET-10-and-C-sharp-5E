using System;
using System.Collections.Generic;
using System.Text;

namespace dotNet10Library.Evaluations.Types
{
    /// <summary>
    /// Evaluation type for premium users that applies a 1.2x multiplier to the grade.
    /// </summary>
    public class PrimeUsersEvaluation : Evaluation
    {
        /// <summary>
        /// Calculates the grade for premium users with a 20% bonus.
        /// </summary>
        /// <returns>The grade multiplied by 1.2.</returns>
        public override double CalculateGrade()
        {
            return Grade * 1.2;
        }
    }
}
