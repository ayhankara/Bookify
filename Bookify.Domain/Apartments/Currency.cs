namespace Bookify.Domain.Apartments;

public record Currency
{
    public static readonly Currency USD = new("USD");
    public static readonly Currency EUR = new("EUR");
    public static readonly Currency GBP = new("GBP");
    public static readonly Currency TRY = new("TRY");
    public static readonly Currency None = new("");

    private Currency(string code) => Code = code;
 
    public string Code { get; private set; } = string.Empty;

    public static Currency FromCode(string code)
    {
        return code switch
        {
            "USD" => USD,
            "EUR" => EUR,
            "GBP" => GBP,
            "TRY" => TRY,
            _ => throw new ArgumentException($"Unknown currency code: {code}", nameof(code))
        };
    }

    public static readonly IReadOnlyCollection<Currency> All = new List<Currency>
    {
        USD,
        EUR,
        GBP,
        TRY
    }.AsReadOnly(); 
}