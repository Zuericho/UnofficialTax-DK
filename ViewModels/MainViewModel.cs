
using Aarsopgoerelse.Models;
using Aarsopgoerelse.Services;

namespace Aarsopgoerelse.ViewModels;

public class MainViewModel : ViewModelBase
{
    public INavigationService Navigation { get; }

    //User Data - Two instances if user is married:
    public Data UserData { get; } = new();
    public Data SpouseData { get; } = new();

    //Relevant Services to make available:
    public BeregningsService Beregning { get; }
    
    //Content pages ViewModels:
    public IndtastningssideViewModel IndtastningssideVM { get; }

    public Side2ViewModel Side2VM { get; }

    public MainViewModel()
    {
        // Opret navigationstjenesten
        var navigationService = new NavigationService();
        Navigation = navigationService;

        //Opret BeregningsService
        Beregning = new BeregningsService();

        // Opret ViewModels
        IndtastningssideVM = new IndtastningssideViewModel(navigationService, UserData, SpouseData);
        Side2VM = new Side2ViewModel(navigationService, UserData, SpouseData, Beregning);

        // Registrer ViewModels i navigationstjenesten
        navigationService.Register(IndtastningssideVM);
        navigationService.Register(Side2VM);

        // Vis indtastningssiden ved opstart
        navigationService.NavigateTo<IndtastningssideViewModel>();
    }
}
