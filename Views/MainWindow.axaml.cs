using Avalonia.Controls;
using Aarsopgoerelse.ViewModels;

namespace Aarsopgoerelse.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void VisIndtastningsside()
    {
        var mainVM = (MainViewModel)DataContext!;

        SideIndhold.Content = new Indtastningsside
        {
            DataContext = mainVM.IndtastningssideVM
        };
    }
}