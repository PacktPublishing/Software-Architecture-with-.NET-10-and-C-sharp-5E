using dotNet10Library.Evaluations;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotNet10Library
{
    /// <summary>
    /// Interface for content that can be evaluated with multiple evaluations.
    /// </summary>
    public interface IContentEvaluated
    {
        /// <summary>
        /// Gets or sets the list of evaluations for the content.
        /// </summary>
        List<Evaluation> Evaluations { get; set; }
    }
}
