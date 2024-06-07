namespace Kolos2_3tabele.DTOs;

public class AddMusicianDTO
{
    public string Imie { get; set; }
    public string Nazwisko { get; set; }
    public string? Pseudonim { get; set; }
    public int IdUtwor { get; set; } = 0;
    public string NazwaUtworu { get; set; }
    public float CzasTrwania { get; set; }
}