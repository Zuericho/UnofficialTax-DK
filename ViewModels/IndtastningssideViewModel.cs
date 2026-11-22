using CommunityToolkit.Mvvm.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Aarsopgoerelse.ViewModels;

public partial class IndtastningssideViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial string VisAarsopgoerelse { get; set; } = "Vis min uofficielle\nÅrsopgørelse";

    private readonly CultureInfo danskKultur =
    CultureInfo.GetCultureInfo("da-DK");

    public bool IsValidNumber(string tekst, out decimal tal)
    {
        return decimal.TryParse(
            tekst,
            NumberStyles.AllowDecimalPoint,
            danskKultur,
            out tal
        );
    }

    //First element of list must be the confirming choice of being a member of the danish church.
    //Second element is the denying choice.
    public IReadOnlyList<string> KirkeskatValgmuligheder { get; } =
    new List<string> { "Ja", "Nej" };

    public string? ValgtKirkeskat { get; set; }

    public bool? BetalerKirkeskat => ValgtKirkeskat switch
    {
        var valg when valg == KirkeskatValgmuligheder[0] => true,
        var valg when valg == KirkeskatValgmuligheder[1] => false,
        _ => null
    };
}
