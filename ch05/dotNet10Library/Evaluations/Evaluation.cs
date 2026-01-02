using System;
using System.Collections.Generic;
using System.Text;

namespace dotNet10Library.Evaluations
{
    /// <summary>
    /// Base class representing an evaluation with user information and grade.
    /// </summary>
    public class Evaluation
    {
        /// <summary>
        /// Gets or sets the unique identifier for the evaluation.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Gets or sets the description of the evaluation.
        /// </summary>
        public string Description { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the user who submitted the evaluation.
        /// </summary>
        public string User { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the grade value for the evaluation.
        /// </summary>
        public int Grade { get; set; }

        /// <summary>
        /// Calculates the final grade for the evaluation.
        /// </summary>
        /// <returns>The calculated grade as a double. Base implementation returns 0.</returns>
        public virtual double CalculateGrade()
        {
            return 0;
        }
    }
}
