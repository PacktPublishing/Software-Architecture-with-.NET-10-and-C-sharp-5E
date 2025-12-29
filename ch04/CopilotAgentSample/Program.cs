using System;
using System.Collections.Generic;

namespace CopilotAgentSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Copilot Agent Sample: Code Refactoring Demo ===\n");

            // Demonstrate the refactored code
            Console.WriteLine("Running refactored code example:");
            RunRefactoredExample();
        }

        static void RunRefactoredExample()
        {
            var dataProcessor = new DataProcessor();
            var logger = new ConsoleLogger();
            var fileService = new FileService();

            var orchestrator = new DataProcessingOrchestrator(dataProcessor, logger, fileService);

            var testData = new List<string> { "apple", "banana", "cherry", "date", "elderberry" };

            try
            {
                // Add test data to processor for searching
                foreach (var item in testData)
                {
                    dataProcessor.AddItem(item);
                }

                // Test search functionality before processing
                string searchResult = dataProcessor.FindItem("banana");
                Console.WriteLine($"✓ Search found: {searchResult}");

                // Process and save data
                orchestrator.ProcessAndSave(testData, "refactored_output.txt");
                Console.WriteLine("✓ Data processed successfully using best practices");

                // Test calculation with proper error handling
                int divisionResult = MathHelper.SafeDivide(10, 2);
                Console.WriteLine($"✓ Division result (10/2): {divisionResult}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error occurred: {ex.Message}");
            }
        }
    }

    // REFACTORED CODE: Following SOLID principles and best practices

    /// <summary>
    /// Handles data processing operations with proper encapsulation
    /// </summary>
    public class DataProcessor
    {
        private readonly List<string> _data;

        public DataProcessor()
        {
            _data = new List<string>();
        }

        public IReadOnlyList<string> Data => _data.AsReadOnly();

        public int ItemCount => _data.Count;

        /// <summary>
        /// Adds an item to the collection with validation
        /// </summary>
        public void AddItem(string item)
        {
            if (string.IsNullOrWhiteSpace(item))
            {
                throw new ArgumentException("Item cannot be null or empty", nameof(item));
            }

            _data.Add(item);
        }

        /// <summary>
        /// Finds an item in the collection efficiently
        /// </summary>
        public string FindItem(string searchValue)
        {
            if (string.IsNullOrWhiteSpace(searchValue))
            {
                throw new ArgumentException("Search value cannot be null or empty", nameof(searchValue));
            }

            // Efficient single-pass search with proper null handling
            return _data.Find(item => 
                item?.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ?? false) 
                ?? string.Empty;
        }

        /// <summary>
        /// Transforms all items to uppercase
        /// </summary>
        public List<string> TransformToUpperCase(IEnumerable<string> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            var result = new List<string>();
            foreach (var item in items)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    result.Add(item.ToUpper());
                }
            }

            return result;
        }
    }

    /// <summary>
    /// Interface for logging operations (Dependency Inversion Principle)
    /// </summary>
    public interface ILogger
    {
        void Log(string message);
    }

    /// <summary>
    /// Console implementation of logger
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"[LOG] {message}");
        }
    }

    /// <summary>
    /// Handles file operations (Single Responsibility Principle)
    /// </summary>
    public class FileService
    {
        public void SaveToFile(string filename, IEnumerable<string> data)
        {
            if (string.IsNullOrWhiteSpace(filename))
            {
                throw new ArgumentException("Filename cannot be null or empty", nameof(filename));
            }

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            try
            {
                System.IO.File.WriteAllLines(filename, data);
            }
            catch (System.IO.IOException ex)
            {
                throw new InvalidOperationException($"Failed to save file: {filename}", ex);
            }
        }
    }

    /// <summary>
    /// Orchestrates the data processing workflow (Single Responsibility Principle)
    /// </summary>
    public class DataProcessingOrchestrator
    {
        private readonly DataProcessor _processor;
        private readonly ILogger _logger;
        private readonly FileService _fileService;

        public DataProcessingOrchestrator(DataProcessor processor, ILogger logger, FileService fileService)
        {
            _processor = processor ?? throw new ArgumentNullException(nameof(processor));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        }

        public void ProcessAndSave(List<string> items, string filename)
        {
            // Validation
            if (items == null || items.Count == 0)
            {
                throw new ArgumentException("Items list cannot be null or empty", nameof(items));
            }

            // Processing
            var transformedData = _processor.TransformToUpperCase(items);
            
            // Logging
            _logger.Log($"Processed {items.Count} items");

            // Persistence
            _fileService.SaveToFile(filename, transformedData);
            _logger.Log($"Saved data to {filename}");
        }
    }

    /// <summary>
    /// Utility class for mathematical operations with proper error handling
    /// </summary>
    public static class MathHelper
    {
        public static int SafeDivide(int dividend, int divisor)
        {
            if (divisor == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero");
            }

            return dividend / divisor;
        }
    }
}
