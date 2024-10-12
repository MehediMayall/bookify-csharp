using Bookify.Domain;
using MediatR;

namespace Bookify.Application;

internal sealed class BookingReserveDomainEventHandler : INotificationHandler<BookingReservedDomainEvent> {
    private readonly IBookingRepository bookingRepository;
    private readonly IUserRepository userRepository;
    private readonly IEmailService emailService;

    public BookingReserveDomainEventHandler(
        IBookingRepository bookingRepository, 
        IUserRepository userRepository,
        IEmailService emailService
        )
    {
        this.bookingRepository = bookingRepository;
        this.userRepository = userRepository;
        this.emailService = emailService;
    }
    public async Task Handle(BookingReservedDomainEvent notification, CancellationToken cancellationToken = default) {
        var booking = await bookingRepository.GetByIdAsync(notification.BookingId, cancellationToken);

        if (booking is null) 
            return;

        var user = await userRepository.GetByIdAsync(booking.UserId, cancellationToken);
        if (user is null) 
            return;
        
        await emailService.SendAsync(user.Email.ToString(), "Booking Reserved", "You have 10 minutes to confirm this booking");
    }
}