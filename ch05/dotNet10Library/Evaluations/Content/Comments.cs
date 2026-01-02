using System.Collections.Generic;

namespace dotNet10Library.Evaluations.Content
{
    /// <summary>
    /// Represents evaluations for user comments.
    /// </summary>
    public class Comments : IContentEvaluated
    {
        /// <summary>
        /// Gets or sets the list of evaluations for the comments.
        /// </summary>
        public List<Evaluation> Evaluations { get; set; } = [];
    }
}
