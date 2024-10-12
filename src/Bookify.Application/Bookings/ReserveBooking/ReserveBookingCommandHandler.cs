using Bookify.Domain;

namespace Bookify.Application;


public sealed class ReserveBookingCommandHandler : ICommandHandler<ReserveBookingCommand, Guid>{
    private readonly IUserRepository userRepository;
    private readonly IApartmentRepository apartmentRepositor;
    private readonly IBookingRepository bookingRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly PricingService pricingService;

    public ReserveBookingCommandHandler(
        IUserRepository userRepository, 
        IApartmentRepository apartmentRepositor,
        IBookingRepository bookingRepository,
        IUnitOfWork unitOfWork,
        PricingService pricingService)
    {
        this.userRepository = userRepository;
        this.apartmentRepositor = apartmentRepositor;
        this.bookingRepository = bookingRepository;
        this.unitOfWork = unitOfWork;
        this.pricingService = pricingService;
    }
    public async Task<Result<Guid>> Handle(ReserveBookingCommand request, CancellationToken cancellationToken = default) {

        return Result.Success(null);
    }
}