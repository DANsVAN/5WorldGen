using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public struct CellularAutomataUISettings
	{
		public double Width;
		public double Height;
		public double Steps;
		public double Density;
		public string  Seed;
		

		// You can also add a Constructor to make creating it easier
		public CellularAutomataUISettings(double width,double height, double steps,double density,string seed)
		{
			Width = width;
			Height = height;
			Steps = steps;
			Density = density;
			Seed = seed;
		}
	}
	
public partial class CellularAutomataUi : CanvasLayer
{
	BoxContainer allContent;
	BoxContainer hideableContent;
	public CellularAutomata cellularautomata;
	public GameManger gameManger;
	public Label widthLable;
	public string widthLableStartText; 
	public Label heightLable;
	public string heightLableStartText; 
	public Label stepsLable;
	public string stepsLableStartText; 
	public Label densityLable;
	public string densityLableStartText; 
public CellularAutomataUISettings cellularAutomataUISettings;

	// Called when the node enters the scene tree for the first time.


	public override void _Ready()
	{
    cellularautomata = GetParent() as CellularAutomata;
    allContent = GetChild(0) as BoxContainer;
    hideableContent = allContent.GetChild(1) as BoxContainer;
    cellularAutomataUISettings = new CellularAutomataUISettings(200.00, 200.00, 4.00, 0.6, "0");
	widthLable = hideableContent.GetChild(0) as Label;
	widthLableStartText = widthLable.Text;
	heightLable = hideableContent.GetChild(2) as Label;
	heightLableStartText = heightLable.Text;
	stepsLable = hideableContent.GetChild(4) as Label;
	stepsLableStartText = stepsLable.Text;
	densityLable = hideableContent.GetChild(6) as Label;
	densityLableStartText = densityLable.Text;
	updateLable(widthLable,widthLableStartText,cellularAutomataUISettings.Width.ToString());
	updateLable(heightLable,heightLableStartText,cellularAutomataUISettings.Height.ToString());
	updateLable(stepsLable,stepsLableStartText,cellularAutomataUISettings.Steps.ToString());
	updateLable(densityLable,densityLableStartText,cellularAutomataUISettings.Density.ToString("0.00"));
	cellularautomata.genCellularAutomata(cellularAutomataUISettings);

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public void updateLable(Label label, string startText, string value)
	{
		label.Text = startText + value;
	}

	public void _on_chunk_width_slider_value_changed(double value)
	{
		cellularAutomataUISettings.Width = value;
		updateLable(widthLable,widthLableStartText,value.ToString());
		
	}
	public void _on_h_slider_value_changed(double value)
	{
		cellularAutomataUISettings.Height = value;
		updateLable(heightLable,heightLableStartText,value.ToString());
	}
	public void _on_num_steps_slider_value_changed(double value)
	{
		cellularAutomataUISettings.Steps = value;
		updateLable(stepsLable,stepsLableStartText,value.ToString());
	}
		public void _on_initial_density_slider_value_changed(double value)
	{
		cellularAutomataUISettings.Density = value;
		updateLable(densityLable,densityLableStartText,value.ToString("0.00"));
	}
		public void _on_line_edit_text_changed(string value)
	{
		cellularAutomataUISettings.Seed = value;
	}
	public void _on_regenerate_world_pressed()
	{
		cellularautomata.genCellularAutomata(cellularAutomataUISettings);
	}
	public void _on_hide_generation_controls_pressed()
	{
		
		hideableContent.Visible = !hideableContent.Visible;
	}
	public void _on_main_menu_pressed(){

		GetTree().ReloadCurrentScene();
	}

	public override void _Process(double delta)
	{
	}
}
