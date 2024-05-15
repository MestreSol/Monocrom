using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    public static MinimapController Instance;

    public GameObject MinimapAreaPrefab;
    public GameObject MinimapDoorPrefab;
    public Transform MinimapParent;

    public Dictionary<Vector2Int, MinimapArea> minimapAreas = new Dictionary<Vector2Int, MinimapArea>();

    private void Awake()
    {
        Instance = this;
    }

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
        if (minimapAreas.ContainsKey(minimapArea.Position))
        {
            return;
        }

        minimapAreas.Add(minimapArea.Position, minimapArea);
    }
}
