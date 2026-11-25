using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using Aarsopgoerelse.ViewModels;

namespace Aarsopgoerelse.Services;

public partial class NavigationService : ObservableObject, INavigationService
{
    private readonly Dictionary<Type, ViewModelBase> _viewModels = new();

    [ObservableProperty]
    private ViewModelBase? currentViewModel;

    public void Register<TViewModel>(TViewModel viewModel)
        where TViewModel : ViewModelBase
    {
        _viewModels[typeof(TViewModel)] = viewModel;
    }

    public void NavigateTo<TViewModel>()
        where TViewModel : ViewModelBase
    {
        if (!_viewModels.TryGetValue(typeof(TViewModel), out var viewModel))
        {
            throw new InvalidOperationException(
                $"ViewModel {typeof(TViewModel).Name} er ikke registreret.");
        }

        CurrentViewModel = viewModel;

        if (viewModel is INavigationAware navigationAware)
        {
            navigationAware.OnNavigatedTo();
        }
    }
}