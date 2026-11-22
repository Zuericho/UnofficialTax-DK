using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using Aarsopgoerelse.ViewModels;
using Aarsopgoerelse.Models;

namespace Aarsopgoerelse.Views;

public partial class Indtastningsside : UserControl
{
    private IndtastningssideViewModel viewModel => (IndtastningssideViewModel)DataContext!;
    private readonly List<TextBox> felter = new();

    private bool allValid = false;
    public Indtastningsside()
    {
        InitializeComponent();

        // Populate ComboBox KommuneComboBox:
        KommuneComboBox.ItemsSource = Kommuneskat.Kommunenavne;

        // Find TextBoxes to read numbers from:
        FindTextBoxes();
    }

    private void FindTextBoxes()
    {
        felter.Add(this.FindControl<TextBox>("BruttoLoen") ?? throw new InvalidOperationException(
            "TextBox 'BruttoLoen' not found in Indtastningsside.axaml."));
        felter.Add(this.FindControl<TextBox>("AMBidrag") ?? throw new InvalidOperationException(
            "TextBox 'AMBidrag' not found in Indtastningsside.axaml."));
        felter.Add(this.FindControl<TextBox>("ASkat") ?? throw new InvalidOperationException(
            "TextBox 'ASkat' not found in Indtastningsside.axaml."));
        felter.Add(this.FindControl<TextBox>("AKasseDagpenge") ?? throw new InvalidOperationException(
            "TextBox 'AKasseDagpenge' not found in Indtastningsside.axaml."));
        felter.Add(this.FindControl<TextBox>("Feriedagpenge") ?? throw new InvalidOperationException(
            "TextBox 'Feriedagpenge' not found in Indtastningsside.axaml."));
        felter.Add(this.FindControl<TextBox>("Sygedagpenge") ?? throw new InvalidOperationException(
            "TextBox 'Sygedagpenge' not found in Indtastningsside.axaml."));
        felter.Add(this.FindControl<TextBox>("Kontanthjaelp") ?? throw new InvalidOperationException(
            "TextBox 'Kontanthjaelp' not found in Indtastningsside.axaml."));
        felter.Add(this.FindControl<TextBox>("Efterloen") ?? throw new InvalidOperationException(
            "TextBox 'Efterloen' not found in Indtastningsside.axaml."));
        felter.Add(this.FindControl<TextBox>("SVU") ?? throw new InvalidOperationException(
            "TextBox 'SVU' not found in Indtastningsside.axaml."));
        felter.Add(this.FindControl<TextBox>("SU") ?? throw new InvalidOperationException(
            "TextBox 'SU' not found in Indtastningsside.axaml."));
        felter.Add(this.FindControl<TextBox>("ArbejdsgiverPension") ?? throw new InvalidOperationException(
            "TextBox 'ArbejdsgiverPension' not found in Indtastningsside.axaml."));
        felter.Add(this.FindControl<TextBox>("ATPAMBidragsPligt") ?? throw new InvalidOperationException(
            "TextBox 'ATPAMBidragsPligt' not found in Indtastningsside.axaml."));
        felter.Add(this.FindControl<TextBox>("ATPIkkeAMBidragsPligt") ?? throw new InvalidOperationException(
            "TextBox 'ATPIkkeAMBidragsPligt' not found in Indtastningsside.axaml."));
        felter.Add(this.FindControl<TextBox>("PensionsindbetalingAfBeskattedeMidler") ?? throw new InvalidOperationException(
            "TextBox 'PensionsindbetalingAfBeskattedeMidler' not found in Indtastningsside.axaml."));
        felter.Add(this.FindControl<TextBox>("GruppelivsForsikring") ?? throw new InvalidOperationException(
            "TextBox 'GruppelivsForsikring' not found in Indtastningsside.axaml."));
        felter.Add(this.FindControl<TextBox>("LegaterOgPersonalegoder") ?? throw new InvalidOperationException(
            "TextBox 'LegaterOgPersonalegoder' not found in Indtastningsside.axaml."));
        felter.Add(this.FindControl<TextBox>("IndskudAldersopsparingViaArbejdsgiver") ?? throw new InvalidOperationException(
            "TextBox 'IndskudAldersopsparingViaArbejdsgiver' not found in Indtastningsside.axaml."));
        felter.Add(this.FindControl<TextBox>("AndetPersonligIndkomst") ?? throw new InvalidOperationException(
            "TextBox 'AndetPersonligIndkomst' not found in Indtastningsside.axaml."));
        felter.Add(this.FindControl<TextBox>("Renteindtaegter") ?? throw new InvalidOperationException(
            "TextBox 'Renteindtaegter' not found in Indtastningsside.axaml."));
        felter.Add(this.FindControl<TextBox>("FitnessMusikskole") ?? throw new InvalidOperationException(
            "TextBox 'FitnessMusikskole' not found in Indtastningsside.axaml."));
        felter.Add(this.FindControl<TextBox>("AfstandArbejde") ?? throw new InvalidOperationException(
            "TextBox 'AfstandArbejde' not found in Indtastningsside.axaml."));
        felter.Add(this.FindControl<TextBox>("AntalDageKoert") ?? throw new InvalidOperationException(
            "TextBox 'AntalDageKoert' not found in Indtastningsside.axaml."));
        felter.Add(this.FindControl<TextBox>("KontingentFagforening") ?? throw new InvalidOperationException(
            "TextBox 'KontingentFagforening' not found in Indtastningsside.axaml."));
        felter.Add(this.FindControl<TextBox>("KontingentAKasse") ?? throw new InvalidOperationException(
            "TextBox 'KontingentAKasse' not found in Indtastningsside.axaml."));
        felter.Add(this.FindControl<TextBox>("GaverTilForeninger") ?? throw new InvalidOperationException(
            "TextBox 'GaverTilForeninger' not found in Indtastningsside.axaml."));        
    }

    private void VisAarsopgoerelse_Click(object? sender, RoutedEventArgs e)
    {
        Data userData = ValiderAlleFelter();
        
        if(this.allValid)
        {
            // Valid data -> Do calculations and go to next page.
            lblResultat.Text = "Gyldige talfelter: " + userData.SkatteTal.Keys.Count.ToString();
            lblResultat.Text += "\n " + userData.Bopaelskommune + "; Skat: " + userData.Kommuneskat*100 + "%";
            lblResultat.Text += "\nKirkeskat: " + userData.Kirkeskat*100 + "%";
            lblResultat.IsVisible = true;
        }
    }

    private Data ValiderAlleFelter()
    {
        this.allValid = true;

        Data userData = new Data();
        string? kommune = KommuneComboBox.SelectedItem as string;

        foreach (var felt in felter)
        {
            if (viewModel.IsValidNumber(felt.Text ?? "", out decimal tal))
            {
                felt.ClearValue(BorderBrushProperty);
                felt.ClearValue(BorderThicknessProperty);

                //Save valid data.
                userData.SkatteTal[felt.Name ?? throw new InvalidOperationException("A TextBox in Indtastningsside.axaml is missing a name.")] = tal;
            }
            else
            {
                felt.BorderBrush = Avalonia.Media.Brushes.Red;
                felt.BorderThickness = new Thickness(2);

                this.allValid = false;
            }

        }
        
        if (kommune != null)
        {
            KommuneComboBox.ClearValue(BorderBrushProperty);
            KommuneComboBox.ClearValue(BorderThicknessProperty);

            //Save valid data.
            userData.Kommuneskat = Kommuneskat.HentKommuneskat(kommune);
            userData.Bopaelskommune = kommune;
            if (ValiderKirkeSkat()) //Colouring of KirkeskatComboBox happens in ValiderKirkeSkat()
            {
                userData.Kirkeskat = Kommuneskat.HentKirkeskat(kommune);
            }
            else
            {
                userData.Kirkeskat = 0;
            }

        }
        else
        {
            ValiderKirkeSkat(); //Colouring of KirkeskatComboBox happens in ValiderKirkeSkat() also if kommune is null.
            KommuneComboBox.BorderBrush = Brushes.Red;
            KommuneComboBox.BorderThickness = new Thickness(2);
            this.allValid = false;
        }

        return userData;
    }

    private bool ValiderKirkeSkat()
    {
        bool? kirkeskat = viewModel.BetalerKirkeskat;

       if (kirkeskat != null)
        {
            KirkeskatComboBox.ClearValue(BorderBrushProperty);
            KirkeskatComboBox.ClearValue(BorderThicknessProperty);

            return kirkeskat.Value;

        }
        else
        {
            KirkeskatComboBox.BorderBrush = Brushes.Red;
            KirkeskatComboBox.BorderThickness = new Thickness(2);
            this.allValid = false;
            //Program wont move on while this.allValid = false. But just in case: Tax to the church is by default assumed.
            return true;
        }
    }

}