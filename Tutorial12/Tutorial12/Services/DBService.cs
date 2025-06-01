using Microsoft.EntityFrameworkCore;
using Tutorial12.Data;
//using Tutorial12.Data;
using Tutorial12.DTOs;
using Tutorial12.Models;
using Tutorial9.Exceptions;

namespace Tutorial12.Services;

public class DBService : IDBService
{
    private readonly MasterContext _context;
    
    public DBService(MasterContext context)
    {
        _context = context;
    }
    
    public async Task<TripResponseDTO> GetTripsAsync(int page, int pageSize)
    {
        if (page < 1) throw new BadHttpRequestException("Page must be greater than 0");
        
        var totalTrips = await _context.Trips.CountAsync();
    
        var trips = await _context.Trips
            .Include(t => t.IdCountries)
            .Include(t => t.ClientTrips)
            .ThenInclude(ct => ct.IdClientNavigation)
            .OrderByDescending(t => t.DateFrom)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(t => new TripDTO
            {
                Name = t.Name,
                Description = t.Description,
                DateFrom = t.DateFrom,
                DateTo = t.DateTo,
                MaxPeople = t.MaxPeople,
                Countries = t.IdCountries.Select(c => new CountryDTO
                {
                    Name = c.Name
                }).ToList(),
                Clients = t.ClientTrips.Select(ct => new ClientDTO
                {
                    FirstName = ct.IdClientNavigation.FirstName,
                    LastName = ct.IdClientNavigation.LastName
                }).ToList()
            })
            .ToListAsync();
    
        return new TripResponseDTO
        {
            PageNum = page,
            PageSize = pageSize,
            AllPages = (int)Math.Ceiling(totalTrips / (double)pageSize),
            Trips = trips
        };
    }
    
    public async Task DeleteClientAsync(int idClient)
    {
        var client = await _context.Clients
            .Include(c => c.ClientTrips)
            .FirstOrDefaultAsync(c => c.IdClient == idClient);

        if (client is null)
            throw new NotFoundException("Client not found.");

        if (client.ClientTrips.Any())
            throw new ConflictException("Client has assigned trips and cannot be deleted.");

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
    }
    
    public async Task RegisterClientToTripAsync(int idTrip, RegisterClientToTripDTO dto)
    {
        var trip = await _context.Trips.FirstOrDefaultAsync(t => t.IdTrip == idTrip);

        if (trip is null)
            throw new NotFoundException("Trip not found.");

        if (trip.DateFrom <= DateTime.Now)
            throw new ConflictException("This trip has already started. You cannot register for it.");

        var client = await _context.Clients.FirstOrDefaultAsync(c => c.Pesel == dto.Pesel);

        if (client is null)
        {
            client = new Client
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Telephone = dto.Telephone,
                Pesel = dto.Pesel
            };
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new BadHttpRequestException("Client already exists.");
        }

        
        var clientTrip = new ClientTrip
        {
            IdClient = client.IdClient,
            IdTrip = trip.IdTrip,
            RegisteredAt = DateTime.Now,
            PaymentDate = dto.PaymentDate
        };

        _context.ClientTrips.Add(clientTrip);
        await _context.SaveChangesAsync();
    }
}