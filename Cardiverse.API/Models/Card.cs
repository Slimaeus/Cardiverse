namespace Cardiverse.API.Models;

public class Card
{
    // Card attributes
    public string Name { get; set; }
    public decimal DawnStrength { get; set; }
    public decimal TwilightFury { get; set; }
    public decimal ApexDominance { get; set; }
    public decimal ResilientResolve { get; set; }
    public decimal MarketInfluence { get; set; }
    public decimal HarmonicMomentum { get; set; }

    // Constructor
    public Card(string name, decimal dawnStrength, decimal twilightFury, decimal apexDominance, decimal resilientResolve, decimal marketInfluence, decimal harmonicMomentum)
    {
        Name = name;
        DawnStrength = dawnStrength;
        TwilightFury = twilightFury;
        ApexDominance = apexDominance;
        ResilientResolve = resilientResolve;
        MarketInfluence = marketInfluence;
        HarmonicMomentum = harmonicMomentum;
    }

    // Additional methods or properties can be added as needed
}
