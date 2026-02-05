using System.Collections.Generic;
using MapperStudio.ViewModels;

namespace MapperStudio.Services;

public class DummyConnectionService : IConnectionService
{
    public List<string>? LoadSourceSchema(string connectionType, string endpoint)
    {
        // Return dummy schema based on connection type
        return connectionType switch
        {
            "REST API" => new List<string> { "user.id", "user.name", "user.email", "timestamp" },
            "SOAP" => new List<string> { "Envelope.Body.User.Id", "Envelope.Body.User.Name" },
            "Database" => new List<string> { "Id", "Name", "Email" },
            "CSV" => new List<string> { "col1", "col2", "col3" },
            "Excel" => new List<string> { "A1", "B1", "C1" },
            _ => new List<string>()
        };
    }

    public List<ColumnDefinition>? LoadTargetSchema(string systemType, string entityName)
    {
        // Return dummy columns
        return new List<ColumnDefinition>
        {
            new ColumnDefinition { Name = "Id", DataType = "int", Required = true },
            new ColumnDefinition { Name = "Name", DataType = "string", Required = false },
            new ColumnDefinition { Name = "Email", DataType = "string", Required = false }
        };
    }
}
