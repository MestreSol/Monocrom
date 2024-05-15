using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MinimapRender : MonoBehaviour
{

    public static MinimapRender Instance;
    public Camera camera;
    public RawImage minimapImage;  
    // Pega todas as áreas do minimapa e renderiza as reveladas em um texture2D
    public Texture2D RenderMinimap(Dictionary<Vector2Int, MinimapArea> minimapAreas)
    {
        int width = 0;
        int height = 0;
        foreach (var minimapArea in minimapAreas.Values)
        {
            if (minimapArea.Position.x + minimapArea.Size.x > width)
            {
                width = minimapArea.Position.x + minimapArea.Size.x;
            }
            if (minimapArea.Position.y + minimapArea.Size.y > height)
            {
                height = minimapArea.Position.y + minimapArea.Size.y;
            }
        }

        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;

        foreach (var minimapArea in minimapAreas.Values)
        {
            if (!minimapArea.IsRevealed)
            {
                continue;
            }

            for (int x = 0; x < minimapArea.Size.x; x++)
            {
                for (int y = 0; y < minimapArea.Size.y; y++)
                {
                    texture.SetPixel(minimapArea.Position.x + x, minimapArea.Position.y + y, Color.white);
                }
            }
        }

        texture.Apply();
        return texture;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(RenderMinimap());
    }

    private IEnumerator RenderMinimap()
    {
        while (true)
        {
            minimapImage.texture = RenderMinimap(MinimapController.Instance.minimapAreas);
            yield return new WaitForSeconds(1);
        }
    }

    private void Update()
    {
        camera.Render();
    }



}
