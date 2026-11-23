using CommunityToolkit.Mvvm.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System;
using Aarsopgoerelse.Models;
using Aarsopgoerelse.Services;
using Avalonia.Metadata;

namespace Aarsopgoerelse.ViewModels;

public partial class IndtastningssideViewModel : ViewModelBase
{
    private readonly CultureInfo danskKultur =
    CultureInfo.GetCultureInfo("da-DK");

    [ObservableProperty]
    public partial string VisAarsopgoerelse { get; set; } = "Vis min uofficielle\nÅrsopgørelse";

    //First element of list must be the confirming choice of being a member of the danish church.
    //Second element is the denying choice.
    public IReadOnlyList<string> KirkeskatValgmuligheder { get; } =
    new List<string> { "Ja", "Nej" };

    public IReadOnlyList<string> Kommunenavne => KommuneskatSatser.Kommunenavne;

    Data userData = new Data();

    [ObservableProperty]
    private string bruttoLoen = "";

    [ObservableProperty]
    private bool bruttoLoenErGyldig = true;

    [ObservableProperty]
    private string  aMBidrag= "";

    [ObservableProperty]
    private bool aMBidragErGyldig = true;

    [ObservableProperty]
    private string aSkat = "";

    [ObservableProperty]
    private bool aSkatErGyldig = true;

    [ObservableProperty]
    private string aKasseDagpenge = "";

    [ObservableProperty]
    private bool aKasseDagpengeErGyldig = true;

    [ObservableProperty]
    private string feriedagpenge = "";

    [ObservableProperty]
    private bool feriedagpengeErGyldig = true;

    [ObservableProperty]
    private string sygedagpenge = "";

    [ObservableProperty]
    private bool sygedagpengeErGyldig = true;

    [ObservableProperty]
    private string kontanthjaelp = "";

    [ObservableProperty]
    private bool kontanthjaelpErGyldig = true;

    [ObservableProperty]
    private string efterloen = "";

    [ObservableProperty]
    private bool efterloenErGyldig = true;

    [ObservableProperty]
    private string sVU = "";

    [ObservableProperty]
    private bool sVUErGyldig = true;

    [ObservableProperty]
    private string sU = "";

    [ObservableProperty]
    private bool sUErGyldig = true;

    [ObservableProperty]
    private string arbejdsgiverPension = "";

    [ObservableProperty]
    private bool arbejdsgiverPensionErGyldig = true;

    [ObservableProperty]
    private string aTPAMBidragsPligt = "";

    [ObservableProperty]
    private bool aTPAMBidragsPligtErGyldig = true;

    [ObservableProperty]
    private string aTPIkkeAMBidragsPligt = "";

    [ObservableProperty]
    private bool aTPIkkeAMBidragsPligtErGyldig = true;

    [ObservableProperty]
    private string pensionsindbetalingAfBeskattedeMidler = "";

    [ObservableProperty]
    private bool pensionsindbetalingAfBeskattedeMidlerErGyldig = true;

    [ObservableProperty]
    private string gruppelivsForsikring = "";

    [ObservableProperty]
    private bool gruppelivsForsikringErGyldig = true;

    [ObservableProperty]
    private string legaterOgPersonalegoder = "";

    [ObservableProperty]
    private bool legaterOgPersonalegoderErGyldig = true;

    [ObservableProperty]
    private string indskudAldersopsparingViaArbejdsgiver = "";

    [ObservableProperty]
    private bool indskudAldersopsparingViaArbejdsgiverErGyldig = true;

    [ObservableProperty]
    private string andetPersonligIndkomst = "";

    [ObservableProperty]
    private bool andetPersonligIndkomstErGyldig = true;

    [ObservableProperty]
    private string renteindtaegter = "";

    [ObservableProperty]
    private bool renteindtaegterErGyldig = true;

    [ObservableProperty]
    private string fitnessMusikskole = "";

    [ObservableProperty]
    private bool fitnessMusikskoleErGyldig = true;

    [ObservableProperty]
    private string afstandArbejde = "";

    [ObservableProperty]
    private bool afstandArbejdeErGyldig = true;

    [ObservableProperty]
    private string antalDageKoert = "";

    [ObservableProperty]
    private bool antalDageKoertErGyldig = true;

    [ObservableProperty]
    private string kontingentFagforening = "";

    [ObservableProperty]
    private bool kontingentFagforeningErGyldig = true;

    [ObservableProperty]
    private string kontingentAKasse = "";

    [ObservableProperty]
    private bool kontingentAKasseErGyldig = true;

    [ObservableProperty]
    private string gaverTilForeninger = "";

    [ObservableProperty]
    private bool gaverTilForeningerErGyldig = true;

    [ObservableProperty]
    private string? kommuneComboBox = null;

    [ObservableProperty]
    private bool kommuneComboBoxErGyldig = true;

    [ObservableProperty]
    private string? kirkeskatComboBox = null;

    [ObservableProperty]
    private bool kirkeskatComboBoxErGyldig = true;

    private decimal tal;

    private int talInt;

    public bool? BetalerKirkeskat => KirkeskatComboBox switch
    {
        var valg when valg == KirkeskatValgmuligheder[0] => true,
        var valg when valg == KirkeskatValgmuligheder[1] => false,
        _ => null
    };
    public bool ValidateAndStoreDecimal(string userInput, string key)
    {
        bool valid = decimal.TryParse(
            userInput,
            NumberStyles.AllowDecimalPoint, //.Number Tillader . i tallet. .AllowLeadingSign tillader negative tal.
            danskKultur,
            out tal
        );

        if (valid)
        {
            this.userData.SkatteTal[key] = tal;
            return true;
        }
        else
        {
            return false;
        }
    }

    //Two test properties and one method for updating info on page insted of writing to console:
    [ObservableProperty]
    private string? result = null;

    [ObservableProperty]
    private bool resultVisible = false;

    private void writeResult()
    {
        Result = "Bopæl: " + userData.Bopaelskommune + ", Kirkeskat: " + userData.Folkekirken + "\n";
        foreach(KeyValuePair<string, decimal> felt in userData.SkatteTal)
        {
            Result += felt.Key + ": " + felt.Value + "\n";
        }
    }

    public void BeregnCommand()
    {
        if(ValiderAlleFelter())
        {
        //Logic to move on to next page. Remember to somehow parse this.userData!
        writeResult();
        ResultVisible = true;            
        }
    }

    private bool ValiderAlleFelter()
    {
        BruttoLoenErGyldig = ValidateAndStoreDecimal(BruttoLoen, "Bruttoloen");
        AMBidragErGyldig = ValidateAndStoreDecimal(AMBidrag, "AMBidrag");
        ASkatErGyldig = ValidateAndStoreDecimal(ASkat, "ASkat");
        AKasseDagpengeErGyldig = ValidateAndStoreDecimal(AKasseDagpenge, "AKasseDagpenge");
        FeriedagpengeErGyldig = ValidateAndStoreDecimal(Feriedagpenge, "Feriedagpenge");
        SygedagpengeErGyldig = ValidateAndStoreDecimal(Sygedagpenge, "Sygedagpenge");
        KontanthjaelpErGyldig = ValidateAndStoreDecimal(Kontanthjaelp, "Kontanthjaelp");
        EfterloenErGyldig = ValidateAndStoreDecimal(Efterloen, "Efterloen");
        SVUErGyldig = ValidateAndStoreDecimal(SVU, "SVU");
        SUErGyldig = ValidateAndStoreDecimal(SU, "SU");
        ArbejdsgiverPensionErGyldig = ValidateAndStoreDecimal(ArbejdsgiverPension, "ArbejdsgiverPension");
        ATPAMBidragsPligtErGyldig = ValidateAndStoreDecimal(ATPAMBidragsPligt, "ATPAMBidragsPligt");
        ATPIkkeAMBidragsPligtErGyldig = ValidateAndStoreDecimal(ATPIkkeAMBidragsPligt, "ATPIkkeAMBidragsPligt");
        PensionsindbetalingAfBeskattedeMidlerErGyldig = ValidateAndStoreDecimal(PensionsindbetalingAfBeskattedeMidler, "PensionsindbetalingAfBeskattedeMidler");
        GruppelivsForsikringErGyldig = ValidateAndStoreDecimal(GruppelivsForsikring, "GruppelivsForsikring");
        LegaterOgPersonalegoderErGyldig = ValidateAndStoreDecimal(LegaterOgPersonalegoder, "LegaterOgPersonalegoder");
        IndskudAldersopsparingViaArbejdsgiverErGyldig = ValidateAndStoreDecimal(IndskudAldersopsparingViaArbejdsgiver, "IndskudAldersopsparingViaArbejdsgiver");
        AndetPersonligIndkomstErGyldig = ValidateAndStoreDecimal(AndetPersonligIndkomst, "AndetPersonligIndkomst");
        RenteindtaegterErGyldig = ValidateAndStoreDecimal(Renteindtaegter, "Renteindtaegter");
        FitnessMusikskoleErGyldig = ValidateAndStoreDecimal(FitnessMusikskole, "FitnessMusikskole");
        AfstandArbejdeErGyldig = ValidateAndStoreDecimal(AfstandArbejde, "AfstandArbejde");
        AntalDageKoertErGyldig = int.TryParse(AntalDageKoert, out talInt) && talInt >= 0 && talInt <= 366;
        ValidateAndStoreDecimal(talInt.ToString(), "AntalDageKoert");
        KontingentFagforeningErGyldig = ValidateAndStoreDecimal(KontingentFagforening, "KontingentFagforening");
        KontingentAKasseErGyldig = ValidateAndStoreDecimal(KontingentAKasse, "KontingentAKasse");
        GaverTilForeningerErGyldig = ValidateAndStoreDecimal(GaverTilForeninger, "GaverTilForeninger");

        //Validate and store Kommune and kirke.
        if (KommuneComboBox != null)
        {
            KommuneComboBoxErGyldig = true;
            userData.Bopaelskommune = KommuneComboBox;
            decimal kommuneskat = KommuneskatSatser.HentKommuneskat(KommuneComboBox);
            ValidateAndStoreDecimal(kommuneskat.ToString(),"Kommuneskat");
        }
        else
        {
            KommuneComboBoxErGyldig = false;
        }

        decimal kirkeskat;
        if (BetalerKirkeskat != null && KommuneComboBox != null)
        {
            KirkeskatComboBoxErGyldig = true;
            userData.Folkekirken = BetalerKirkeskat;
            if (BetalerKirkeskat.Value)
            {
                kirkeskat = KommuneskatSatser.HentKirkeskat(KommuneComboBox);
            }
            else
            {
                kirkeskat = 0;
            }
            ValidateAndStoreDecimal(kirkeskat.ToString(),"Kirkeskat");
        }
        else if(BetalerKirkeskat != null)
        {
            //Program shouldn't pass here since KommuneComboBox is null. This method should return false.
            // For precautionary reasons Kirkeskat defaults to highest value if BetalerKirkeskat is true.
            KirkeskatComboBoxErGyldig = true;
            userData.Folkekirken = BetalerKirkeskat;
            if (BetalerKirkeskat.Value)
            {
                kirkeskat = KommuneskatSatser.HighestKirkeskat();
            }
            else
            {
                kirkeskat = 0;
            }
            ValidateAndStoreDecimal(kirkeskat.ToString(),"Kirkeskat");
        }
        else
        {
            KirkeskatComboBoxErGyldig = false;
        }

        // Return true if and only if all boxes in ui hav valid input.
        return BruttoLoenErGyldig &&
            AMBidragErGyldig &&
            ASkatErGyldig &&
            AKasseDagpengeErGyldig &&
            FeriedagpengeErGyldig &&
            SygedagpengeErGyldig &&
            KontanthjaelpErGyldig &&
            EfterloenErGyldig &&
            SVUErGyldig &&
            SUErGyldig &&
            ArbejdsgiverPensionErGyldig &&
            ATPAMBidragsPligtErGyldig &&
            ATPIkkeAMBidragsPligtErGyldig &&
            PensionsindbetalingAfBeskattedeMidlerErGyldig &&
            GruppelivsForsikringErGyldig &&
            LegaterOgPersonalegoderErGyldig &&
            IndskudAldersopsparingViaArbejdsgiverErGyldig &&
            AndetPersonligIndkomstErGyldig &&
            RenteindtaegterErGyldig &&
            FitnessMusikskoleErGyldig &&
            AfstandArbejdeErGyldig &&
            AntalDageKoertErGyldig &&
            KontingentFagforeningErGyldig &&
            KontingentAKasseErGyldig &&
            GaverTilForeningerErGyldig &&
            KommuneComboBoxErGyldig &&
            KirkeskatComboBoxErGyldig;

    }

}
