using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerProgress PlayerProgress;
    public Player Player;

    private bool isFall = false;
    private bool isJump = false;
    private bool isRun = false;
    private bool isWallSliding = false;
    public Material colorReplaceMaterial;
    public Texture2D colorMap;

    void Start()
    {
        //colorReplaceMaterial.SetTexture("_ColorMap", colorMap);
        //GetComponent<SpriteRenderer>().material = colorReplaceMaterial;
    }
    public void ApplyColors(Color[] colors, Color[] replacements)
    {
        // Obtenha o Renderer do jogador
        Renderer renderer = GetComponent<Renderer>();

        // Crie uma nova instância do Material com o shader
        Material material = new Material(Shader.Find("Custom/MultipleColorReplace"));

        // Crie a textura de cores
        Texture2D colorTex = new Texture2D(colors.Length, 1);
        for (int i = 0; i < colors.Length; i++)
        {
            colorTex.SetPixel(i, 0, replacements[i]);
        }
        colorTex.Apply();

        // Defina a textura de cores do shader
        material.SetTexture("_ColorTex", colorTex);

        // Aplique o material ao jogador
        renderer.material = material;
    }
    private void Update()
    {
        isWallSliding = Physics2D.Raycast(Player.WallCheck.transform.position, Player.WallCheck.transform.right, Player.wallCheckDistance, Player.wallLayer) && GetComponent<Rigidbody2D>().velocity.y < 0;
        if (isWallSliding)
        {
        }
        if (Input.GetButtonDown("Jump"))
        {
            Player.Jump();
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            Player.Move(new Vector2(Input.GetAxis("Horizontal"), 0));

        }
        Player.CanJump();
        if (!Player.inGround)
        {
            if (Mathf.RoundToInt(Player.Rigidbody2D.velocity.y) > 0)
            {
                ResetStates();
                Player.animator.SetBool("isJump", true);

            }
            else if (Mathf.RoundToInt(Player.Rigidbody2D.velocity.y) < 0)
            {
                ResetStates();
                Player.animator.SetBool("isFall", true);
            }
        }
        else
        {
            ResetStates();
            Player.animator.SetBool("InGround", Player.inGround);

            if (!Mathf.Approximately(Player.Rigidbody2D.velocity.x, 0))
            {
                ResetStates();
                Player.animator.SetBool("isRun", Mathf.RoundToInt(Player.Rigidbody2D.velocity.x) != 0);
                if (Mathf.RoundToInt(Player.Rigidbody2D.velocity.x) != 0)
                {
                    float direction = Mathf.Sign(Player.Rigidbody2D.velocity.x);
                    if (direction < 0)
                    {
                        gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                    if (direction > 0)
                    {
                        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                }
            }
            else
            {
                Player.animator.SetBool("isRun", false);
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
    public void ResetStates()
    {
        Player.animator.SetBool("isFall", false);
        Player.animator.SetBool("isJump", false);
        Player.animator.SetBool("isRun", false);
        Player.animator.SetBool("InGround", false);
    }
    void OnGUI()
    {
        if (Debug.isDebugBuild)
        {
            string playerInfo = $"Velocidade: {Player.Rigidbody2D.velocity}\nNo ch�o: {Player.inGround}";
            GUI.Box(new Rect(10, 10, 200, 50), playerInfo);
        }
    }

}
