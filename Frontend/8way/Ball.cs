using Godot;
using System;

public class Ball : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private int size;
	
	private string texturePathStart = "res://Sprites/";
	private string texturePathEnd = ".png";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public void ChangeSize(int s) {
		size = s;
		
		Sprite ballSprite = GetNode<Sprite>("BallSprite");
		
		string texturePath = texturePathStart + size.ToString() + texturePathEnd;
		ballSprite.Texture = (Texture)ResourceLoader.Load(texturePath);
	}
	
	public int GetSize() {
		return size;
	}
	
	public void SetLocation (int x, int y) {
		Vector2 vec = new Vector2();
		vec.x = (x - 3) * 21;
		vec.y = (y - 3) * 21;
		GlobalPosition = vec;
	}
}
