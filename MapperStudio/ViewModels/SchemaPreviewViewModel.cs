using System.Collections.ObjectModel;
using MapperStudio.Models;

namespace MapperStudio.ViewModels;

public sealed class SchemaPreviewViewModel : ViewModelBase
{
    public SchemaPreviewViewModel()
    {
        SourceSchema = new ObservableCollection<SchemaNode>
        {
            new SchemaNode("Customers")
            {
                Children =
                {
                    new SchemaNode("CustomerId"),
                    new SchemaNode("FirstName"),
                    new SchemaNode("LastName"),
                    new SchemaNode("Email")
                }
            }
        };

        TargetSchema = new ObservableCollection<SchemaNode>
        {
            new SchemaNode("Customer")
            {
                Children =
                {
                    new SchemaNode("CustomerId"),
                    new SchemaNode("FullName"),
                    new SchemaNode("EmailAddress"),
                    new SchemaNode("CreatedDate")
                }
            }
        };

        SelectedFieldMetadata = new ObservableCollection<FieldMetadata>
        {
            new FieldMetadata("Type", "nvarchar(255)"),
            new FieldMetadata("Nullable", "False"),
            new FieldMetadata("Description", "Primary identifier")
        };
    }

    public ObservableCollection<SchemaNode> SourceSchema { get; }

    public ObservableCollection<SchemaNode> TargetSchema { get; }

    public ObservableCollection<FieldMetadata> SelectedFieldMetadata { get; }
}
