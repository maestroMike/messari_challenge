using System.Globalization;
namespace MarketDataParser;

public static class TradeInfoParser
{
    public static TradeInfo Parse(string str)
    {
        // {"id":121509,"market":5773,"price":1.234,"volume":1234.56,"is_buy":true}
        // 6 - id - 10 - market - 9 - price - 10 - volume -10 - isBuy
        int[] indexes = new[] {6,10,9,10,10};

        var span = ParseSegment(str.AsSpan(), 6, out var id);
        span = ParseSegment(span, 9, out var marketId);
        span = ParseSegment(span, 8, out var price);
        span = ParseSegment(span, 9, out var vol);
        
        return new TradeInfo(
            int.Parse(id), 
            int.Parse(marketId), 
            decimal.Parse(price, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture),
            decimal.Parse(vol, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture), 
            span[9] == 't' || span[9] == 'T'
        );
    }

    private static ReadOnlySpan<char> ParseSegment(ReadOnlySpan<char> segment, int skipChars, out ReadOnlySpan<char> val, char ch = ',')
    {
        var commaIndex = segment.IndexOf(ch);
        val = segment.Slice(skipChars, commaIndex - skipChars);
        var nextSegment = segment.Slice(commaIndex + 1);
        
        return nextSegment;
    }
}

public readonly struct TradeInfo
{
    public TradeInfo(int id, int marketId, decimal price, decimal volume, bool isBuy) : this()
    {
        MarketId = marketId;
        Price = price;
        Volume = volume;
        IsBuy = isBuy;
        Id = id;
    }

    public readonly int Id;
    
    public readonly int MarketId;

    public readonly decimal Price;

    public readonly decimal Volume;

    public readonly bool IsBuy;
}