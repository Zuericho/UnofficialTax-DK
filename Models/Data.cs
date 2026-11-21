namespace Aarsopgoerelse.Models;
using System.Collections.Generic;

public class Data
{
    public Dictionary<string, decimal> Tal { get; set; } = new();

    public string Bopaelskommune { get; set; } = "";

    public string Folkekirken { get; set; } = "";
}