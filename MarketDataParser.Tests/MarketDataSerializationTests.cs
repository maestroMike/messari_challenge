using Xunit;
using MarketDataParser;
using FluentAssertions;
using System.Globalization;

namespace MarketDataParser.Tests;

public class MarketDataSerializationTest
{
    [Fact]
    public void ShouldSerializeResult()
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        var obj = new MarketStatistics(){
            Id = 111,
            TotalVolume = 123.456m,
            MeanPrice = 345.34m,
            MeanVolume = 3.333m,
            VolumeWeightedAvgPrice = 123.55m,
            PercentageBuy = 0.44m
        };

        var expected = "{\"market\":111,\"total_volume\":123.456,\"mean_price\":345.34,\"mean_volume\":3.333,\"volume_weighted_average_price\":123.55,\"percentage_buy\":0.44}";

        var actual = obj.Serialize();

        actual.Should().Be(expected);
    }
}