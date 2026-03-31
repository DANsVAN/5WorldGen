using Godot;
using System;

public partial class MainMenu : CanvasLayer
{
	string secneFilePath;
	GameManger gameManger;
	// Called when the node enters the scene tree for the first time.





	public void _on_cellualr_automata_btn_pressed()
	{

		secneFilePath = "res://scenes/cellular_automata.tscn";
		this.FollowViewportEnabled = true;
		gameManger.ChangeChildScene(secneFilePath);
	}
		public void _on_perlin_noise_btn_pressed()
	{
		secneFilePath = "res://scenes/perlin_noise.tscn";
		this.FollowViewportEnabled = true;
		gameManger.ChangeChildScene(secneFilePath);
	}
		public void _on_exit_btn_pressed()
	{
		gameManger.EndGame();
	}
	public override void _Ready()
	{
		gameManger = GetParent() as GameManger;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
