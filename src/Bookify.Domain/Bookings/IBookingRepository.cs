namespace Bookify.Domain;

public interface IBookingRepository {
    Task<Booking> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> IsOverlappingAsync(Guid apartmentId, DateRange duration, CancellationToken cancellationToken = default);
    Task Add(Booking booking);
}