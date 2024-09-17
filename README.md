# YAML Anchor and Merge Key Expander

This C# console application expands YAML anchors and merge keys in OpenAPI (Swagger) specifications. It's designed to help prepare YAML files for tools like NSwag that don't support these YAML-specific features.

## Features

- Expands YAML anchors (`&anchor`, `*anchor`)
- Resolves merge keys (`<<`)
- Preserves the overall structure of the YAML file
- Works with OpenAPI (Swagger) specifications

## Prerequisites

- .NET Core 3.1 or later
- YamlDotNet NuGet package

## Installation

1. Clone this repository:
   ```
   git clone https://github.com/yourusername/yaml-expander.git
   ```
2. Navigate to the project directory:
   ```
   cd yaml-expander
   ```
3. Restore the NuGet packages:
   ```
   dotnet restore
   ```

## Usage

Run the application from the command line, providing input and output file paths:

```
dotnet run YamlExpander.dll <input_file.yaml> <output_file.yaml>
```

For example:
```
dotnet run YamlExpander.dll input.yaml output.yaml
```

This will read `input.yaml`, expand all anchors and merge keys, and write the result to `output.yaml`.

