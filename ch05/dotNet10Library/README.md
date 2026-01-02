# dotNet10Library

A .NET 10 library for managing and calculating evaluations for various types of content with support for different user types and evaluation strategies.

## Overview

This library provides a flexible evaluation system that allows you to:
- Manage evaluations for different types of content (cities, packages, comments, destination experts)
- Support different user types with different calculation strategies (basic users, premium users)
- Calculate average grades across multiple evaluations

## Features

- **Generic Evaluation Service**: Type-safe service for managing content evaluations
- **Flexible Content Types**: Support for multiple content types through the `IContentEvaluated` interface
- **User-Specific Calculations**: Different grade calculation strategies based on user type
- **Extensible Architecture**: Easy to add new content types and evaluation strategies

## Installation

Add a reference to this library in your project:

```xml
<ItemGroup>
  <ProjectReference Include="path\to\dotNet10Library\dotNet10Library.csproj" />
</ItemGroup>
```

## Usage

### Basic Example

```csharp
using dotNet10Library;
using dotNet10Library.Evaluations.Content;
using dotNet10Library.Evaluations.Types;

// Create an evaluation service for city evaluations
var service = new EvaluationService<CityEvaluation>();

// Add basic user evaluations
service.Content.Evaluations.Add(new BasicUsersEvaluation
{
    Id = 1,
    User = "John Doe",
    Description = "Great city to visit!",
    Grade = 8
});

// Add premium user evaluations (with 1.2x multiplier)
service.Content.Evaluations.Add(new PrimeUsersEvaluation
{
    Id = 2,
    User = "Jane Smith",
    Description = "Amazing experience!",
    Grade = 10
});

// Calculate the average grade
double average = service.CalculateEvaluationAverage();
Console.WriteLine($"Average grade: {average}");
```

### Working with Different Content Types

```csharp
// Package evaluations
var packageService = new EvaluationService<Package>();

// Comments evaluations
var commentsService = new EvaluationService<Comments>();

// Destination expert evaluations
var expertService = new EvaluationService<DestinationExpert>();
```

## Architecture

### Core Components

#### `IContentEvaluated`
Interface that defines content that can be evaluated. All content types must implement this interface.

#### `EvaluationService<T>`
Generic service class that manages evaluations for any content type implementing `IContentEvaluated`.

#### `Evaluation`
Base class for all evaluation types. Contains common properties like:
- `Id`: Unique identifier
- `User`: User who submitted the evaluation
- `Description`: Evaluation description
- `Grade`: Raw grade value

### Content Types

- **`CityEvaluation`**: Evaluations for city destinations
- **`Package`**: Evaluations for travel packages
- **`Comments`**: Evaluations for user comments
- **`DestinationExpert`**: Evaluations for destination expert content

### Evaluation Types

#### `BasicUsersEvaluation`
Standard evaluation for basic users. Returns the grade without modification.

```csharp
CalculateGrade() => Grade
```

#### `PrimeUsersEvaluation`
Enhanced evaluation for premium users. Applies a 1.2x multiplier to grades.

```csharp
CalculateGrade() => Grade * 1.2
```

## Extending the Library

### Adding a New Content Type

```csharp
using System.Collections.Generic;
using dotNet10Library.Evaluations;

namespace YourNamespace
{
    public class YourCustomContent : IContentEvaluated
    {
        public List<Evaluation> Evaluations { get; set; } = [];
    }
}
```

### Adding a New Evaluation Strategy

```csharp
using dotNet10Library.Evaluations;

namespace YourNamespace
{
    public class CustomUserEvaluation : Evaluation
    {
        public override double CalculateGrade()
        {
            // Implement your custom calculation logic
            return Grade * 1.5; // Example: 50% bonus
        }
    }
}
```

## Project Structure

```
dotNet10Library/
??? IContentEvaluated.cs           # Core interface
??? EvaluationService.cs           # Generic service class
??? Evaluations/
?   ??? Evaluation.cs              # Base evaluation class
?   ??? Types/
?   ?   ??? BasicUsersEvaluation.cs     # Basic user strategy
?   ?   ??? PrimeUsersEvaluation.cs     # Premium user strategy
?   ??? Content/
?       ??? CityEvaluation.cs      # City content type
?       ??? Package.cs             # Package content type
?       ??? Comments.cs            # Comments content type
?       ??? DestinationExpert.cs   # Expert content type
??? README.md
```

## Requirements

- .NET 10.0 or higher
- Nullable reference types enabled

## License

Please refer to the main repository license.

## Contributing

Contributions are welcome! Please feel free to submit pull requests or open issues for bugs and feature requests.

## Related Projects

This library is part of the [Software Architecture with .NET 10 and C# 5E](https://github.com/PacktPublishing/Software-Architecture-with-.NET-10-and-C-sharp-5E) book examples.
