namespace UnitConversionAPI.Services;

public class ConversionService
{
    // All units convert TO a base unit first, then FROM base to target
    // Base: meter (length), celsius (temp), kilogram (weight)
    private static readonly Dictionary<string, double> ToBase = new(StringComparer.OrdinalIgnoreCase)
    {
        // Length → meters
        ["meter"] = 1,
        ["metre"] = 1,
        ["m"] = 1,
        ["kilometer"] = 1000,
        ["km"] = 1000,
        ["centimeter"] = 0.01,
        ["cm"] = 0.01,
        ["millimeter"] = 0.001,
        ["mm"] = 0.001,
        ["foot"] = 0.3048,
        ["feet"] = 0.3048,
        ["ft"] = 0.3048,
        ["inch"] = 0.0254,
        ["inches"] = 0.0254,
        ["in"] = 0.0254,
        ["yard"] = 0.9144,
        ["yd"] = 0.9144,
        ["mile"] = 1609.344,
        ["mi"] = 1609.344,

        // Weight → kilograms
        ["kilogram"] = 1,
        ["kg"] = 1,
        ["gram"] = 0.001,
        ["g"] = 0.001,
        ["pound"] = 0.453592,
        ["lb"] = 0.453592,
        ["lbs"] = 0.453592,
        ["ounce"] = 0.0283495,
        ["oz"] = 0.0283495,
        ["ton"] = 1000,
    };

    public (bool success, double result, string error) Convert(double value, string from, string to)
    {
        // Temperature is special — uses formulas, not factors
        if (IsTemperature(from) && IsTemperature(to))
        {
            double celsius = ToCelsius(value, from);
            return (true, FromCelsius(celsius, to), "");
        }

        if (!ToBase.TryGetValue(from, out double fromFactor))
            return (false, 0, $"Unknown unit: '{from}'");

        if (!ToBase.TryGetValue(to, out double toFactor))
            return (false, 0, $"Unknown unit: '{to}'");

        return (true, value * fromFactor / toFactor, "");
    }

    private static bool IsTemperature(string unit) =>
        unit.Equals("celsius", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("fahrenheit", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("kelvin", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("c", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("f", StringComparison.OrdinalIgnoreCase) ||
        unit.Equals("k", StringComparison.OrdinalIgnoreCase);

    private static double ToCelsius(double value, string unit) => unit.ToLower() switch
    {
        "fahrenheit" or "f" => (value - 32) * 5 / 9,
        "kelvin" or "k" => value - 273.15,
        _ => value
    };

    private static double FromCelsius(double celsius, string unit) => unit.ToLower() switch
    {
        "fahrenheit" or "f" => celsius * 9 / 5 + 32,
        "kelvin" or "k" => celsius + 273.15,
        _ => celsius
    };
}