namespace Kolos2_3tabele.Entities;

public class Utwor
{
    public int IdUtwor { get; set; }
    public string NazwaUtworu { get; set; }
    public float CzasTrwania { get; set; }
    public virtual ICollection<Muzyk> IdMuzycy { get; set; } = new List<Muzyk>();
}