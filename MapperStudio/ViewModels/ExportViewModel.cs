using System.ComponentModel;
using System.Text.Json;
using System.Windows.Input;
using MapperStudio.Models;

namespace MapperStudio.ViewModels;

public sealed class ExportViewModel : ViewModelBase
{
    private readonly ValidationViewModel _validationViewModel;
    private string _exportStatus = "";
    private bool _canExport;
    private readonly RelayCommand _exportCommand;

    public ExportViewModel(ValidationViewModel validationViewModel)
    {
        _validationViewModel = validationViewModel;
        MappingTitle = "Customer_Mapping";
        SourceSystem = "REST";
        TargetSystem = "SQL";
        MappedFieldCount = 24;
        JsonPreview = BuildJsonPreview();

        _exportCommand = new RelayCommand(_ => Export(), _ => CanExport);
        ExportCommand = _exportCommand;

        _validationViewModel.PropertyChanged += ValidationViewModelOnPropertyChanged;
        RefreshExportState();
    }

    public string MappingTitle { get; }

    public string SourceSystem { get; }

    public string TargetSystem { get; }

    public int MappedFieldCount { get; }

    public string JsonPreview { get; }

    public string ExportStatus
    {
        get => _exportStatus;
        private set
        {
            if (_exportStatus == value)
            {
                return;
            }

            _exportStatus = value;
            OnPropertyChanged();
        }
    }

    public bool CanExport
    {
        get => _canExport;
        private set
        {
            if (_canExport == value)
            {
                return;
            }

            _canExport = value;
            OnPropertyChanged();
            _exportCommand.RaiseCanExecuteChanged();
        }
    }

    public ICommand ExportCommand { get; }

    private void Export()
    {
        ExportStatus = "Mapping exported to JSON.";
    }

    private void RefreshExportState()
    {
        CanExport = !_validationViewModel.HasErrors;
        if (!CanExport)
        {
            ExportStatus = "Resolve validation errors to enable export.";
        }
        else if (string.IsNullOrWhiteSpace(ExportStatus))
        {
            ExportStatus = "Ready to export.";
        }
    }

    private void ValidationViewModelOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ValidationViewModel.HasErrors))
        {
            RefreshExportState();
        }
    }

    private string BuildJsonPreview()
    {
        var export = new MappingExport
        {
            MappingTitle = MappingTitle,
            Source = new SystemInfo { SystemType = SourceSystem },
            Target = new SystemInfo { SystemType = TargetSystem }
        };

        return JsonSerializer.Serialize(export, new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
    }
}
