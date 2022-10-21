using Godot;
using System.Threading.Tasks;
using System.Collections.Generic;

public class GraphUI : Control
{
	private Timer timer;
	private RichTextLabel label;

	public override void _Ready()
	{
		timer = GetNode<Timer>("Timer");
		timer.Connect("timeout", this, "OnTimerTimeout");
		label = GetNode<RichTextLabel>("RichTextLabel");
		OnTimerTimeout();
	}
	
	/*
	public override void _PhysicsProcess(float delta)
	{
		if (timer.TimeLeft - (int)timer.TimeLeft < 0.01f && timer.TimeLeft != timer.WaitTime)
			GD.Print((int) timer.TimeLeft);
	}
	*/
	
	public async void OnTimerTimeout()
	{
		List<PriceData> data = ApiPull.GetPriceData("MSFT");
		
		foreach (PriceData i in data)
		{
			await Task.Delay(50);
			GD.Print(i);
		}
	}
}