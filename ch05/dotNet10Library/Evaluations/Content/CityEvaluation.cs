using System.Collections.Generic;

namespace dotNet10Library.Evaluations.Content
{
    /// <summary>
    /// Represents evaluations for a city destination.
    /// </summary>
    public class CityEvaluation : IContentEvaluated
    {
        /// <summary>
        /// Gets or sets the list of evaluations for the city.
        /// </summary>
        public List<Evaluation> Evaluations { get; set; } = new List<Evaluation>();
    }
}
