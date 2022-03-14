using System.Diagnostics;
using System.Globalization;
using MarketDataParser;

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

WaitForBeginInput();
var markets = InitMarketCollectors();
await new MarketDataProcessor(markets).Process(Environment.ProcessorCount);
OutResults(markets);

void WaitForBeginInput()
{
    string? line = null;
    var counter = 0;
    do
    {
        if (counter > 10)
        {
            Console.WriteLine("BEGIN command hasn't been provided.");
            Environment.Exit(10);
        }

        line = Console.ReadLine();
    } while (line != "BEGIN");

}

static MarketDataCollector[] InitMarketCollectors()
{
    var markets = new MarketDataCollector[12001];
    for (int i = 0; i < markets.Length; i++)
    {
        markets[i] = new MarketDataCollector(i);
    }

    return markets;
}

static void OutResults(MarketDataCollector[] markets)
{
    foreach (var market in markets)
    {
        if (market.TotalVolume == 0)
        {
            continue;
        }

        var stat = market.CalculateStatistics();
        Console.WriteLine(stat.Serialize());
    }
}