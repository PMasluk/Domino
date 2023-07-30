using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMoving : MonoBehaviour
{
    [SerializeField]
    private int rotateValue = 0;
   
    private bool canRotate;
    private bool isOnMap;
    private Camera camera;
    private Vector2 startPosition;
    private BlockPoints blockPoints;

    private void Awake()
    {
        camera = Camera.main;
        blockPoints = GetComponent<BlockPoints>();
    }

    private void OnMouseEnter()
    {
        canRotate = true;
    }

    private void OnMouseDown()
    {
        startPosition = transform.position;
    }

    private void OnMouseDrag()
    {
        if (isOnMap == false)
        {
            Vector2 newPosition = camera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = newPosition;
        }
    }

    private void OnMouseUp()
    {
        SetNewPosition();

        if (!TileManager.Instance.IsInGameArea(transform.position))
        {
            transform.position = startPosition;
            return;
        }

        Vector2 firstBlockPosition = default;
        Vector2 secondBlockPosition = default;

        if (rotateValue == 0)
        {
            firstBlockPosition = new Vector2(transform.position.x, transform.position.y + 0.5f);
            secondBlockPosition = new Vector2(transform.position.x, transform.position.y - 0.5f);
        }
        else if (rotateValue == 2)
        {
            firstBlockPosition = new Vector2(transform.position.x, transform.position.y - 0.5f);
            secondBlockPosition = new Vector2(transform.position.x, transform.position.y + 0.5f);

        }
        else if (rotateValue == 1)
        {
            firstBlockPosition = new Vector2(transform.position.x + 0.5f, transform.position.y);
            secondBlockPosition = new Vector2(transform.position.x - 0.5f, transform.position.y);

        }
        else if (rotateValue == 3)
        {
            firstBlockPosition = new Vector2(transform.position.x - 0.5f, transform.position.y);
            secondBlockPosition = new Vector2(transform.position.x + 0.5f, transform.position.y);
        }

        if (BlocksManager.Instance.IsSlotOnList(firstBlockPosition) || BlocksManager.Instance.IsSlotOnList(secondBlockPosition))
        {
            transform.position = startPosition;
            return;
        }

        if (BlocksManager.Instance.NeighborsAmount(firstBlockPosition) + BlocksManager.Instance.NeighborsAmount(secondBlockPosition) != 1 && !BlocksManager.Instance.IsListEmpty())
        {
            transform.position = startPosition;
            return;
        }

        PositionPointsStruct[] newEndsStructs;
        PositionPointsStruct firstStruct = new PositionPointsStruct(firstBlockPosition, blockPoints.FirstValue);
        PositionPointsStruct secondStruct = new PositionPointsStruct(secondBlockPosition, blockPoints.SecondValue);

        if (!BlocksManager.Instance.IsListEmpty())
        {
            int counter = 0;
            int index = 0;
            PositionPointsStruct newEndStruct = default;
            PositionPointsStruct oppositeStruct = default;

            for (int i = 0; i < 2; i++)
            {
                if (Vector2.Distance(BlocksManager.Instance.TakeEndPositions()[i].Position, firstBlockPosition) == 1)
                {
                    newEndStruct = secondStruct;
                    oppositeStruct = firstStruct;
                    index = i;
                    counter++;
                }
                if (Vector2.Distance(BlocksManager.Instance.TakeEndPositions()[i].Position, secondBlockPosition) == 1)
                {
                    newEndStruct = firstStruct;
                    oppositeStruct = secondStruct;
                    index = i;
                    counter++;
                }
            }

            if (counter != 1)
            {
                transform.position = startPosition;
                return;
            }

            Debug.Log($"{BlocksManager.Instance.TakeEndPositions()[index].Value}");
            Debug.Log($"{oppositeStruct.Value}");

            if (BlocksManager.Instance.TakeEndPositions()[index].Value != oppositeStruct.Value && !(BlocksManager.Instance.TakeEndPositions()[index].Value == 0 || oppositeStruct.Value == 0)) 
            {
                transform.position = startPosition;
                return;
            }

            if (index == 0)
            {
                newEndsStructs = new PositionPointsStruct[] { newEndStruct, BlocksManager.Instance.TakeEndPositions()[1] };
            }
            else
            {
                newEndsStructs = new PositionPointsStruct[] { newEndStruct, BlocksManager.Instance.TakeEndPositions()[0] };
            }
        }
        else
        {
            newEndsStructs = new PositionPointsStruct[] { firstStruct, secondStruct };
        }

      
        BlocksManager.Instance.ChangeEndPositions(newEndsStructs);

        BlocksManager.Instance.AddSlot(firstBlockPosition);
        BlocksManager.Instance.AddSlot(secondBlockPosition);
        isOnMap = true;
        SpawnManager.Instance.SpawnNewBlock(startPosition);
        UserManager.Instance.AddPoints();
    }


    private void SetNewPosition()
    {
        Vector2 newPosition = default;
        if (rotateValue == 0 || rotateValue == 2)
        {
            newPosition.x = Mathf.RoundToInt(transform.position.x);
            newPosition.y = ((int)transform.position.y) + 0.5f;
        }
        else if (rotateValue == 1 || rotateValue == 3)
        {
            newPosition.x = ((int)transform.position.x) + 0.5f;
            newPosition.y = Mathf.RoundToInt(transform.position.y);
        }
        transform.position = newPosition;
    }

    private void OnMouseExit()
    {
        canRotate = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && canRotate == true && isOnMap == false)
        {
            transform.Rotate(0, 0, -90);
            rotateValue++;

            if (rotateValue == 4)
            {
                rotateValue = 0;
            }

        }
    }
}
