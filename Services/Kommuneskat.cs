
using System.Linq;
using System.Collections.Generic;
using System;

namespace Aarsopgoerelse.Services;
public static class KommuneskatSatser
{

private static readonly Dictionary<string,
    (decimal Kommune, decimal Kirke, decimal Nedslag, decimal Grundskyld)> Satser =
    new(StringComparer.OrdinalIgnoreCase)
{
    ["København"] = (0.233900m, 0.008000m, 0.000000m, 0.005100m),
    ["Frederiksberg"] = (0.245000m, 0.005000m, 0.000000m, 0.003100m),
    ["Ballerup"] = (0.255000m, 0.007500m, 0.004400m, 0.008300m),
    ["Brøndby"] = (0.243000m, 0.008000m, 0.000000m, 0.004900m),
    ["Dragør"] = (0.248000m, 0.006100m, 0.000000m, 0.008100m),
    ["Gentofte"] = (0.241400m, 0.003800m, 0.000000m, 0.005100m),
    ["Gladsaxe"] = (0.236000m, 0.007500m, 0.000000m, 0.005900m),
    ["Glostrup"] = (0.246000m, 0.008000m, 0.000000m, 0.006500m),
    ["Herlev"] = (0.237000m, 0.008000m, 0.000000m, 0.005400m),
    ["Albertslund"] = (0.256000m, 0.008000m, 0.005400m, 0.009100m),
    ["Hvidovre"] = (0.254000m, 0.007200m, 0.003400m, 0.006500m),
    ["Høje-Taastrup"] = (0.246000m, 0.008000m, 0.000000m, 0.006300m),
    ["Lyngby-Taarbæk"] = (0.243800m, 0.006000m, 0.000000m, 0.006700m),
    ["Rødovre"] = (0.257000m, 0.007200m, 0.006400m, 0.007800m),
    ["Ishøj"] = (0.250000m, 0.009000m, 0.000000m, 0.006500m),
    ["Tårnby"] = (0.241000m, 0.006100m, 0.000000m, 0.006000m),
    ["Vallensbæk"] = (0.256000m, 0.008000m, 0.005400m, 0.008600m),
    ["Furesø"] = (0.248800m, 0.007000m, 0.000000m, 0.007000m),
    ["Allerød"] = (0.253000m, 0.005800m, 0.002400m, 0.008000m),
    ["Fredensborg"] = (0.253000m, 0.006400m, 0.002400m, 0.008300m),
    ["Helsingør"] = (0.258200m, 0.006300m, 0.007600m, 0.009500m),
    ["Hillerød"] = (0.256000m, 0.006900m, 0.005400m, 0.006600m),
    ["Hørsholm"] = (0.237000m, 0.006200m, 0.000000m, 0.008500m),
    ["Rudersdal"] = (0.234700m, 0.005700m, 0.000000m, 0.009400m),
    ["Egedal"] = (0.257000m, 0.007600m, 0.006400m, 0.008800m),
    ["Frederikssund"] = (0.256000m, 0.009600m, 0.005400m, 0.012000m),
    ["Greve"] = (0.245900m, 0.008100m, 0.000000m, 0.005500m),
    ["Køge"] = (0.252600m, 0.008700m, 0.002000m, 0.005300m),
    ["Halsnæs"] = (0.257000m, 0.008500m, 0.006400m, 0.010900m),
    ["Roskilde"] = (0.252000m, 0.008400m, 0.001400m, 0.007400m),
    ["Solrød"] = (0.249900m, 0.008400m, 0.000000m, 0.007900m),
    ["Gribskov"] = (0.254000m, 0.008500m, 0.003400m, 0.011100m),
    ["Odsherred"] = (0.263000m, 0.009800m, 0.012400m, 0.012300m),
    ["Holbæk"] = (0.253000m, 0.009600m, 0.002400m, 0.008100m),
    ["Faxe"] = (0.258000m, 0.010800m, 0.007400m, 0.008000m),
    ["Kalundborg"] = (0.242000m, 0.010100m, 0.000000m, 0.016600m),
    ["Ringsted"] = (0.261000m, 0.009500m, 0.010400m, 0.007000m),
    ["Slagelse"] = (0.261000m, 0.009600m, 0.010400m, 0.011100m),
    ["Stevns"] = (0.260000m, 0.011000m, 0.009400m, 0.008300m),
    ["Sorø"] = (0.263000m, 0.009500m, 0.012400m, 0.009800m),
    ["Lejre"] = (0.253100m, 0.010500m, 0.002500m, 0.009600m),
    ["Lolland"] = (0.263000m, 0.012300m, 0.012400m, 0.017400m),
    ["Næstved"] = (0.250000m, 0.009800m, 0.000000m, 0.009800m),
    ["Guldborgsund"] = (0.258000m, 0.011600m, 0.007400m, 0.015900m),
    ["Vordingborg"] = (0.263000m, 0.010200m, 0.012400m, 0.013500m),
    ["Bornholm"] = (0.262000m, 0.009300m, 0.011400m, 0.010700m),
    ["Middelfart"] = (0.258000m, 0.009000m, 0.007400m, 0.006800m),
    ["Assens"] = (0.261000m, 0.009800m, 0.010400m, 0.011200m),
    ["Faaborg-Midtfyn"] = (0.261000m, 0.010500m, 0.010400m, 0.008500m),
    ["Kerteminde"] = (0.261000m, 0.009800m, 0.010400m, 0.012400m),
    ["Nyborg"] = (0.263000m, 0.010000m, 0.012400m, 0.012800m),
    ["Odense"] = (0.255000m, 0.006800m, 0.004400m, 0.005700m),
    ["Svendborg"] = (0.263000m, 0.010200m, 0.012400m, 0.008800m),
    ["Nordfyns"] = (0.260000m, 0.010400m, 0.009400m, 0.015100m),
    ["Langeland"] = (0.263000m, 0.011400m, 0.012400m, 0.012200m),
    ["Ærø"] = (0.261000m, 0.010700m, 0.010400m, 0.011100m),
    ["Haderslev"] = (0.263000m, 0.009500m, 0.012400m, 0.012700m),
    ["Billund"] = (0.240000m, 0.008900m, 0.000000m, 0.007200m),
    ["Sønderborg"] = (0.257000m, 0.009100m, 0.006400m, 0.013500m),
    ["Tønder"] = (0.253000m, 0.011600m, 0.002400m, 0.010800m),
    ["Esbjerg"] = (0.261000m, 0.008100m, 0.010400m, 0.009900m),
    ["Fanø"] = (0.261000m, 0.011400m, 0.010400m, 0.012500m),
    ["Varde"] = (0.251000m, 0.009500m, 0.000400m, 0.017700m),
    ["Vejen"] = (0.258000m, 0.010600m, 0.007400m, 0.009000m),
    ["Aabenraa"] = (0.256000m, 0.009500m, 0.005400m, 0.007800m),
    ["Fredericia"] = (0.255000m, 0.008800m, 0.004400m, 0.013000m),
    ["Horsens"] = (0.256900m, 0.007900m, 0.006300m, 0.008700m),
    ["Kolding"] = (0.255000m, 0.009200m, 0.004400m, 0.011100m),
    ["Vejle"] = (0.234000m, 0.008900m, 0.000000m, 0.010500m),
    ["Herning"] = (0.254000m, 0.009900m, 0.003400m, 0.009900m),
    ["Holstebro"] = (0.255000m, 0.010800m, 0.004400m, 0.011500m),
    ["Lemvig"] = (0.257000m, 0.012700m, 0.006400m, 0.015400m),
    ["Struer"] = (0.253000m, 0.012000m, 0.002400m, 0.015500m),
    ["Syddjurs"] = (0.259000m, 0.009800m, 0.008400m, 0.014200m),
    ["Norddjurs"] = (0.260000m, 0.010000m, 0.009400m, 0.016800m),
    ["Favrskov"] = (0.257000m, 0.009600m, 0.006400m, 0.010600m),
    ["Odder"] = (0.251000m, 0.009500m, 0.000400m, 0.014400m),
    ["Randers"] = (0.260000m, 0.008900m, 0.009400m, 0.013900m),
    ["Silkeborg"] = (0.255000m, 0.009400m, 0.004400m, 0.011000m),
    ["Samsø"] = (0.259000m, 0.012000m, 0.008400m, 0.010500m),
    ["Skanderborg"] = (0.260000m, 0.008600m, 0.009400m, 0.008600m),
    ["Aarhus"] = (0.245200m, 0.007400m, 0.000000m, 0.006000m),
    ["Ikast-Brande"] = (0.251000m, 0.009700m, 0.000400m, 0.009100m),
    ["Ringkøbing-Skjern"] = (0.250000m, 0.010500m, 0.000000m, 0.016400m),
    ["Hedensted"] = (0.255200m, 0.009800m, 0.004600m, 0.007900m),
    ["Morsø"] = (0.258000m, 0.012000m, 0.007400m, 0.015300m),
    ["Skive"] = (0.255000m, 0.010900m, 0.004400m, 0.011600m),
    ["Thisted"] = (0.255000m, 0.012700m, 0.004400m, 0.011200m),
    ["Viborg"] = (0.255000m, 0.009300m, 0.004400m, 0.011500m),
    ["Brønderslev"] = (0.263000m, 0.010600m, 0.012400m, 0.017100m),
    ["Frederikshavn"] = (0.262000m, 0.010300m, 0.011400m, 0.013400m),
    ["Vesthimmerland"] = (0.263000m, 0.011800m, 0.012400m, 0.009300m),
    ["Læsø"] = (0.263000m, 0.013000m, 0.012400m, 0.012700m),
    ["Rebild"] = (0.258300m, 0.012000m, 0.007700m, 0.008900m),
    ["Mariagerfjord"] = (0.259000m, 0.011500m, 0.008400m, 0.015700m),
    ["Jammerbugt"] = (0.257000m, 0.012000m, 0.006400m, 0.013200m),
    ["Aalborg"] = (0.256000m, 0.009800m, 0.005400m, 0.007400m),
    ["Hjørring"] = (0.262100m, 0.011900m, 0.011500m, 0.016500m)
};

    public static decimal HentKommuneskat(string kommunenavn)
    {
        return Satser.TryGetValue(kommunenavn, out var satser)
            ? satser.Kommune
            : 0m;
    }

    public static decimal HentKirkeskat(string kommunenavn)
    {
        return Satser.TryGetValue(kommunenavn, out var satser)
            ? satser.Kirke
            : 0m;
    }

    public static decimal HighestKirkeskat()
    {
        return Satser.Values.Max(x => x.Kirke);
    }

    public static decimal LowesKirkeskat()
    {
        return Satser.Values.Min(x => x.Kirke);
    }

    public static decimal AverageKirkeskat()
    {
        return Satser.Values.Average(x => x.Kirke);
    }

    public static decimal HentNedslag(string kommunenavn)
{
    return Satser.TryGetValue(kommunenavn, out var satser)
        ? satser.Nedslag
        : 0m;
}

public static decimal HentGrundskyld(string kommunenavn)
{
    return Satser.TryGetValue(kommunenavn, out var satser)
        ? satser.Grundskyld
        : 0m;
}

    public static IReadOnlyList<string> Kommunenavne =>
    Satser.Keys.OrderBy(navn => navn).ToList();
}
