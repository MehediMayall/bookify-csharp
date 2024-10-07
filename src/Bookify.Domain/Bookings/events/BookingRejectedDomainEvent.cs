namespace Bookify.Domain;

public record BookingRejectedDomainEvent(Guid BookingId): IDomainEvent;
