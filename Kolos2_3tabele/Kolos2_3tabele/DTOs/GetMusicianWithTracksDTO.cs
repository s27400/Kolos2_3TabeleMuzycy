namespace Kolos2_3tabele.DTOs;

public class GetMusicianWithTracksDTO
{
    public int IdMuzyk { get; set; }
    public string Imie { get; set; }
    public string Nazwisko { get; set; }
    public string? Pseudonim { get; set; }
    public IEnumerable<TrackDTO> Utwory { get; set; }
}