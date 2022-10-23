using System;

public class GraphMapper
{
    private const int HEIGHT = 720;

    public static int PriceToScreen(int min, int max, float num)
    {
        float sub = num - min;
        float perc = sub / (max - min); // Percentage to max
        int output = (int) (perc * HEIGHT); // Gets pixel height
        output = HEIGHT - output; // Inverts for screen
        return output;
    }
}