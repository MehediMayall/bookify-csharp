using Bookify.Domain;

namespace Bookify.Application;


public sealed class ReserveBookingCommandHandler : ICommandHandler<ReserveBookingCommand, Guid>{
    private readonly IUserRepository userRepository;
    private readonly IApartmentRepository apartmentRepositor;
    private readonly IBookingRepository bookingRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly PricingService pricingService;
    private readonly IDateTimeProvider dateTimeProvider;

    public ReserveBookingCommandHandler(
        IUserRepository userRepository, 
        IApartmentRepository apartmentRepositor,
        IBookingRepository bookingRepository,
        IUnitOfWork unitOfWork,
        PricingService pricingService,
        IDateTimeProvider dateTimeProvider
        )
    {
        this.userRepository = userRepository;
        this.apartmentRepositor = apartmentRepositor;
        this.bookingRepository = bookingRepository;
        this.unitOfWork = unitOfWork;
        this.pricingService = pricingService;
        this.dateTimeProvider = dateTimeProvider;
    }
    public async Task<Result<Guid>> Handle(ReserveBookingCommand request, CancellationToken cancellationToken = default) {

        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null) {
            return Result<Guid>.Failure<Guid>(UserErrors.NotFound(request.UserId));
        }

        var apartment = await apartmentRepositor.GetByIdAsync(request.ApartmentId, cancellationToken);
        if (apartment is null) {
            return Result<Guid>.Failure<Guid>(ApartmentErrors.NotFound(request.ApartmentId));
        }

        var duration = DateRange.Create(request.StartDate, request.EndDate);   

        // Checking optimistic checking
        if ( await bookingRepository.IsOverlappingAsync(apartment.Id, duration, cancellationToken) ) {
            return Result.Failure<Guid>(BookingErrors.OverLap);
        }

        var booking = Booking.Reserve(
            request.UserId,
            apartment,
            duration,
            dateTimeProvider.UtcNow
        );

        await bookingRepository.Add(booking);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return booking.Id;
    }
}