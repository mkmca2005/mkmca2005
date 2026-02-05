using System.Collections.ObjectModel;

namespace MapperStudio.Models;

public sealed record MappingSummary(
    string Title,
    string Description,
    string RecordCount,
    string SourceSystem,
    string SourceType,
    string TargetSystem,
    string TargetType,
    string Entity,
    string Status,
    string Version,
    string UpdatedOn,
    string UpdatedBy);

public sealed class SchemaNode
{
    public SchemaNode(string name)
    {
        Name = name;
        Children = new ObservableCollection<SchemaNode>();
    }

    public string Name { get; }

    public ObservableCollection<SchemaNode> Children { get; }
}

public sealed record FieldMetadata(string Key, string Value);

public sealed record MappingFieldStatus(string Label, string Severity);

public sealed record TargetColumn(string Name, string DataType, string Required);

public sealed class MappingExport
{
    public string MappingTitle { get; init; } = string.Empty;
    public string Version { get; init; } = "1.0";
    public SystemInfo Source { get; init; } = new();
    public SystemInfo Target { get; init; } = new();
    public ObservableCollection<FieldMapping> Mappings { get; init; } = new();
}

public sealed class SystemInfo
{
    public string SystemType { get; init; } = string.Empty;
}

public sealed class FieldMapping
{
    public string TargetField { get; init; } = string.Empty;
    public string SourceField { get; init; } = string.Empty;
    public string Strategy { get; init; } = string.Empty;
}
