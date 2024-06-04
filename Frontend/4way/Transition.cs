using Godot;
using System;

public class Transition : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	public async void reload() {
		AnimationPlayer fader = GetNode<AnimationPlayer>("Fader");
		fader.Play("fadeout");

		await ToSignal(fader, "animation_finished");

		Node worldNode = GetNode("/root/World");
		worldNode.Call("reload");

		fader.PlayBackwards("fadeout");
	}
	
	public void Enter()
	{
		// Play the 'fadeout' animation backwards
		AnimationPlayer fader = GetNode<AnimationPlayer>("Fader");
		fader.PlayBackwards("fadeout");

		// Load the banner
		Node bannerNode = GetNode("/root/Main/Banner");
		bannerNode.Call("banner_load");
	}
}

