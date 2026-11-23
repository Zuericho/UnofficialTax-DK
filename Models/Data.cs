using System;
using System.Collections.Generic;

namespace Aarsopgoerelse.Models;

public class Data
{
    public Dictionary<string, decimal> SkatteTal { get; set; } = new();

    public string Bopaelskommune { get; set; } = "";

    public bool? Folkekirken { get; set; } = null;
}