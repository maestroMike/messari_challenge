using System.Runtime.CompilerServices;
using System.Threading.Channels;

namespace MarketDataParser;

public class MarketDataProcessor_Simplified
{
    private readonly MarketDataCollector[] markets;
    private readonly StrongBox<bool> processingFinished;

    public MarketDataProcessor_Simplified(MarketDataCollector[] markets)
    {
        this.markets = markets;
        this.processingFinished = new StrongBox<bool>(false);
    }

    public Task Process(int workersCount)
    {
        var tasks = Enumerable.Range(0, workersCount).Select(x => ReadInput()).ToArray();
        return Task.WhenAll(tasks);
    }

    private async Task ReadInput()
    {
        await Task.Yield();
        var input = Console.In;
        var line = input.ReadLine();
        while (true)
        {
            if (!string.IsNullOrEmpty(line))
            {
                var tradeData = TradeInfoParser.Parse(line);
                this.markets[tradeData.MarketId].Add(tradeData.Volume, tradeData.Price, tradeData.IsBuy);
            }

            lock (input)
            {
                if(this.processingFinished.Value)
                    return;
                    
                line = input.ReadLine();
                if(line == "END")
                {
                    this.processingFinished.Value = true;
                    return;
                }
            }
        }
    }
}