namespace MarketDataParser;

public class MarketDataCollector
{
    public MarketDataCollector(int id)
    {
        Id = id;
    }

    public int Id {get; }
    public int BuyCount { get; private set; }
    public int SellCount { get; private set; }
    public decimal TotalVolume {get; private set; }
    public decimal TotalPrice {get; private set; }
    public decimal TotalWeightedPrice {get; private set; }

    public void Add(decimal volume, decimal price, bool isBuy)
    {
        lock(this)
        {
            TotalVolume += volume;
            TotalPrice += price;
            TotalWeightedPrice += volume*price;
            if(isBuy)
                BuyCount++;
            else
                SellCount++;
        }
    }

    public MarketStatistics CalculateStatistics()
    {
        var tradesCount = BuyCount + SellCount;
        var res = new MarketStatistics();
        res.Id = Id;
        res.TotalVolume = TotalVolume;
        res.VolumeWeightedAvgPrice = TotalWeightedPrice / TotalVolume;
        res.MeanPrice = TotalPrice / tradesCount;
        res.MeanVolume = TotalVolume / tradesCount;
        res.PercentageBuy = ((decimal) BuyCount) / tradesCount;
        return res;
    }
}