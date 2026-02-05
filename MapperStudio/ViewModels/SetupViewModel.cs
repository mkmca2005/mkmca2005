using System.Collections.ObjectModel;
using System.Windows.Input;
using MapperStudio.Models;

namespace MapperStudio.ViewModels;

public sealed class SetupViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;
    private string _mappingTitle = string.Empty;
    private string _description = string.Empty;
    private string _selectedSourceConnectionType;
    private string _selectedTargetConnectionType;
    private string _sourceEndpointUrl = "https://api.enterprise.com/v1/users/schema";
    private string _sourceBearerToken = "";
    private string _sourceConnectionString = "";
    private string _sourceTableName = "";
    private string _sourceCsvPath = "";
    private string _targetEndpointUrl = "";
    private string _targetBearerToken = "";
    private string _targetConnectionString = "";
    private string _targetTableName = "";
    private string _targetCsvPath = "";
    private bool _isInbound = true;
    private bool _isOutbound;
    private bool _isSourceSchemaLoaded;
    private bool _isTargetSchemaLoaded;
    private string _sourceSchemaStatus = "Not loaded";
    private string _targetSchemaStatus = "Not loaded";
    private readonly RelayCommand _nextCommand;

    public SetupViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        ConnectionTypes = new ObservableCollection<string> { "REST API", "SQL Server", "CSV" };
        _selectedSourceConnectionType = ConnectionTypes[0];
        _selectedTargetConnectionType = ConnectionTypes[1];

        SourceHeaders = new ObservableCollection<HeaderItem>
        {
            new("X-Tenant-ID", "US-PROD-01"),
            new("X-Region", "US-EAST")
        };

        TargetHeaders = new ObservableCollection<HeaderItem>
        {
            new("X-Org-Id", "ACME"),
            new("X-Env", "PROD")
        };

        SourceSchema = new ObservableCollection<SchemaNode>
        {
            new SchemaNode("user")
            {
                Children =
                {
                    new SchemaNode("id"),
                    new SchemaNode("name"),
                    new SchemaNode("email"),
                    new SchemaNode("preferences"),
                    new SchemaNode("timestamp")
                }
            }
        };

        TargetColumns = new ObservableCollection<TargetColumn>
        {
            new("ExternalId", "Guid", "*"),
            new("DisplayName", "String(250)", ""),
            new("PrimaryEmail", "String(100)", ""),
            new("LastModified", "DateTime", ""),
            new("SystemRole", "Choice", ""),
            new("CreatedBy", "Lookup", "")
        };

        LoadSourceSchemaCommand = new RelayCommand(_ => LoadSourceSchema());
        LoadTargetSchemaCommand = new RelayCommand(_ => LoadTargetSchema());
        SaveDraftCommand = new RelayCommand(_ => SaveDraft());
        _nextCommand = new RelayCommand(_ => _mainViewModel.CurrentView = _mainViewModel.SchemaPreviewViewModel, _ => CanProceed());
        NextCommand = _nextCommand;

        ConnectionStatusText = "Connected: API Gateway";
        EnvironmentText = "US-PROD";
        LastValidationTime = "Today 14:02";
    }

    public ObservableCollection<string> ConnectionTypes { get; }

    public ObservableCollection<HeaderItem> SourceHeaders { get; }

    public ObservableCollection<HeaderItem> TargetHeaders { get; }

    public ObservableCollection<SchemaNode> SourceSchema { get; }

    public ObservableCollection<TargetColumn> TargetColumns { get; }

    public string ConnectionStatusText { get; }

    public string EnvironmentText { get; }

    public string LastValidationTime { get; }

    public string MappingTitle
    {
        get => _mappingTitle;
        set
        {
            if (_mappingTitle == value)
            {
                return;
            }

            _mappingTitle = value;
            OnPropertyChanged();
        }
    }

    public string Description
    {
        get => _description;
        set
        {
            if (_description == value)
            {
                return;
            }

            _description = value;
            OnPropertyChanged();
        }
    }

    public bool IsInbound
    {
        get => _isInbound;
        set
        {
            if (_isInbound == value)
            {
                return;
            }

            _isInbound = value;
            if (value)
            {
                _isOutbound = false;
                OnPropertyChanged(nameof(IsOutbound));
            }

            OnPropertyChanged();
        }
    }

    public bool IsOutbound
    {
        get => _isOutbound;
        set
        {
            if (_isOutbound == value)
            {
                return;
            }

            _isOutbound = value;
            if (value)
            {
                _isInbound = false;
                OnPropertyChanged(nameof(IsInbound));
            }

            OnPropertyChanged();
        }
    }

    public string SelectedSourceConnectionType
    {
        get => _selectedSourceConnectionType;
        set
        {
            if (_selectedSourceConnectionType == value)
            {
                return;
            }

            _selectedSourceConnectionType = value;
            OnPropertyChanged();
        }
    }

    public string SelectedTargetConnectionType
    {
        get => _selectedTargetConnectionType;
        set
        {
            if (_selectedTargetConnectionType == value)
            {
                return;
            }

            _selectedTargetConnectionType = value;
            OnPropertyChanged();
        }
    }

    public string SourceEndpointUrl
    {
        get => _sourceEndpointUrl;
        set
        {
            if (_sourceEndpointUrl == value)
            {
                return;
            }

            _sourceEndpointUrl = value;
            OnPropertyChanged();
        }
    }

    public string SourceBearerToken
    {
        get => _sourceBearerToken;
        set
        {
            if (_sourceBearerToken == value)
            {
                return;
            }

            _sourceBearerToken = value;
            OnPropertyChanged();
        }
    }

    public string SourceConnectionString
    {
        get => _sourceConnectionString;
        set
        {
            if (_sourceConnectionString == value)
            {
                return;
            }

            _sourceConnectionString = value;
            OnPropertyChanged();
        }
    }

    public string SourceTableName
    {
        get => _sourceTableName;
        set
        {
            if (_sourceTableName == value)
            {
                return;
            }

            _sourceTableName = value;
            OnPropertyChanged();
        }
    }

    public string SourceCsvPath
    {
        get => _sourceCsvPath;
        set
        {
            if (_sourceCsvPath == value)
            {
                return;
            }

            _sourceCsvPath = value;
            OnPropertyChanged();
        }
    }

    public string TargetEndpointUrl
    {
        get => _targetEndpointUrl;
        set
        {
            if (_targetEndpointUrl == value)
            {
                return;
            }

            _targetEndpointUrl = value;
            OnPropertyChanged();
        }
    }

    public string TargetBearerToken
    {
        get => _targetBearerToken;
        set
        {
            if (_targetBearerToken == value)
            {
                return;
            }

            _targetBearerToken = value;
            OnPropertyChanged();
        }
    }

    public string TargetConnectionString
    {
        get => _targetConnectionString;
        set
        {
            if (_targetConnectionString == value)
            {
                return;
            }

            _targetConnectionString = value;
            OnPropertyChanged();
        }
    }

    public string TargetTableName
    {
        get => _targetTableName;
        set
        {
            if (_targetTableName == value)
            {
                return;
            }

            _targetTableName = value;
            OnPropertyChanged();
        }
    }

    public string TargetCsvPath
    {
        get => _targetCsvPath;
        set
        {
            if (_targetCsvPath == value)
            {
                return;
            }

            _targetCsvPath = value;
            OnPropertyChanged();
        }
    }

    public string SourceSchemaStatus
    {
        get => _sourceSchemaStatus;
        private set
        {
            if (_sourceSchemaStatus == value)
            {
                return;
            }

            _sourceSchemaStatus = value;
            OnPropertyChanged();
        }
    }

    public string TargetSchemaStatus
    {
        get => _targetSchemaStatus;
        private set
        {
            if (_targetSchemaStatus == value)
            {
                return;
            }

            _targetSchemaStatus = value;
            OnPropertyChanged();
        }
    }

    public ICommand LoadSourceSchemaCommand { get; }

    public ICommand LoadTargetSchemaCommand { get; }

    public ICommand SaveDraftCommand { get; }

    public ICommand NextCommand { get; }

    private void LoadSourceSchema()
    {
        _isSourceSchemaLoaded = true;
        SourceSchemaStatus = "Loaded sample schema";
        _nextCommand.RaiseCanExecuteChanged();
    }

    private void LoadTargetSchema()
    {
        _isTargetSchemaLoaded = true;
        TargetSchemaStatus = "Loaded sample schema";
        _nextCommand.RaiseCanExecuteChanged();
    }

    private void SaveDraft()
    {
    }

    private bool CanProceed() => _isSourceSchemaLoaded && _isTargetSchemaLoaded;
}

public sealed record HeaderItem(string Key, string Value);
