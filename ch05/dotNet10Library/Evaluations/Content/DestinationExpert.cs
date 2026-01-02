using System.Collections.Generic;

namespace dotNet10Library.Evaluations.Content
{
    /// <summary>
    /// Represents evaluations for destination expert content.
    /// </summary>
    public class DestinationExpert : IContentEvaluated
    {
        /// <summary>
        /// Gets or sets the list of evaluations for the destination expert.
        /// </summary>
        public List<Evaluation> Evaluations { get; set; } = [];
    }
}
