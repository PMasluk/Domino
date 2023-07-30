using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksManager : Singleton<BlocksManager>
{
    [SerializeField]
    private List<Vector2> occupiedSlots;

    [SerializeField]
    private PositionPointsStruct[] endPositionsPoints;

    public void ChangeEndPositions(PositionPointsStruct[] positionsPoints)
    {
        endPositionsPoints = positionsPoints;
    }

    public PositionPointsStruct[] TakeEndPositions()
    {
        return endPositionsPoints;
    }

    public void AddSlot(Vector2 slot)
    {
        occupiedSlots.Add(slot);
    }

    public bool IsSlotOnList(Vector2 slot)
    {
        if (occupiedSlots.Contains(slot))
        {
            return true;
        }
        return false;
    }

    public bool IsListEmpty()
    {
        if (occupiedSlots.Count == 0)
        {
            return true;
        }
        return false;
    }

    public int NeighborsAmount(Vector2 slot)
    {
        int counter = 0;
        if (occupiedSlots.Contains(new Vector2(slot.x + 1, slot.y)))
        {
            counter++;
        }
        if (occupiedSlots.Contains(new Vector2(slot.x - 1, slot.y)))
        {
            counter++;
        }
        if (occupiedSlots.Contains(new Vector2(slot.x, slot.y + 1)))
        {
            counter++;
        }
        if (occupiedSlots.Contains(new Vector2(slot.x, slot.y - 1)))
        {
            counter++;
        }
        return counter;
    }

}
