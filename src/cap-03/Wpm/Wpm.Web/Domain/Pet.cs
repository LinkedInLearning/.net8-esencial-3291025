using VitalSigns = (decimal Temperature, int Bpm, int Rpm, System.DateTime date);
namespace Wpm.Web.Domain;
public class Pet
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int? Age { get; set; }
    public decimal? Weight { get; set; }
    public string? PhotoUrl { get; set; }
    public int BreedId { get; set; }
    public Breed Breed { get; set; }
    public List<Owner> Owners { get; set; } = new();

    private List<VitalSigns> vitalSigns;
    public IReadOnlyCollection<VitalSigns> VitalSigns => vitalSigns?.AsReadOnly();

    public void AddVitalSigns(ref readonly VitalSigns vs)
    {
        vitalSigns.Add(vs);
    }
}