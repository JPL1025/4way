using Godot;
using System;

public class SwipeControls : Control
{
	// Declare member variables here. Examples:
	private Vector2 startPos;
	private Vector2 endPos;
	private bool currentlySwiping = false;
	private float minThreshold = 25.0f;
	
	[Signal]
	public delegate void UpSignal();
		
	[Signal]
	public delegate void DownSignal();
	
	[Signal]
	public delegate void RightSignal();
	
	[Signal]
	public delegate void LeftSignal();

	public override void _Ready() {
		SetProcessInput(true);
	}
	
/*
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta) {
		if (Input.IsActionJustPressed("press")) {
			startPos = GetGlobalMousePosition();
			GD.Print("Start Swipe");
			currentlySwiping = true;
		}
		if (Input.IsActionJustReleased("press")) {
			endPos = GetGlobalMousePosition();
			GD.Print("End Swipe");
			currentlySwiping = false;
			AnalyzeSwipe();
		}
	}
*/

	// Called when the node enters the scene tree for the first time.
	public override void _Input(InputEvent @event) {
		//GD.Print("Input");
		
		if (@event is InputEventScreenTouch touchEvent) {
			
			if (touchEvent.Pressed) {
				startPos = touchEvent.Position;
				GD.Print("Start Swipe");
				currentlySwiping = true;
			} 
			else if (currentlySwiping) {
				endPos = touchEvent.Position;
				GD.Print("End Swipe");
				currentlySwiping = false;
				AnalyzeSwipe();
			}
		}
	}
	
/*
	private void _on_Swipe_Controls_button_down()
	{
		startPos = GetGlobalMousePosition();
		GD.Print("Start Swipe");
		currentlySwiping = true;
	}


	private void _on_Swipe_Controls_button_up()
	{
		endPos = GetGlobalMousePosition();
		GD.Print("End Swipe");
		currentlySwiping = false;
		AnalyzeSwipe();
	}
*/
	private void AnalyzeSwipe() {
		Vector2 swipeVector = endPos - startPos;
		GD.Print(swipeVector.Length());
		
		if (swipeVector.Length() >= minThreshold) {
			
			//Node wrld = GetNode("World");
			float swipeAngle = swipeVector.Angle();
			
			if (swipeAngle > -Mathf.Pi / 4 && swipeAngle < Mathf.Pi / 4)
			{
				GD.Print("Swipe Right");
				EmitSignal(nameof(RightSignal));
			}
			else if (swipeAngle > Mathf.Pi / 4 && swipeAngle < 3 * Mathf.Pi / 4)
			{
				GD.Print("Swipe Down");
				EmitSignal(nameof(DownSignal));
			}
			else if (swipeAngle < -Mathf.Pi / 4 && swipeAngle > -3 * Mathf.Pi / 4)
			{
				GD.Print("Swipe Up");
				EmitSignal(nameof(UpSignal));
			}
			else
			{
				GD.Print("Swipe Left");
				EmitSignal(nameof(LeftSignal));
			}
		}
	}
}
