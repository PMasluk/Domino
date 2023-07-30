using UnityEngine;

public struct PositionPointsStruct
{
    public Vector2 Position { get; }
    public int Value { get; }

    public PositionPointsStruct(Vector2 position, int value)
    {
        Position = position;
        Value = value;
    }
}