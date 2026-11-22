using System.Collections.Generic;

namespace Aarsopgoerelse.Models;

public class Data
{
    public Dictionary<string, decimal> SkatteTal { get; set; } = new();

    public string Bopaelskommune { get; set; } = "";

    public decimal Kommuneskat { get; set; }

    public string Folkekirken { get; set; } = "";
    public decimal Kirkeskat { get; set; }
}