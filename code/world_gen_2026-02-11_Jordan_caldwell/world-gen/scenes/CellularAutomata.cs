using Godot;
using System;
public partial class CellularAutomata : Node2D
{
	public bool floor = false;
	public bool wall = true;
	Random rand;
	[Export] public TileMapLayer WorldTileMap;
	// Called when the node enters the scene tree for the first time.
		public void genCellularAutomata(CellularAutomataUISettings setings)
	{
		bool[,] noiseGride = makeNoiseGride(setings);
		noiseGride = startSteps(setings,noiseGride);
		DrawToTileMap(setings,noiseGride);
	}
	public bool[,] makeNoiseGride(CellularAutomataUISettings setings)
	{
		// A 2D array with Height = rows and Width = columns
	bool[,] matrix = new bool[(int)setings.Height, (int)setings.Width];
	if(setings.Seed == "0")
		{
			rand = new Random();
		}
		else
		{
			rand = new Random(setings.Seed.GetHashCode());
		}

	double min = 0.01;
	double max = 1.0;
	// Initializing with values immediately
	for (int col = 0; col < setings.Width; col++)
		{
			for (int row = 0; row < setings.Height; row++)
			{
				double random = (rand.NextDouble() * (max - min)) + min;
				if (random <= setings.Density)
				{
					matrix[row,col] = wall;
				}
				else
				{
					matrix[row,col] = floor;
				}
				
			}
			
		}
		return(matrix);
	}


public bool[,] startSteps(CellularAutomataUISettings setings, bool[,] startingNoiseGride)
{
    // 1. Prepare the two buffers
    bool[,] bufferA = startingNoiseGride;
    bool[,] bufferB = new bool[(int)setings.Height, (int)setings.Width];

    for (int step = 0; step < setings.Steps; step++)
    {
        // We pass BOTH arrays to the function
        takeStep(setings, bufferA, bufferB);

        // 2. SWAP: The results we just wrote into B now become our source for the next step
        // This is a "shallow swap" (fast) - we just point the variables to the other array
        bool[,] temp = bufferA;
        bufferA = bufferB;
        bufferB = temp;
    }

    // After the last swap, bufferA contains the final result
    return bufferA;
}

// Note: We changed the return type to void because we are modifying the 'writeTo' array directly
public void takeStep(CellularAutomataUISettings setings, bool[,] readFrom, bool[,] writeTo)
{
    int h = (int)setings.Height;
    int w = (int)setings.Width;

    for (int col = 0; col < w; col++)
    {
        for (int row = 0; row < h; row++)
        {
            int wallCounter = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;

                    int nRow = row + i;
                    int nCol = col + j;

                    if (nRow < 0 || nRow >= h || nCol < 0 || nCol >= w)
                    {
                        wallCounter++; 
                    }
                    else if (readFrom[nRow, nCol] == wall) // Reading from bufferA
                    {
                        wallCounter++;
                    }
                }
            }

            // Writing into bufferB
            if (wallCounter >= 5) 
                writeTo[row, col] = wall;
            else 
                writeTo[row, col] = floor;
        }
    }
}

private void DrawToTileMap(CellularAutomataUISettings setings, bool[,] grid)
{
    WorldTileMap.Clear();
    
    var wallCells = new Godot.Collections.Array<Vector2I>();


    int floorSourceID = 0; 
    Vector2I floorAtlasCoords = new Vector2I(2, 8); // Change to your floor tile's X, Y

    for (int col = 0; col < setings.Width; col++)
    {
        for (int row = 0; row < setings.Height; row++)
        {
            Vector2I cellPos = new Vector2I(col, row);
            
            if (grid[row, col] == wall)
            {
                wallCells.Add(cellPos);
            }
            else
            {
                // Draw floor tile immediately
                WorldTileMap.SetCell(cellPos, floorSourceID, floorAtlasCoords);
            }
        }
    }

    WorldTileMap.SetCellsTerrainConnect(wallCells, 0, 0); 
}

	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
