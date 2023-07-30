using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : Singleton<TileManager>
{
    [SerializeField]
    private GameObject tilePrefab;
    [SerializeField]
    private int width;
    [SerializeField]
    private int height;

    private Vector2 lastTilePosition;

    private void Awake()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Instantiate(tilePrefab, new Vector2(j, i), Quaternion.identity, this.transform);
            }
        }
        Camera.main.transform.position = new Vector3(width / 2 - 0.5f, height / 2 - 0.5f, Camera.main.transform.position.z);

        lastTilePosition = new Vector2(width - 1, height - 1);
    }

    public bool IsInGameArea(Vector2 blockPosition)
    {
        if (blockPosition.x >= 0 && blockPosition.x <= lastTilePosition.x && blockPosition.y >= 0 && blockPosition.y <= lastTilePosition.y)
        {
            return true;
        }
        return false;
    }
}
