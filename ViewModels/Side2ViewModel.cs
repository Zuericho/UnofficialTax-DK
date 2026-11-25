using Aarsopgoerelse.Services;
using Aarsopgoerelse.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;

namespace Aarsopgoerelse.ViewModels;

public partial class Side2ViewModel : ViewModelBase, INavigationAware
{
    private readonly Data _userData;
    private readonly Data _spouseData;
    private readonly INavigationService _navigationService;
    private readonly BeregningsService _beregningsService;

    [ObservableProperty]
    private decimal sum;
    public Side2ViewModel(INavigationService navigationService, Data userData, Data spouseData, BeregningsService beregningsService)
    {
        _navigationService = navigationService;
        _userData = userData;
        _spouseData = spouseData;
        _beregningsService = beregningsService;
    }
    public void OnNavigatedTo()
    {
        Sum = _beregningsService.BeregnSum(_userData);
        writeResult();
    }

    [RelayCommand]
    private void ToIndtastningsside()
    {
        _navigationService.NavigateTo<IndtastningssideViewModel>();
    }

    [RelayCommand]
    private void Beregn()
    {
        Sum = _beregningsService.BeregnSum(_userData);
    }

    //Two test properties and one method for updating info on page insted of writing to console:
    [ObservableProperty]
    private string? result = null;

    [ObservableProperty]
    private bool resultVisible = false;

    public void writeResult()
    {
        Result = "Bopæl: " + _userData.Bopaelskommune + ", Kirkeskat: " + _userData.Folkekirken + "\n";
        foreach(KeyValuePair<string, decimal> felt in _userData.SkatteTal)
        {
            Result += felt.Key + ": " + felt.Value + "\n";
        }
        ResultVisible = true;
    }

}