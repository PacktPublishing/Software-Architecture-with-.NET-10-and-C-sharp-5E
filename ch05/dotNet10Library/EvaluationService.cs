using System.Linq;

namespace dotNet10Library
{
    /// <summary>
    /// Service class for managing and calculating evaluations for content.
    /// </summary>
    /// <typeparam name="T">The type of content that implements IContentEvaluated.</typeparam>
    public class EvaluationService<T> where T : IContentEvaluated, new()
    {
        /// <summary>
        /// Gets or sets the content being evaluated.
        /// </summary>
        public T Content { get; set; }

        /// <summary>
        /// Initializes a new instance of the EvaluationService class.
        /// </summary>
        public EvaluationService()
        {
            Content = new T();
        }

        /// <summary>
        /// Calculates the average grade of all evaluations for the content.
        /// </summary>
        /// <returns>The average grade as a double.</returns>
        public double CalculateEvaluationAverage() =>
            Content.Evaluations
                .Select(x => x.CalculateGrade())
                .Average();
    }
}
