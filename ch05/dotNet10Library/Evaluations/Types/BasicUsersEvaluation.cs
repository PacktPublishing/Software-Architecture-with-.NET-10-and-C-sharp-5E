using System;
using System.Collections.Generic;
using System.Text;

namespace dotNet10Library.Evaluations.Types
{
    /// <summary>
    /// Evaluation type for basic users that returns the grade without any modification.
    /// </summary>
    public class BasicUsersEvaluation: Evaluation
    { 
        /// <summary>
        /// Calculates the grade for basic users.
        /// </summary>
        /// <returns>The unmodified grade value.</returns>
        public override double CalculateGrade()
        {
            return Grade;
        }
    }
}
