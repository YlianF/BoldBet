using Godot;
using System;

public partial class MainMenuButton : Control
{
	private ColorRect screenTransition;

	public override void _Ready()
	{
		screenTransition = GetNode<ColorRect>("%ScreenTransition");
	}



	public void OnButtonCliked(String name)
	{
		switch (name)
		{
			case "ButtonNewGame":
				ButtonNewGameCliked();
				break;
			case "ButtonContinue":
				ButtonContinueCliked();
				break;
			case "ButtonSettings":
				ButtonSettingsCliked();
				break;
			case "ButtonExit":
				ButtonExitCliked();
				break;
			default:
				GD.Print("defalut");
				break;
		}
	}

	
	private void ButtonNewGameCliked()
	{
		// If save -> Pop UP
		StratScreenTransition(Callable.From(CreateNewGame));
		// Generate save and start game
	}

	private void ButtonContinueCliked()
	{
		StratScreenTransition(Callable.From(LoadGame));
		// Load save and start game
	}

	private void ButtonSettingsCliked()
	{
		StratScreenTransition(Callable.From(LoadSetting));
	}

	private void ButtonExitCliked()
	{
		StratScreenTransition(Callable.From(Exit));
	}



	private void StratScreenTransition(Godot.Callable function)
	{
		screenTransition.Scale = Vector2.Zero;
		screenTransition.Visible = true;

		Vector2 screenSize = GetViewport().GetVisibleRect().Size;
		screenTransition.PivotOffset = screenSize/2;
		//screenTransition.Color = Colors.Transparent;


		Tween tween = CreateTween();
		tween.TweenProperty(screenTransition, "scale", new Vector2(1f, 1f), 0.4f);
		//tween.TweenProperty(screenTransition, "color", Colors.Black, 0.5f);
		//tween.Call(method);
		tween.TweenCallback(function);
	}

	private void CreateNewGame()
	{
		GD.Print("New Game");
		// Load Game Scene
		// Create New save
		// QueueFree()
	}

	private void LoadGame()
	{
		GD.Print("Load Game");
		// Load Game Scene
		// Load save
		// Put save in Game Scene
		// QueueFree()
	}

	private void LoadSetting()
	{
		GD.Print("Settings");
	}

	
	private void Exit()
	{
		GetTree().Quit();
	}

}
