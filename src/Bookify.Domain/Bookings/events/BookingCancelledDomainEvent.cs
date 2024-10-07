namespace Bookify.Domain;

public record BookingCancelledDomainEvent(Guid BookingId): IDomainEvent;
