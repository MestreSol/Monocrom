using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapAreaTransaction : MonoBehaviour
{
    public Vector2Int newAreaPosition;
    public Vector2Int newAreaSize;

    public Vector2Int oldAreaPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var mapArea = collision.GetComponent<MinimapArea>();
            if (mapArea != null)
            {
                var minimapArea = new MinimapArea
                {
                    Position = newAreaPosition,
                    Size = newAreaSize,
                    IsRevealed = true
                };
                MinimapController.Instance.AddMinimapArea(minimapArea);
            }
        }
    }
}
