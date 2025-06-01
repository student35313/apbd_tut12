using Tutorial12.DTOs;

namespace Tutorial12.Services;

public interface IDBService
{
    Task<TripResponseDTO> GetTripsAsync(int page, int pageSize);
    Task DeleteClientAsync(int idClient);
    Task RegisterClientToTripAsync(int idTrip, RegisterClientToTripDTO dto);
}