using System;

public class PriceData
{
    public DateTime Timestamp;
    public float Open;
    public float High;
    public float Low;
    public float Close;
    public int Volume;

    public override string ToString()
    {
        string str = "";

        str += Timestamp.ToString() + "\n";
        str += "Open: " + Open + "\n";
        str += "High: " + High + "\n";
        str += "Low: " + Low + "\n";
        str += "Close: " + Close + "\n";
        str += "Volume: " + Volume;

        return str;
    }
}