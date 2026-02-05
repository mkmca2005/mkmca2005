using System.Windows.Input;

namespace MapperStudio.ViewModels;

public sealed class MainViewModel : ViewModelBase
{
    private ViewModelBase _currentView;

    public MainViewModel()
    {
        DashboardViewModel = new DashboardViewModel(this);
        SetupViewModel = new SetupViewModel(this);
        SchemaPreviewViewModel = new SchemaPreviewViewModel();
        MappingEditorViewModel = new MappingEditorViewModel(this);
        StrategyConfigViewModel = new StrategyConfigViewModel();
        ValidationViewModel = new ValidationViewModel();
        ExportViewModel = new ExportViewModel(ValidationViewModel);

        _currentView = DashboardViewModel;

        NavigateDashboardCommand = new RelayCommand(_ => CurrentView = DashboardViewModel);
        NavigateSetupCommand = new RelayCommand(_ => CurrentView = SetupViewModel);
        NavigateSchemaPreviewCommand = new RelayCommand(_ => CurrentView = SchemaPreviewViewModel);
        NavigateMappingEditorCommand = new RelayCommand(_ => CurrentView = MappingEditorViewModel);
        NavigateStrategyConfigCommand = new RelayCommand(_ => CurrentView = StrategyConfigViewModel);
        NavigateValidationCommand = new RelayCommand(_ => CurrentView = ValidationViewModel);
        NavigateExportCommand = new RelayCommand(_ => CurrentView = ExportViewModel);
    }

    public DashboardViewModel DashboardViewModel { get; }
    public SetupViewModel SetupViewModel { get; }
    public SchemaPreviewViewModel SchemaPreviewViewModel { get; }
    public MappingEditorViewModel MappingEditorViewModel { get; }
    public StrategyConfigViewModel StrategyConfigViewModel { get; }
    public ValidationViewModel ValidationViewModel { get; }
    public ExportViewModel ExportViewModel { get; }

    public ViewModelBase CurrentView
    {
        get => _currentView;
        set
        {
            if (_currentView == value)
            {
                return;
            }

            _currentView = value;
            OnPropertyChanged();
        }
    }

    public ICommand NavigateDashboardCommand { get; }
    public ICommand NavigateSetupCommand { get; }
    public ICommand NavigateSchemaPreviewCommand { get; }
    public ICommand NavigateMappingEditorCommand { get; }
    public ICommand NavigateStrategyConfigCommand { get; }
    public ICommand NavigateValidationCommand { get; }
    public ICommand NavigateExportCommand { get; }
}
