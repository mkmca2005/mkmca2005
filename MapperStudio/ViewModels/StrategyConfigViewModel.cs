using System.Collections.ObjectModel;

namespace MapperStudio.ViewModels;

public sealed class StrategyConfigViewModel : ViewModelBase
{
    private StrategyTabViewModel? _selectedStrategyTab;

    public StrategyConfigViewModel()
    {
        StrategyTabs = new ObservableCollection<StrategyTabViewModel>
        {
            new StrategyTabViewModel("Default", new DefaultStrategyConfigViewModel()),
            new StrategyTabViewModel("IfNull", new IfNullStrategyConfigViewModel()),
            new StrategyTabViewModel("Transform", new TransformStrategyConfigViewModel()),
            new StrategyTabViewModel("DateFormat", new DateFormatStrategyConfigViewModel()),
            new StrategyTabViewModel("ValueMap", new ValueMapStrategyConfigViewModel())
        };

        _selectedStrategyTab = StrategyTabs[0];
    }

    public ObservableCollection<StrategyTabViewModel> StrategyTabs { get; }

    public StrategyTabViewModel? SelectedStrategyTab
    {
        get => _selectedStrategyTab;
        set
        {
            if (_selectedStrategyTab == value)
            {
                return;
            }

            _selectedStrategyTab = value;
            OnPropertyChanged();
        }
    }
}

public sealed class StrategyTabViewModel
{
    public StrategyTabViewModel(string header, ViewModelBase content)
    {
        Header = header;
        Content = content;
    }

    public string Header { get; }

    public ViewModelBase Content { get; }
}

public sealed class DefaultStrategyConfigViewModel : ViewModelBase
{
    private string _selectedDefaultType;
    private string _defaultValue = string.Empty;

    public DefaultStrategyConfigViewModel()
    {
        DefaultTypes = new ObservableCollection<string> { "Static", "System", "Lookup" };
        _selectedDefaultType = DefaultTypes[0];
    }

    public ObservableCollection<string> DefaultTypes { get; }

    public string SelectedDefaultType
    {
        get => _selectedDefaultType;
        set
        {
            if (_selectedDefaultType == value)
            {
                return;
            }

            _selectedDefaultType = value;
            OnPropertyChanged();
        }
    }

    public string DefaultValue
    {
        get => _defaultValue;
        set
        {
            if (_defaultValue == value)
            {
                return;
            }

            _defaultValue = value;
            OnPropertyChanged();
        }
    }
}

public sealed class IfNullStrategyConfigViewModel : ViewModelBase
{
    private string _fallbackValue = string.Empty;

    public string FallbackValue
    {
        get => _fallbackValue;
        set
        {
            if (_fallbackValue == value)
            {
                return;
            }

            _fallbackValue = value;
            OnPropertyChanged();
        }
    }
}

public sealed class TransformStrategyConfigViewModel : ViewModelBase
{
    private string _selectedTransform;

    public TransformStrategyConfigViewModel()
    {
        TransformOptions = new ObservableCollection<string> { "Trim", "Uppercase", "Lowercase", "Concat" };
        _selectedTransform = TransformOptions[0];
        TransformInputs = new ObservableCollection<string> { "Field A", "Field B" };
    }

    public ObservableCollection<string> TransformOptions { get; }

    public ObservableCollection<string> TransformInputs { get; }

    public string SelectedTransform
    {
        get => _selectedTransform;
        set
        {
            if (_selectedTransform == value)
            {
                return;
            }

            _selectedTransform = value;
            OnPropertyChanged();
        }
    }
}

public sealed class DateFormatStrategyConfigViewModel : ViewModelBase
{
    private string _selectedInputFormat;
    private string _selectedOutputFormat;

    public DateFormatStrategyConfigViewModel()
    {
        FormatOptions = new ObservableCollection<string> { "yyyy-MM-dd", "MM/dd/yyyy", "dd-MM-yyyy", "yyyyMMdd" };
        _selectedInputFormat = FormatOptions[0];
        _selectedOutputFormat = FormatOptions[1];
    }

    public ObservableCollection<string> FormatOptions { get; }

    public string SelectedInputFormat
    {
        get => _selectedInputFormat;
        set
        {
            if (_selectedInputFormat == value)
            {
                return;
            }

            _selectedInputFormat = value;
            OnPropertyChanged();
        }
    }

    public string SelectedOutputFormat
    {
        get => _selectedOutputFormat;
        set
        {
            if (_selectedOutputFormat == value)
            {
                return;
            }

            _selectedOutputFormat = value;
            OnPropertyChanged();
        }
    }
}

public sealed class ValueMapStrategyConfigViewModel : ViewModelBase
{
    public ValueMapStrategyConfigViewModel()
    {
        Mappings = new ObservableCollection<ValueMapItem>
        {
            new("Active", "A"),
            new("Inactive", "I")
        };
    }

    public ObservableCollection<ValueMapItem> Mappings { get; }
}

public sealed record ValueMapItem(string SourceValue, string TargetValue);
