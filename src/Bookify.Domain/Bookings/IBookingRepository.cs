namespace Bookify.Domain;

public interface IBookingRepository {
    Task<bool> IsOverlappingAsync(Guid apartmentId, DateRange duration, CancellationToken cancellationToken = default);
    Task Add(Booking booking);
}