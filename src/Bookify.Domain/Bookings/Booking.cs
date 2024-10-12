using System.IO.Pipelines;

namespace Bookify.Domain;

public sealed class Booking : Entity {
    private Booking(Guid id,
        Guid apartmentId,
        Guid userId,
        DateRange duration,
        Money priceForPeriod,
        Money cleaningFee,
        Money amenitiesUpCharge,
        Money totalPrice,
        BookingStatus bookingStatus,
        DateTime createdOn        
        ) : base(id) {

        ApartmentId = apartmentId;
        UserId = userId;
        Duration = duration;
        PriceForPeriod = priceForPeriod;
        CleaningFee = cleaningFee;
        AmenitiesUpCharge = amenitiesUpCharge;
        TotalPrice = totalPrice;
        Status = bookingStatus;
        CreatedOnUtc = createdOn;

    }

    public Guid ApartmentId { get; private set; }
    public Guid UserId { get; private set; }
    public DateRange Duration { get; private set; }

    public Money PriceForPeriod { get; private set; }   
    public Money CleaningFee { get; private set; }   
    public Money AmenitiesUpCharge { get; private set; }   
    public Money TotalPrice { get; private set; }   
    public BookingStatus Status { get; private set; }   
    public DateTime CreatedOnUtc { get; private set; }   
    public DateTime ConfirmedOnUtc { get; private set; }   
    public DateTime RejectedOnUtc { get; private set; }   
    public DateTime CompletedOnUtc { get; private set; }   
    public DateTime CancelledOnUtc { get; private set; }   


    public static Booking Reserve(
        Guid userId,
        Apartment apartment,
        DateRange duration,
        DateTime utcNow){

        PricingDetails pricingDetails = new PricingService().CalculatePrice(
            apartment,
            duration
        );

        var booking = new Booking(
            Guid.NewGuid(),
            apartment.Id,
            userId,
            duration,
            pricingDetails.PriceForPeriod,
            pricingDetails.CleaningFee,
            pricingDetails.AmenitiesUpCharge,
            pricingDetails.TotalPrice,
            BookingStatus.Reserved,
            utcNow
        );

        booking.RaiseDomainEvent(new BookingReservedDomainEvent(booking.Id));
        return booking;
    }
    public Result Confirm() {
        if ( Status != BookingStatus.Reserved ) 
            return Result.Failure(BookingErrors.NotReserved);

        Status = BookingStatus.Confirmed;
        ConfirmedOnUtc = DateTime.UtcNow;
        RaiseDomainEvent(new BookingConfirmedDomainEvent(Id));

        return Result.Success();
    }

    public Result Reject() {
        if ( Status != BookingStatus.Reserved ) 
            return Result.Failure(BookingErrors.NotReserved);
        
        Status = BookingStatus.Rejected;
        RejectedOnUtc = DateTime.UtcNow;

        RaiseDomainEvent(new BookingRejectedDomainEvent(Id));

        return Result.Success();
    }

    public Result Cancel() {
        if ( Status != BookingStatus.Reserved ) 
            return Result.Failure(BookingErrors.NotReserved);
        
        Status = BookingStatus.Cancelled;
        CancelledOnUtc = DateTime.UtcNow;

        RaiseDomainEvent(new BookingCancelledDomainEvent(Id));

        return Result.Success();
    }

    public Result Complete() {
        if ( Status != BookingStatus.Reserved ) 
            return Result.Failure(BookingErrors.NotReserved);
        
        Status = BookingStatus.Completed;
        CompletedOnUtc = DateTime.UtcNow;

        RaiseDomainEvent(new BookingCompletedDomainEvent(Id));

        return Result.Success();
    }

}