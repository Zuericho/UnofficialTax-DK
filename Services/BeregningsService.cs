using System.Linq;
using Aarsopgoerelse.Models;

namespace Aarsopgoerelse.Services;

public class BeregningsService
{
    public decimal BeregnSum(Data userData)
    {
        return userData.SkatteTal.Values.Sum();
    }
}