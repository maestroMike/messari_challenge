using System.Threading.Channels;

namespace MarketDataParser;

public class MarketDataProcessor
{
    private readonly Channel<string> inChannel;
    private readonly MarketDataCollector[] markets;

    public MarketDataProcessor(MarketDataCollector[] markets)
    {
        this.inChannel = CreateChannel(true, false);
        this.markets = markets;
    }

    public async Task Process(int workersCount)
    {
        var readTask = ReadInputAsync();
        RunProcessingWorkers(workersCount);
        await readTask;
        await this.inChannel.Reader.Completion;
    }

    private async Task ReadInputAsync()
    {
        await Task.Yield();
        var input = Console.In;
        var line = await input.ReadLineAsync();
        while (line != "END")
        {
            if (!string.IsNullOrEmpty(line))
            {
                await this.inChannel.Writer.WriteAsync(line);
            }

            line = await input.ReadLineAsync();
        }

        this.inChannel.Writer.Complete();
    }

    private static Channel<string> CreateChannel(bool singleWriter, bool singleReader)
    {
        var options = new UnboundedChannelOptions()
        {
            AllowSynchronousContinuations = true,
            SingleWriter = singleWriter,
            SingleReader = singleReader
        };
        var inChannel = Channel.CreateUnbounded<string>(options);
        return inChannel;
    }

    private Task[] RunProcessingWorkers(int workersCount)
    {
        return Enumerable.Range(0, workersCount).Select(x => ProcessInputAsync()).ToArray();
    }

    private async Task ProcessInputAsync()
    {
        await Task.Yield();
        await foreach (var input in this.inChannel.Reader.ReadAllAsync())
        {
            await Task.Yield();
            var tradeData = TradeInfoParser.Parse(input);
            this.markets[tradeData.MarketId].Add(tradeData.Volume, tradeData.Price, tradeData.IsBuy);
        }
    }
}