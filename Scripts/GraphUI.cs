using Godot;
using System.Threading.Tasks;
using System.Collections.Generic;

public class GraphUI : Control
{
	private Timer timer;

	public override void _Ready()
	{
		timer = GetNode<Timer>("Timer");
		timer.Connect("timeout", this, "OnTimerTimeout");
		OnTimerTimeout();
	}
	
	/*
	public override void _PhysicsProcess(float delta)
	{
		if (timer.TimeLeft - (int)timer.TimeLeft < 0.01f && timer.TimeLeft != timer.WaitTime)
			GD.Print((int) timer.TimeLeft);
	}
	*/
	
	public void OnTimerTimeout()
	{
		List<PriceData> data = ApiPull.GetPriceData("MSFT");
		
		for (int i = 0; i < data.Count; i++)
		{
			// Candle
		    ColorRect rect = new ColorRect();
		    int top = GraphMapper.PriceToScreen(100, 500, data[i].Open);
		    rect.SetPosition(new Vector2(16 * i, top));
			rect.SetSize(new Vector2(16, GraphMapper.PriceToScreen(100, 500, data[0].Close) - top));
		    rect.Color = new Color(0, 0, 0);
		    AddChild(rect);
			
			// Wick
			ColorRect wick = new ColorRect();
			int high = GraphMapper.PriceToScreen(100, 500, data[0].High);
			wick.SetPosition(new Vector2(16 * i + 6, 16 * high));
			wick.SetSize(new Vector2(4, GraphMapper.PriceToScreen(100, 500, data[0].Low) - top));
			wick.Color = new Color(1, 1, 1);
			AddChild(wick);
		}
	}
}