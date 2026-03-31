using Godot;
using System;

public partial class MovableCamera : Camera2D
{
    [Export] public MouseButton DragButton = MouseButton.Left;
    [Export] public float ZoomSpeed = 0.1f;
    [Export] public Vector2 MinMaxZoom = new Vector2(0.1f, 4.0f);

    private bool _isDragging = false;

    public override void _UnhandledInput(InputEvent @event)
    {
        // 1. Zoom Logic
        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.ButtonIndex == MouseButton.WheelUp) ZoomIn();
            if (mouseEvent.ButtonIndex == MouseButton.WheelDown) ZoomOut();

            // 2. Detect Drag Start/Stop
            if (mouseEvent.ButtonIndex == DragButton)
            {
                _isDragging = mouseEvent.Pressed;
            }
        }

        // 3. The Drag Math
        if (@event is InputEventMouseMotion mouseMotion && _isDragging)
        {
            Position -= mouseMotion.Relative / Zoom;
        }
    }

    private void ZoomIn()
    {
        Zoom = (Zoom + new Vector2(ZoomSpeed, ZoomSpeed)).Clamp(MinMaxZoom.X * Vector2.One, MinMaxZoom.Y * Vector2.One);
    }

    private void ZoomOut()
    {
        Zoom = (Zoom - new Vector2(ZoomSpeed, ZoomSpeed)).Clamp(MinMaxZoom.X * Vector2.One, MinMaxZoom.Y * Vector2.One);
    }


	public override void _Ready()
	{
		this.Enabled = true;
		this.MakeCurrent(); 
	}
}