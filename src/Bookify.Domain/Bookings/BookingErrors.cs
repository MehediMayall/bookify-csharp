namespace Bookify.Domain;

public static class BookingErrors {
    public static Error NotFound = new("Booking.Found", "The booking with the specified identifier was not found");
    public static Error OverLap = new("Booking.Overlap", "The current booking is overlapping with an existing one");
    public static Error NotReserved = new("Booking.NotReserved", "The booking is not pending");

}