namespace MarketDataParser;

public class MarketStatistics
{
    public int Id { get; set; }

    public decimal TotalVolume { get; set; }  

    public decimal MeanPrice { get; set; }

    public decimal MeanVolume { get; set; }

    public decimal VolumeWeightedAvgPrice { get; set; }

    public decimal PercentageBuy {get; set;} 

    public string Serialize()
    {
        return $"{{\"market\":{Id},\"total_volume\":{TotalVolume},\"mean_price\":{MeanPrice},\"mean_volume\":{MeanVolume},\"volume_weighted_average_price\":{VolumeWeightedAvgPrice},\"percentage_buy\":{PercentageBuy}}}";
    }
}