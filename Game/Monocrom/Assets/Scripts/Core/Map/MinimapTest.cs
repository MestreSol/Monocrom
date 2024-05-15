using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapTest : MonoBehaviour
{
    public GameObject MinimapAreaPrefab;
    public GameObject MinimapDoorPrefab;
    public Transform MinimapParent;

    public Dictionary<Vector2Int, MinimapArea> minimapAreas = new Dictionary<Vector2Int, MinimapArea>();

    public void CreateMinimapArea(Vector2Int position, Vector2Int size)
    {
        if (minimapAreas.ContainsKey(position))
        {
            return;
        }

        GameObject minimapAreaGameObject = Instantiate(MinimapAreaPrefab, MinimapParent);
        MinimapArea minimapArea = minimapAreaGameObject.GetComponent<MinimapArea>();
        minimapArea.Position = position;
        minimapArea.Size = size;
        minimapAreas.Add(position, minimapArea);
    }

    public void CreateMinimapDoor(Vector2Int position, Vector2Int areaPosition)
    {
        if (!minimapAreas.ContainsKey(areaPosition))
        {
            return;
        }

        MinimapArea minimapArea = minimapAreas[areaPosition];
        MinimapDoor minimapDoor = new MinimapDoor();
        minimapDoor.Position = position;
        minimapArea.Doors.Add(minimapDoor);

        GameObject minimapDoorGameObject = Instantiate(MinimapDoorPrefab, MinimapParent);
        minimapDoorGameObject.transform.position = new Vector3(position.x, 0, position.y);
    }

    public void RevealMinimapArea(Vector2Int position)
    {
        if (!minimapAreas.ContainsKey(position))
        {
            return;
        }

        MinimapArea minimapArea = minimapAreas[position];
        minimapArea.IsRevealed = true;
        minimapArea.gameObject.SetActive(true);
    }

    public void AddMinimapArea(MinimapArea minimapArea)
    {
        minimapAreas.Add(minimapArea.Position, minimapArea);
    }

    public void Start()
    {
        CreateMinimapArea(new Vector2Int(0, 0), new Vector2Int(10, 10));
        CreateMinimapArea(new Vector2Int(10, 0), new Vector2Int(10, 10));
        CreateMinimapArea(new Vector2Int(0, 10), new Vector2Int(10, 10));
        CreateMinimapArea(new Vector2Int(10, 10), new Vector2Int(10, 10));

        CreateMinimapDoor(new Vector2Int(5, 0), new Vector2Int(0, 0));
        CreateMinimapDoor(new Vector2Int(0, 5), new Vector2Int(0, 0));
        CreateMinimapDoor(new Vector2Int(10, 5), new Vector2Int(0, 0));
        CreateMinimapDoor(new Vector2Int(5, 10), new Vector2Int(0, 0));

        CreateMinimapDoor(new Vector2Int(5, 0), new Vector2Int(10, 0));
        CreateMinimapDoor(new Vector2Int(0, 5), new Vector2Int(10, 0));
        CreateMinimapDoor(new Vector2Int(10, 5), new Vector2Int(10, 0));
        CreateMinimapDoor(new Vector2Int(5, 10), new Vector2Int(10, 0));

        CreateMinimapDoor(new Vector2Int(5, 0), new Vector2Int(0, 10));
        CreateMinimapDoor(new Vector2Int(0, 5), new Vector2Int(0, 10));
        CreateMinimapDoor(new Vector2Int(10, 5), new Vector2Int(0, 10));
        CreateMinimapDoor(new Vector2Int(5, 10), new Vector2Int(0, 10));

        CreateMinimapDoor(new Vector2Int(5, 0), new Vector2Int(10, 10));
        CreateMinimapDoor(new Vector2Int(0, 5), new Vector2Int(10, 10));
        CreateMinimapDoor(new Vector2Int(10, 5), new Vector2Int(10, 10));
        CreateMinimapDoor(new Vector2Int(5, 10), new Vector2Int(10, 10));
    }
}
