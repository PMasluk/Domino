using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField]
    private GameObject[] blocksPrefabs;
    [SerializeField]
    private int blocksPoolCount;
    [SerializeField]
    private int visibleBlocksAmount;
    [SerializeField]
    private Vector2 firstBlockPosition;
    [SerializeField]
    private Vector2 blocksPoolPosition;
    [SerializeField]
    private float blocksPositionOffset;
    [SerializeField]
    private List<GameObject> spawnedBlocks;

    private void Awake()
    {
        int randomNumber;
        GameObject spawnedObject;

        for (int i = 0; i < blocksPoolCount; i++)
        {
            randomNumber = Random.Range(0, blocksPrefabs.Length);
            spawnedObject = Instantiate(blocksPrefabs[randomNumber], blocksPoolPosition, Quaternion.identity, this.transform);
            spawnedBlocks.Add(spawnedObject);
        }

        for (int i = 0; i < visibleBlocksAmount; i++)
        {
            spawnedBlocks.FirstOrDefault().transform.position = new Vector2(firstBlockPosition.x, firstBlockPosition.y + blocksPositionOffset * i);
            spawnedBlocks.Remove(spawnedBlocks.FirstOrDefault());
        }
    }

    public void SpawnNewBlock(Vector2 position)
    {
        spawnedBlocks.FirstOrDefault().transform.position = position;
        spawnedBlocks.Remove(spawnedBlocks.FirstOrDefault());
    }
}
