using Aarsopgoerelse.ViewModels;

namespace Aarsopgoerelse.Services;

public interface INavigationService
{
    ViewModelBase? CurrentViewModel { get; }

    void Register<TViewModel>(TViewModel viewModel)
        where TViewModel : ViewModelBase;

    void NavigateTo<TViewModel>()
        where TViewModel : ViewModelBase;
}