using Godot;
using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class ApiPull
{
    private static string GetApiKey()
    {
        string piss = System.IO.File.ReadAllText("api.txt");
        return piss;
    }
    
    public static List<PriceData> GetPriceData(string symbol)
    {
        string apiKey = GetApiKey();
        string url = $"https://alpha-vantage.p.rapidapi.com/query?interval=1min&function=TIME_SERIES_INTRADAY&symbol={symbol}&datatype=json&output_size=compact";
        
        WebRequest req = WebRequest.Create(url);
        req.Headers["X-RapidAPI-Key"] = apiKey;
        req.Headers["X-RapidAPI-Host"] = "alpha-vantage.p.rapidapi.com";
        
        Stream reqStr = req.GetResponse().GetResponseStream();
        StreamReader reader = new StreamReader(reqStr);
    
        string line = "";
        List<PriceData> data = new List<PriceData>();
        PriceData append = new PriceData();
        while (line != null)
        {
            line = reader.ReadLine();

            if (line != null)
            {
                line = line.Trim();
                if (line.Contains("Meta") || line.Contains("Information") || line.Contains("Symbol") || line.Contains("Last") || line.Contains("Interval") || line.Contains("Output") || line.Contains("Time Zone"))
                    continue;
                if (line.Contains("-") && line.Contains(":") && line.Contains("{"))
                {
                    append = new PriceData();
                    line = line.Trim(new char[] {'"', ':', '{', ' '});
                    append.Timestamp = DateTime.Parse(line);
                    continue;
                }
                if (line.Contains("open"))
                {
                    MatchCollection m = Regex.Matches(line, "[0-9]+\\.[0-9]*");
                    append.Open = m[1].ToString().ToFloat();
                    continue;
                }
                if (line.Contains("high"))
                {
                    MatchCollection m = Regex.Matches(line, "[0-9]+\\.[0-9]*");
                    append.High = m[1].ToString().ToFloat();
                    continue;
                }
                if (line.Contains("low"))
                {
                    MatchCollection m = Regex.Matches(line, "[0-9]+\\.[0-9]*");
                    append.Low = m[1].ToString().ToFloat();
                    continue;
                }
                if (line.Contains("close"))
                {
                    MatchCollection m = Regex.Matches(line, "[0-9]+\\.[0-9]*");
                    append.Close = m[1].ToString().ToFloat();
                    continue;
                }
                if (line.Contains("volume"))
                {
                    MatchCollection m = Regex.Matches(line, "[0-9]+");
                    append.Volume = m[1].ToString().ToInt();
                    continue;
                }
                if (line.Contains("}"))
                {
                    data.Add(append);
                }
            }
        }
        
        return data;
    }   
}