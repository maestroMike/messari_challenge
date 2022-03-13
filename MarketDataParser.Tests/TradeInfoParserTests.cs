using Xunit;
using MarketDataParser;
using FluentAssertions;
using System.Globalization;

namespace MarketDataParser.Tests;

public class TradeInfoParserTests
{
    [Fact]
    public void ShouldParseLine()
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        var line = "{\"id\":121509,\"market\":5773,\"price\":1.234,\"volume\":1234.56,\"is_buy\":true}";
        var expected = new TradeInfo(121509, 5773, 1.234m, 1234.56m, true); 
        var actual = TradeInfoParser.Parse(line);

        actual.Should().Be(expected);
    }
}