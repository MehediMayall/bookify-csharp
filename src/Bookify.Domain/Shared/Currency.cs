namespace Bookify.Domain;


public sealed record Currency{
    public string Code {get; init;}

    private Currency(string code) => Code = code;

    public static readonly Currency USD = new("USD");
    public static readonly Currency EUR = new("EUR");

    public static Currency FromCode(string code) {
        return All.FirstOrDefault(c=> c.Code == code) ?? 
        throw new ApplicationException("The currency code is invalid") ;
    } 

    public static readonly IReadOnlyCollection<Currency> All = new[] {
        USD,
        EUR,
    };

}