using Xunit;
using MarketDataParser;
using FluentAssertions;

namespace MarketDataParser.Tests;

public class MarketDataCollectorTest
{
    [Fact]
    public void ShouldCollectStatistics()
    {
        var expected = new MarketStatistics(){
            Id = 111,
            TotalVolume = 19.3m,
            MeanPrice = 99.075m,
            MeanVolume = 4.825m,
            VolumeWeightedAvgPrice = 95.00518134715025906735751295m,
            PercentageBuy = 0.75m
        };

        var collector = new MarketDataCollector(111);
        collector.Add(10.3m, 90m, true);
        collector.Add(5m, 100.5m, true);
        collector.Add(3.5m, 100.4m, false);
        collector.Add(0.5m, 105.4m, true);

        var actual = collector.CalculateStatistics();

        actual.Should().BeEquivalentTo(expected);
    }
}