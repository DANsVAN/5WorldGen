using Godot;
using System;
using System.Xml.Serialization;
public struct PerlinNoiseUISettings
	{
		public double Width;
		public double Height;
		public double Octaves;
		public string  Seed;
		public double DeepWater;
		public double ShallowWater;
		public double Beach;
		public double Grassland;
		public double Mountain;
		public double SnowyPeaks;
		public bool FBM;

		

		// You can also add a Constructor to make creating it easier
		public PerlinNoiseUISettings(double width,double height, double octaves ,string seed, double deepWater, double shallowWater, double beach, double grassland, double mountain, double snowyPeaks, bool fbm)
		{
			Width = width;
			Height = height;
			Octaves = octaves;
			Seed = seed;
			DeepWater = deepWater;
			ShallowWater = shallowWater;
			Beach = beach;
			Grassland = grassland;
			Mountain = mountain;
			SnowyPeaks = snowyPeaks;
			FBM = fbm;
		}
	}
public partial class PerlinNoiseUi : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	BoxContainer hideableContent;
	PerlinNoise perlinnoise;
	BoxContainer allContent;
	PerlinNoiseUISettings perlinNoiseUISettings;
	Label widthLable;
	Label heightLable;
	Label octavesLable;
	Label deepWaterLable;
	Label shallowWaterLable;
	Label beachLable;
	Label grasslandLable;
	Label mountainLable;
	Label snowyPeaksLable;
	string widthLableStartText;
	string heightLableStartText;
	string octavesLableStartText;
	string deepWaterLableStartText;
	string shallowWaterLableStartText;
	string beachLableStartText;
	string grasslandLableStartText;
	string mountainLableStartText;
	string snowyPeaksLableStartText;
	
	public void _on_hide_generation_controls_pressed()
	{
		
		hideableContent.Visible = !hideableContent.Visible;
	}

	public void updateLable(Label label, string startText, string value)
	{
		label.Text = startText + value;
	}
	public void _on_chunk_width_slider_value_changed(double value)
	{
		perlinNoiseUISettings.Width = value;
		updateLable(widthLable,widthLableStartText,perlinNoiseUISettings.Width.ToString());
	}
	public void _on_h_slider_value_changed(double value)
	{
		perlinNoiseUISettings.Height = value;
		updateLable(heightLable,heightLableStartText,perlinNoiseUISettings.Height.ToString());
	}
	public void _on_noise_octaves_text_value_changed(double value)
	{
		perlinNoiseUISettings.Octaves = value;
		updateLable(octavesLable,octavesLableStartText,perlinNoiseUISettings.Octaves.ToString());
	}
public void _on_deep_water_threshold_slider_value_changed(double value)
	{
		perlinNoiseUISettings.DeepWater = value;
		updateLable(deepWaterLable,deepWaterLableStartText,perlinNoiseUISettings.DeepWater.ToString("0.00"));
	}
public void _on_shallow_water_threshold_slider_value_changed(double value)
	{
		perlinNoiseUISettings.ShallowWater = value;
		updateLable(shallowWaterLable,shallowWaterLableStartText,perlinNoiseUISettings.ShallowWater.ToString("0.00"));
	}
public void _on_beach_threshold_slider_value_changed(double value)
	{
		perlinNoiseUISettings.Beach = value;
		updateLable(beachLable,beachLableStartText,perlinNoiseUISettings.Beach.ToString("0.00"));
	}
public void _on_grassland_threshold_slider_value_changed(double value)
	{
		perlinNoiseUISettings.Grassland = value;
		updateLable(grasslandLable,grasslandLableStartText,perlinNoiseUISettings.Grassland.ToString("0.00"));
	}
public void _on_mountain_threshold_slider_value_changed(double value)
	{
		perlinNoiseUISettings.Mountain = value;
		updateLable(mountainLable,mountainLableStartText,perlinNoiseUISettings.Mountain.ToString("0.00"));
	}
public void _on_snowy_peaks_threshold_slider_value_changed(double value)
	{
		perlinNoiseUISettings.SnowyPeaks = value;
		updateLable(snowyPeaksLable,snowyPeaksLableStartText,perlinNoiseUISettings.SnowyPeaks.ToString("0.00"));
	}
public void _on_fbm_toggled(bool value)
	{
		perlinNoiseUISettings.FBM = value;
	}
public void _on_line_edit_text_changed(string value)
	{
		perlinNoiseUISettings.Seed = value;
	}
	public void _on_regenerate_world_pressed()
	{
		perlinnoise.genPerlinNoise(perlinNoiseUISettings);
	}
	public void _on_main_menu_pressed()
	{
		GetTree().ReloadCurrentScene();
	}
	public override void _Ready()
	{
		perlinnoise = GetParent() as PerlinNoise;
		allContent = GetChild(0) as BoxContainer;
		hideableContent = allContent.GetChild(1) as BoxContainer;
		perlinNoiseUISettings = new PerlinNoiseUISettings(1000.00, 1000.00, 8.00, "0",-0.3,-0.1,0.0,0.25,0.5,1.0,true);
		widthLable = hideableContent.GetChild(0) as Label;
		widthLableStartText = widthLable.Text;
		heightLable = hideableContent.GetChild(2) as Label;
		heightLableStartText = heightLable.Text;
		octavesLable = hideableContent.GetChild(5) as Label;
		octavesLableStartText = octavesLable.Text;
		deepWaterLable = hideableContent.GetChild(9) as Label;
		deepWaterLableStartText = deepWaterLable.Text;
		shallowWaterLable = hideableContent.GetChild(11) as Label;
		shallowWaterLableStartText = shallowWaterLable.Text;
		beachLable = hideableContent.GetChild(13) as Label;
		beachLableStartText = beachLable.Text;
		grasslandLable = hideableContent.GetChild(15) as Label;
		grasslandLableStartText = grasslandLable.Text;
		mountainLable = hideableContent.GetChild(17) as Label;
		mountainLableStartText = mountainLable.Text;
		snowyPeaksLable = hideableContent.GetChild(19) as Label;
		snowyPeaksLableStartText = snowyPeaksLable.Text;

		updateLable(widthLable,widthLableStartText,perlinNoiseUISettings.Width.ToString());
		updateLable(heightLable,heightLableStartText,perlinNoiseUISettings.Height.ToString());
		updateLable(octavesLable,octavesLableStartText,perlinNoiseUISettings.Octaves.ToString());
		updateLable(deepWaterLable,deepWaterLableStartText,perlinNoiseUISettings.DeepWater.ToString());
		updateLable(shallowWaterLable,shallowWaterLableStartText,perlinNoiseUISettings.ShallowWater.ToString());
		updateLable(beachLable,beachLableStartText,perlinNoiseUISettings.Beach.ToString());
		updateLable(grasslandLable,grasslandLableStartText,perlinNoiseUISettings.Grassland.ToString());
		updateLable(mountainLable,mountainLableStartText,perlinNoiseUISettings.Mountain.ToString());
		updateLable(snowyPeaksLable,snowyPeaksLableStartText,perlinNoiseUISettings.SnowyPeaks.ToString());
		Callable.From(() => perlinnoise.genPerlinNoise(perlinNoiseUISettings)).CallDeferred();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
