using System.Collections.ObjectModel;

namespace MapperStudio.ViewModels;

public sealed class MappingEditorViewModel : ViewModelBase
{
    public MappingEditorViewModel(MainViewModel mainViewModel)
    {
        SourceFields = new ObservableCollection<string>
        {
            "user.id",
            "user.name",
            "user.email",
            "timestamp",
            "user.preferences.theme"
        };

        Strategies = new ObservableCollection<string>
        {
            "Direct",
            "Ignore",
            "Default",
            "ValueMap",
            "Transform",
            "DateFormat",
            "Lookup"
        };

        MappingRows = new ObservableCollection<MappingRowViewModel>
        {
            new MappingRowViewModel("ExternalId", "String(max)", SourceFields[0], Strategies[0], "Valid", "Success", mainViewModel),
            new MappingRowViewModel("DisplayName", "String(max)", SourceFields[1], Strategies[0], "Valid", "Success", mainViewModel),
            new MappingRowViewModel("PrimaryEmail", "String(max)", "-- Unmapped --", Strategies[0], "Required", "Error", mainViewModel),
            new MappingRowViewModel("LastModified", "String(max)", SourceFields[3], Strategies[0], "Warning", "Warning", mainViewModel),
            new MappingRowViewModel("SystemRole", "String(max)", SourceFields[4], Strategies[6], "Valid", "Success", mainViewModel),
            new MappingRowViewModel("CreatedBy", "String(max)", "-- Unmapped --", Strategies[3], "Valid", "Success", mainViewModel)
        };
    }

    public ObservableCollection<string> SourceFields { get; }

    public ObservableCollection<string> Strategies { get; }

    public ObservableCollection<MappingRowViewModel> MappingRows { get; }
}

public sealed class MappingRowViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;
    private string _targetField;
    private string _targetType;
    private string _selectedSourceField;
    private string _selectedStrategy;
    private string _statusLabel;
    private string _statusSeverity;

    public MappingRowViewModel(string targetField, string targetType, string selectedSourceField, string selectedStrategy, string statusLabel, string statusSeverity, MainViewModel mainViewModel)
    {
        _targetField = targetField;
        _targetType = targetType;
        _selectedSourceField = selectedSourceField;
        _selectedStrategy = selectedStrategy;
        _statusLabel = statusLabel;
        _statusSeverity = statusSeverity;
        _mainViewModel = mainViewModel;
        ConfigureCommand = new RelayCommand(_ => _mainViewModel.CurrentView = _mainViewModel.StrategyConfigViewModel);
    }

    public string TargetField
    {
        get => _targetField;
        set
        {
            if (_targetField == value)
            {
                return;
            }

            _targetField = value;
            OnPropertyChanged();
        }
    }

    public string TargetType
    {
        get => _targetType;
        set
        {
            if (_targetType == value)
            {
                return;
            }

            _targetType = value;
            OnPropertyChanged();
        }
    }

    public string SelectedSourceField
    {
        get => _selectedSourceField;
        set
        {
            if (_selectedSourceField == value)
            {
                return;
            }

            _selectedSourceField = value;
            OnPropertyChanged();
        }
    }

    public string SelectedStrategy
    {
        get => _selectedStrategy;
        set
        {
            if (_selectedStrategy == value)
            {
                return;
            }

            _selectedStrategy = value;
            OnPropertyChanged();
        }
    }

    public string StatusLabel
    {
        get => _statusLabel;
        set
        {
            if (_statusLabel == value)
            {
                return;
            }

            _statusLabel = value;
            OnPropertyChanged();
        }
    }

    public string StatusSeverity
    {
        get => _statusSeverity;
        set
        {
            if (_statusSeverity == value)
            {
                return;
            }

            _statusSeverity = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand ConfigureCommand { get; }
}
