using Godot;
using System.Threading.Tasks;
using System.Collections.Generic;

public class GraphUI : Control
{
	private const int MIN = 50;
	private const int MAX = 150;

	public override void _Ready()
	{
		GetShits();
	}
	
	/*
	public override void _PhysicsProcess(float delta)
	{
		if (timer.TimeLeft - (int)timer.TimeLeft < 0.01f && timer.TimeLeft != timer.WaitTime)
			GD.Print((int) timer.TimeLeft);
	}
	*/
	
	public async void GetShits()
	{
		foreach(Node node in GetChildren())
		{
			RemoveChild(node);
			node.QueueFree();
		}

		List<PriceData> data = ApiPull.GetPriceData("GOOG");
		
		float length = 1280.0f / data.Count;

		for (int i = 0; i < data.Count; i++)
		{
			// Candle
		    ColorRect rect = new ColorRect();
			if (data[i].Open >= data[i].Close)
			{
				if (data[i].High == data[i].Low || data[i].Close == data[i].Open)
				{
					GD.Print("Shit's the same");
					continue;
				}
		    	int top = GraphMapper.PriceToScreen(MIN, MAX, data[i].Open);
		    	rect.SetPosition(new Vector2(length * i, top));
				rect.SetSize(new Vector2(16, GraphMapper.PriceToScreen(MIN, MAX, data[i].Close) - top));
		    	rect.Color = new Color(0, 1, 0);
			}
			else
			{
		    	int top = GraphMapper.PriceToScreen(MIN, MAX, data[i].Close);
				rect.SetPosition(new Vector2(length * i, top));
				rect.SetSize(new Vector2(16, GraphMapper.PriceToScreen(MIN, MAX, data[i].Open) - top));
		    	rect.Color = new Color(1, 0, 0);
			}
		    AddChild(rect);
			
			// Wick
			ColorRect wick = new ColorRect();
			int high = GraphMapper.PriceToScreen(MIN, MAX, data[i].High);
			wick.SetPosition(new Vector2(length * i + (length / 2 - length / 8), length * high));
			wick.SetSize(new Vector2(length / 4, GraphMapper.PriceToScreen(MIN, MAX, data[i].Low) - high));
			wick.Color = new Color(1, 1, 1);
			AddChild(wick);
		}
		
		foreach(PriceData d in data)
		{
			await Task.Delay(200);
			GD.Print(d);
		}
	}
}