using Godot;
using System;
using System.Linq;

public partial class PerlinNoise : Node2D
{
    // 1. Reference your TileMapLayer instead of a Sprite
    [Export] public TileMapLayer WorldTileMap;
    
    // 2. Define your Atlas Coordinates (Change these to match your actual tileset!)
    private readonly Vector2I TileDeepWater = new Vector2I(13, 2);
    private readonly Vector2I TileWater     = new Vector2I(13, 2);
    private readonly Vector2I TileSand      = new Vector2I(1, 2);
    private readonly Vector2I TileGrass     = new Vector2I(5, 6);
    private readonly Vector2I TileRock      = new Vector2I(5, 10);
    private readonly Vector2I TileSnow      = new Vector2I(9, 1);

    private int[] _p;
    public Random rng;

    public void genPerlinNoise(PerlinNoiseUISettings settings)
    {
        if (WorldTileMap == null) {
            GD.PrintErr("Please assign a TileMapLayer in the inspector!");
            return;
        }

        // Clear previous generation
        WorldTileMap.Clear();
        InitPermutationTable(settings.Seed);

        int width = (int)settings.Width;
        int height = (int)settings.Height;
        float scale = 0.05f; 

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float noiseVal = settings.FBM 
                    ? GetFBM(x * scale, y * scale, (int)settings.Octaves) 
                    : GetNoise(x * scale, y * scale);
				SetTileByNoise(x, y, noiseVal, settings);
            }
        }
    }

    private void InitPermutationTable(string seed)
    {
		if(seed == "0")
		{
			int seedHash = seed.GetHashCode();
        	rng = new Random();
		}
		else
		{
			int seedHash = seed.GetHashCode();
        	rng = new Random(seedHash);
		}
        _p = Enumerable.Range(0, 256).OrderBy(x => rng.Next()).ToArray();
        _p = _p.Concat(_p).ToArray();
    }

    private float GetFBM(float x, float y, int octaves)
    {
        float total = 0;
        float frequency = 1;
        float amplitude = 1;
        float maxValue = 0;

        for (int i = 0; i < octaves; i++)
        {
            total += GetNoise(x * frequency, y * frequency) * amplitude;
            maxValue += amplitude;
            amplitude *= 0.5f; 
            frequency *= 2.0f; 
        }

        return total / maxValue;
    }

    private float GetNoise(float x, float y)
    {
        // Find unit grid cell containing point
        int X = (int)Math.Floor(x) & 255;
        int Y = (int)Math.Floor(y) & 255;

        // Get relative coordinates in cell
        float xf = x - (float)Math.Floor(x);
        float yf = y - (float)Math.Floor(y);

        // Fade curves
        float u = Fade(xf);
        float v = Fade(yf);

        // Hash coordinates of the 4 corners
        int aa = _p[_p[X] + Y];
        int ab = _p[_p[X] + Y + 1];
        int ba = _p[_p[X + 1] + Y];
        int bb = _p[_p[X + 1] + Y + 1];

        // Blend the results from the 4 corners
        float x1 = Lerp(u, Grad(aa, xf, yf), Grad(ba, xf - 1, yf));
        float x2 = Lerp(u, Grad(ab, xf, yf - 1), Grad(bb, xf - 1, yf - 1));

        return Lerp(v, x1, x2); 
    }

    private float Fade(float t) => t * t * t * (t * (t * 6 - 15) + 10);
    private float Lerp(float t, float a, float b) => a + t * (b - a);
    
    private float Grad(int hash, float x, float y)
    {
        // Convert low 4 bits of hash code into 8 gradient directions
        int h = hash & 7;
        float u = h < 4 ? x : y;
        float v = h < 4 ? y : x;
        return ((h & 1) == 0 ? u : -u) + ((h & 2) == 0 ? v : -v);
    }

	private void SetTileByNoise(int x, int y, float noiseVal, PerlinNoiseUISettings settings)
    {
        Vector2I gridPos = new Vector2I(x, y);
        int sourceId = 0; 

        if (noiseVal < settings.DeepWater)
        {
            // deep water
            WorldTileMap.SetCell(gridPos, sourceId, TileWater, 1);
        }
        else if (noiseVal < settings.ShallowWater)
        {
            WorldTileMap.SetCell(gridPos, sourceId, TileWater, 0);
        }
        else if (noiseVal < settings.Beach)
        {
            WorldTileMap.SetCell(gridPos, sourceId, TileSand, 0);
        }
        else if (noiseVal < settings.Grassland)
        {
            WorldTileMap.SetCell(gridPos, sourceId, TileGrass, 0);
        }
        else if (noiseVal < settings.Mountain)
        {
            WorldTileMap.SetCell(gridPos, sourceId, TileRock, 0);
        }
        else
        {
            WorldTileMap.SetCell(gridPos, sourceId, TileSnow, 0);
        }
    }
	
}