using System.Collections.ObjectModel;
using System.Windows.Input;
using MapperStudio.Models;

namespace MapperStudio.ViewModels;

public sealed class SetupViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;
    private string _mappingTitle = string.Empty;
    private string _description = string.Empty;
    private string _selectedSourceSystem;
    private string _selectedTargetSystem;
    private string _selectedEntity;
    private bool _isSourceSchemaLoaded;
    private bool _isTargetSchemaLoaded;
    private string _sourceSchemaStatus = "Not loaded";
    private string _targetSchemaStatus = "Not loaded";
    private readonly RelayCommand _nextCommand;

    public SetupViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        SystemTypes = new ObservableCollection<string> { "SQL", "REST", "CSV" };
        _selectedSourceSystem = SystemTypes[0];
        _selectedTargetSystem = SystemTypes[1];
        EntityOptions = new ObservableCollection<string> { "cr_contact_profile", "account", "contact" };
        _selectedEntity = EntityOptions[0];
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
        _nextCommand = new RelayCommand(_ => _mainViewModel.CurrentView = _mainViewModel.SchemaPreviewViewModel, _ => CanProceed());
        NextCommand = _nextCommand;
    }

    public ObservableCollection<string> SystemTypes { get; }

    public ObservableCollection<string> EntityOptions { get; }

    public ObservableCollection<TargetColumn> TargetColumns { get; }

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

    public string SelectedSourceSystem
    {
        get => _selectedSourceSystem;
        set
        {
            if (_selectedSourceSystem == value)
            {
                return;
            }

            _selectedSourceSystem = value;
            OnPropertyChanged();
        }
    }

    public string SelectedTargetSystem
    {
        get => _selectedTargetSystem;
        set
        {
            if (_selectedTargetSystem == value)
            {
                return;
            }

            _selectedTargetSystem = value;
            OnPropertyChanged();
        }
    }

    public string SelectedEntity
    {
        get => _selectedEntity;
        set
        {
            if (_selectedEntity == value)
            {
                return;
            }

            _selectedEntity = value;
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

    private bool CanProceed() => _isSourceSchemaLoaded && _isTargetSchemaLoaded;
}
