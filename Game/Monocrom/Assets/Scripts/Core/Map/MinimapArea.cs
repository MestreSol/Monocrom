using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapArea : MonoBehaviour
{
    public Vector2Int Position;
    public Vector2Int Size;
    public bool IsRevealed = false;
    public List<MinimapDoor> Doors = new List<MinimapDoor>();
    
}

public class MinimapDoor
{
    public Vector2Int Position;
}
