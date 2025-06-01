namespace Tutorial12.DTOs;

public class TripDTO
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int MaxPeople { get; set; }
    public List<CountryDTO> Countries { get; set; } = [];
    public List<ClientDTO> Clients { get; set; } = [];
}