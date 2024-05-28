using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class SimpleCamera : MonoBehaviour
{
    [Header("Camera Alignment Camera")]
    public Grid grid;
    public GameObject MainCamera;
    public GameObject player;
    public Tile[] Tiles;

    [Header("Room Indication Components")]
    public Tilemap tilemap;
    public Tilemap ForegroundMap;
    public GameObject playerDot;

    TileBase[] allTiles;

    public MinimapController minimapController;

    void Update()
    {
        Vector3Int cellPosition = grid.WorldToCell(player.transform.position);
        ForegroundMap.SetTile(cellPosition, null);

        if (!minimapController.isActive)
        {

            if (tilemap.GetTile(cellPosition) == Tiles[0])
            {
                // Single Size Room
                MainCamera.transform.position = grid.GetCellCenterWorld(cellPosition);
            }
            else if (tilemap.GetTile(cellPosition) == Tiles[1])
            {
                //  Top left Corner
                Vector3 Pos = player.transform.position;
                if (Pos.x < grid.GetCellCenterWorld(cellPosition).x) { Pos.x = grid.GetCellCenterWorld(cellPosition).x; }
                if (Pos.y > grid.GetCellCenterWorld(cellPosition).y) { Pos.y = grid.GetCellCenterWorld(cellPosition).y; }
                Pos.z = grid.GetCellCenterWorld(cellPosition).z;
                MainCamera.transform.position = Pos;
            }
            else if (tilemap.GetTile(cellPosition) == Tiles[2])
            {
                // Large Room top center
                Vector3 Pos = player.transform.position;
                if (Pos.y > grid.GetCellCenterWorld(cellPosition).y) { Pos.y = grid.GetCellCenterWorld(cellPosition).y; }
                Pos.z = grid.GetCellCenterWorld(cellPosition).z;
                MainCamera.transform.position = Pos;
            }
            else if (tilemap.GetTile(cellPosition) == Tiles[3])
            {
                //  Top right Corner
                Vector3 Pos = player.transform.position;
                if (Pos.x > grid.GetCellCenterWorld(cellPosition).x) { Pos.x = grid.GetCellCenterWorld(cellPosition).x; }
                if (Pos.y > grid.GetCellCenterWorld(cellPosition).y) { Pos.y = grid.GetCellCenterWorld(cellPosition).y; }
                Pos.z = grid.GetCellCenterWorld(cellPosition).z;
                MainCamera.transform.position = Pos;
            }
            else if (tilemap.GetTile(cellPosition) == Tiles[4])
            {
                //  Large Room left
                Vector3 Pos = player.transform.position;
                if (Pos.x < grid.GetCellCenterWorld(cellPosition).x) { Pos.x = grid.GetCellCenterWorld(cellPosition).x; }
                Pos.z = grid.GetCellCenterWorld(cellPosition).z;
                MainCamera.transform.position = Pos;
            }
            else if (tilemap.GetTile(cellPosition) == Tiles[5])
            {
                // Large Room Center
                Vector3 Pos = player.transform.position;
                Pos.z = grid.GetCellCenterWorld(cellPosition).z;
                MainCamera.transform.position = Pos;
            }
            else if (tilemap.GetTile(cellPosition) == Tiles[6])
            {
                // Large Room Right
                Vector3 Pos = player.transform.position;
                if (Pos.x > grid.GetCellCenterWorld(cellPosition).x) { Pos.x = grid.GetCellCenterWorld(cellPosition).x; }
                Pos.z = grid.GetCellCenterWorld(cellPosition).z;
                MainCamera.transform.position = Pos;
            }
            else if (tilemap.GetTile(cellPosition) == Tiles[7])
            {
                // Bottom left Corner
                Vector3 Pos = player.transform.position;
                if (Pos.x < grid.GetCellCenterWorld(cellPosition).x) { Pos.x = grid.GetCellCenterWorld(cellPosition).x; }
                if (Pos.y < grid.GetCellCenterWorld(cellPosition).y) { Pos.y = grid.GetCellCenterWorld(cellPosition).y; }
                Pos.z = grid.GetCellCenterWorld(cellPosition).z;
                MainCamera.transform.position = Pos;
            }
            else if (tilemap.GetTile(cellPosition) == Tiles[8])
            {
                // Large Room Bottom Center
                Vector3 Pos = player.transform.position;
                if (Pos.y < grid.GetCellCenterWorld(cellPosition).y) { Pos.y = grid.GetCellCenterWorld(cellPosition).y; }
                Pos.z = grid.GetCellCenterWorld(cellPosition).z;
                MainCamera.transform.position = Pos;
            }
            else if (tilemap.GetTile(cellPosition) == Tiles[9])
            {
                // Bottom right Corner
                Vector3 Pos = player.transform.position;
                if (Pos.x > grid.GetCellCenterWorld(cellPosition).x) { Pos.x = grid.GetCellCenterWorld(cellPosition).x; }
                if (Pos.y < grid.GetCellCenterWorld(cellPosition).y) { Pos.y = grid.GetCellCenterWorld(cellPosition).y; }
                Pos.z = grid.GetCellCenterWorld(cellPosition).z;
                MainCamera.transform.position = Pos;
            }
            else if (tilemap.GetTile(cellPosition) == Tiles[10])
            {
                // Vertival corridor top
                Vector3 Pos = player.transform.position;
                if (Pos.y > grid.GetCellCenterWorld(cellPosition).y) { Pos.y = grid.GetCellCenterWorld(cellPosition).y; }
                Pos.x = grid.GetCellCenterWorld(cellPosition).x;
                Pos.z = grid.GetCellCenterWorld(cellPosition).z;
                MainCamera.transform.position = Pos;
            }
            else if (tilemap.GetTile(cellPosition) == Tiles[11])
            {
                // Vertival corridor center
                Vector3 Pos = player.transform.position;
                Pos.x = grid.GetCellCenterWorld(cellPosition).x;
                Pos.z = grid.GetCellCenterWorld(cellPosition).z;
                MainCamera.transform.position = Pos;
            }
            else if (tilemap.GetTile(cellPosition) == Tiles[12])
            {
                //  Vertival corridor bottom
                Vector3 Pos = player.transform.position;
                if (Pos.y < grid.GetCellCenterWorld(cellPosition).y) { Pos.y = grid.GetCellCenterWorld(cellPosition).y; }
                Pos.x = grid.GetCellCenterWorld(cellPosition).x;
                Pos.z = grid.GetCellCenterWorld(cellPosition).z;
                MainCamera.transform.position = Pos;
            }
            else if (tilemap.GetTile(cellPosition) == Tiles[13])
            {
                // Horizontal corridor left
                Vector3 Pos = player.transform.position;
                if (Pos.x < grid.GetCellCenterWorld(cellPosition).x) { Pos.x = grid.GetCellCenterWorld(cellPosition).x; }
                Pos.y = grid.GetCellCenterWorld(cellPosition).y;
                Pos.z = grid.GetCellCenterWorld(cellPosition).z;
                MainCamera.transform.position = Pos;
            }
            else if (tilemap.GetTile(cellPosition) == Tiles[14])
            {
                // Horizontal corridor center
                Vector3 Pos = player.transform.position;
                Pos.y = grid.GetCellCenterWorld(cellPosition).y;
                Pos.z = grid.GetCellCenterWorld(cellPosition).z;
                MainCamera.transform.position = Pos;
            }
            else if (tilemap.GetTile(cellPosition) == Tiles[15])
            {
                // Horizontal corridor right
                Vector3 Pos = player.transform.position;
                if (Pos.x > grid.GetCellCenterWorld(cellPosition).x) { Pos.x = grid.GetCellCenterWorld(cellPosition).x; }
                Pos.y = grid.GetCellCenterWorld(cellPosition).y;
                Pos.z = grid.GetCellCenterWorld(cellPosition).z;
                MainCamera.transform.position = Pos;
            }

            ForegroundMap.gameObject.SetActive(false);

        }
        else
        {

            playerDot.transform.position = tilemap.GetCellCenterWorld(cellPosition);
            ForegroundMap.gameObject.SetActive(true);

        }
    }
}
