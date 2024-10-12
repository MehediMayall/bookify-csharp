namespace Bookify.Domain;

public static class ApartmentErrors{
    public static Error NotFound(Guid Id) => new("Apartment.NotFound", "Apartment not found with the apartment Id: " + Id);
}