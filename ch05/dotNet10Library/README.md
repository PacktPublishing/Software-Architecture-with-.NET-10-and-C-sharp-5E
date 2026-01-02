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

### From NuGet (Once Published)

```bash
dotnet add package dotNet10Library
```

Or via Package Manager Console:

```powershell
Install-Package dotNet10Library
```

### From Project Reference

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

## Publishing as a NuGet Package

### Prerequisites

1. **NuGet Account**: Create a free account at [nuget.org](https://www.nuget.org/)
2. **API Key**: Generate an API key from your NuGet account settings
3. **.NET SDK**: Ensure .NET 10 SDK is installed

### Step 1: Update Package Metadata

Edit `dotNet10Library.csproj` and update the following properties:

```xml
<PropertyGroup>
    <Version>1.0.0</Version>
    <Authors>Your Name</Authors>
    <Company>Your Company</Company>
    <Description>Your library description</Description>
    <PackageLicenseExpression>MIT</PackageLicenseExpression>
</PropertyGroup>
```

### Step 2: Build the Package

Navigate to the project directory and run:

```bash
cd dotNet10Library
dotnet pack --configuration Release
```

This creates a `.nupkg` file in `bin/Release/`.

### Step 3: Test Locally (Optional)

Before publishing, test the package locally:

```bash
# Create a local NuGet source
mkdir C:\LocalNuGet
dotnet nuget add source C:\LocalNuGet --name LocalPackages

# Copy the package to local source
copy bin\Release\dotNet10Library.1.0.0.nupkg C:\LocalNuGet\

# Test in another project
dotnet add package dotNet10Library --source LocalPackages
```

### Step 4: Publish to NuGet.org

#### Option A: Using .NET CLI (Recommended)

```bash
dotnet nuget push bin/Release/dotNet10Library.1.0.0.nupkg --api-key YOUR_API_KEY --source https://api.nuget.org/v3/index.json
```

#### Option B: Manual Upload

1. Go to [nuget.org/packages/manage/upload](https://www.nuget.org/packages/manage/upload)
2. Upload your `.nupkg` file
3. Follow the verification steps

### Step 5: Verify Publication

After publishing, your package will be available at:
```
https://www.nuget.org/packages/dotNet10Library
```

Note: It may take a few minutes for the package to be indexed and searchable.

### Publishing Updates

To publish a new version:

1. Update the `<Version>` in the `.csproj` file (e.g., `1.0.1`, `1.1.0`)
2. Run `dotnet pack --configuration Release`
3. Push the new package: `dotnet nuget push bin/Release/dotNet10Library.1.0.1.nupkg --api-key YOUR_API_KEY --source https://api.nuget.org/v3/index.json`

### Best Practices

- **Semantic Versioning**: Follow [SemVer](https://semver.org/) (MAJOR.MINOR.PATCH)
  - MAJOR: Breaking changes
  - MINOR: New features (backward compatible)
  - PATCH: Bug fixes (backward compatible)
- **Release Notes**: Include a `CHANGELOG.md` file to document changes
- **Symbol Packages**: Consider publishing symbol packages for debugging:
  ```bash
  dotnet pack --configuration Release --include-symbols --include-source
  ```
- **CI/CD**: Automate publishing using GitHub Actions or Azure DevOps

### GitHub Actions Example

Create `.github/workflows/publish.yml`:

```yaml
name: Publish NuGet Package

on:
  release:
    types: [published]

jobs:
  publish:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v3
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: '10.0.x'
    
    - name: Restore dependencies
      run: dotnet restore
    
    - name: Build
      run: dotnet build --configuration Release --no-restore
    
    - name: Pack
      run: dotnet pack --configuration Release --no-build --output ./nupkg
    
    - name: Push to NuGet
      run: dotnet nuget push ./nupkg/*.nupkg --api-key ${{secrets.NUGET_API_KEY}} --source https://api.nuget.org/v3/index.json
```

### Troubleshooting

**Package ID Conflict**: If the package name is taken, choose a unique name like `YourCompany.dotNet10Library`

**API Key Issues**: Ensure your API key has the correct permissions and hasn't expired

**Version Already Exists**: You cannot republish the same version. Increment the version number.

**Missing Dependencies**: All dependencies will be automatically included based on your project references.

## License

Please refer to the main repository license.

## Contributing

Contributions are welcome! Please feel free to submit pull requests or open issues for bugs and feature requests.

## Related Projects

This library is part of the [Software Architecture with .NET 10 and C# 5E](https://github.com/PacktPublishing/Software-Architecture-with-.NET-10-and-C-sharp-5E) book examples.
