namespace Bookify.Domain;

public record BookingConfirmedDomainEvent(Guid BookingId): IDomainEvent;
