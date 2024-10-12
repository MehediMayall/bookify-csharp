namespace Bookify.Domain;

public record BookingReservedDomainEvent(Guid BookingId): IDomainEvent;
