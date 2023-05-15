﻿using System.Text.Json.Serialization;

namespace Cardiverse.API.Models;

public class StockTickerData
{
    [JsonPropertyName("afterHours")]
    public decimal AfterHours { get; set; }

    [JsonPropertyName("close")]
    public decimal Close { get; set; }

    [JsonPropertyName("from")]
    public string From { get; set; }

    [JsonPropertyName("high")]
    public decimal High { get; set; }

    [JsonPropertyName("low")]
    public decimal Low { get; set; }

    [JsonPropertyName("open")]
    public decimal Open { get; set; }

    [JsonPropertyName("preMarket")]
    public decimal PreMarket { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; }

    [JsonPropertyName("volume")]
    public long Volume { get; set; }

    [JsonPropertyName("vwap")]
    public decimal VWAP { get; set; }
}

