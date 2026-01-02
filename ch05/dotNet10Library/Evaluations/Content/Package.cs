using System.Collections.Generic;

namespace dotNet10Library.Evaluations.Content
{
    /// <summary>
    /// Represents evaluations for a travel package.
    /// </summary>
    public class Package : IContentEvaluated
    {
        /// <summary>
        /// Gets or sets the list of evaluations for the package.
        /// </summary>
        public List<Evaluation> Evaluations { get; set; } = [];
    }
}
