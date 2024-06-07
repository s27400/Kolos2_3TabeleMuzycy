namespace Kolos2_3tabele.Entities;

public class Muzyk
{
    public int IdMuzyk { get; set; }
    public string Imie { get; set; }
    public string Nazwisko { get; set; }
    public string? Pseudonim { get; set; }
    public virtual ICollection<Utwor> IdUtwory { get; set; } = new List<Utwor>();
}