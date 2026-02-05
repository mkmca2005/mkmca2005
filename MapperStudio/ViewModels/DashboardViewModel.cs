using System.Collections.ObjectModel;
using System.Windows.Input;
using MapperStudio.Models;

namespace MapperStudio.ViewModels;

public sealed class DashboardViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;
    private string _searchText = string.Empty;
    private string _selectedFilter;

    public DashboardViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        FilterOptions = new ObservableCollection<string> { "All", "Draft", "Validated", "Exported" };
        _selectedFilter = FilterOptions[0];
        Mappings = new ObservableCollection<MappingSummary>
        {
            new MappingSummary("Customer Integration","Customer Integration - Integration between SQL to SQL database.Customer Integration - Integration between SQL to SQL database.", "1,247 records", "Legacy CRM", "SQL", "Salesforce", "SQL","Account", "Draft", "1.2.0", "2 hours ago", "Sarah Johnson"),
            new MappingSummary("Product Catalog Sync","Description 1", "5,432 records", "E-commerce API", "REST API", "ERP System", "SQL", "Product", "Published", "2.1.5", "22 hours ago", "Mike Chen"),
            new MappingSummary("Order Data Migration","Description 1", "892 records", "CSV Import", "CSV", "Dynamics 365", "SalesOrder","SQL", "Published", "1.0.0", "3 days ago", "Alex Martinez"),
            new MappingSummary("Contact Synchronization","Description 1", "3,201 records", "Dataverse", "Dataverse", "Marketing Cloud",  "SQL","Contact", "Testing", "1.1.0", "4 hours ago", "Emily Davis"),
            new MappingSummary("Invoice Processing","Description 1", "0 records", "Financial DB", "SQL", "QuickBooks", "Invoice", "Error", "SQL", "1.3.2", "20 hours ago", "David Wilson"),
            new MappingSummary("Employee Directory","Description 1", "456 records", "HR System API", "REST API", "Active Directory", "SQL", "User", "Published", "3.0.1", "5 days ago", "Lisa Anderson")
        };

        CreateNewMappingCommand = new RelayCommand(_ => _mainViewModel.CurrentView = _mainViewModel.SetupViewModel);
    }

    public ObservableCollection<MappingSummary> Mappings { get; }

    public ObservableCollection<string> FilterOptions { get; }

    public string SelectedFilter
    {
        get => _selectedFilter;
        set
        {
            if (_selectedFilter == value)
            {
                return;
            }

            _selectedFilter = value;
            OnPropertyChanged();
        }
    }

    public string SearchText
    {
        get => _searchText;
        set
        {
            if (_searchText == value)
            {
                return;
            }

            _searchText = value;
            OnPropertyChanged();
        }
    }

    public ICommand CreateNewMappingCommand { get; }
}
