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

    private void Awake()
    {

    }

    private void Update()
    {
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
                Player.spriteRenderer.flipX = Mathf.RoundToInt(Player.Rigidbody2D.velocity.x) < 0;
            }
            else
            {
                Player.animator.SetBool("isRun", false);
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
            string playerInfo = $"Velocidade: {Player.Rigidbody2D.velocity}\nNo chão: {Player.inGround}";
            GUI.Box(new Rect(10, 10, 200, 50), playerInfo);
        }
    }

}
