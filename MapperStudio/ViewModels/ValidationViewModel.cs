using System.Collections.ObjectModel;
using System.Linq;

namespace MapperStudio.ViewModels;

public sealed class ValidationViewModel : ViewModelBase
{
    private bool _hasErrors;
    private string _validationSummary = string.Empty;

    public ValidationViewModel()
    {
        ValidationItems = new ObservableCollection<ValidationItem>
        {
            new("Error", "Missing required mapping", "CustomerId is required but not mapped."),
            new("Warning", "Format mismatch", "CreatedDate uses a non-standard date format.")
        };

        UnmappedRequiredFields = new ObservableCollection<string>
        {
            "CustomerId",
            "CreatedDate"
        };

        UpdateValidationState();
    }

    public ObservableCollection<ValidationItem> ValidationItems { get; }

    public ObservableCollection<string> UnmappedRequiredFields { get; }

    public bool HasErrors
    {
        get => _hasErrors;
        private set
        {
            if (_hasErrors == value)
            {
                return;
            }

            _hasErrors = value;
            OnPropertyChanged();
        }
    }

    public string ValidationSummary
    {
        get => _validationSummary;
        private set
        {
            if (_validationSummary == value)
            {
                return;
            }

            _validationSummary = value;
            OnPropertyChanged();
        }
    }

    private void UpdateValidationState()
    {
        HasErrors = ValidationItems.Any(item => item.Severity == "Error");
        ValidationSummary = HasErrors
            ? "Resolve blocking errors before export."
            : "All required fields mapped.";
    }
}

public sealed record ValidationItem(string Severity, string Title, string Message);
