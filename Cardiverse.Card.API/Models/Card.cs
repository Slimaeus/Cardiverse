namespace Cardiverse.Card.API.Models;

public class Card
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Atk { get; set; }
    public int Def { get; set; }
    public int Hp { get; set; }
}
