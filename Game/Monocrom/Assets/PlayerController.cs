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
    if(Mathf.RoundToInt(Player.Rigidbody2D.velocity.x) != 0){
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
    // Adicione esta linha para definir a rotação para um valor padrão quando o jogador não está se movendo
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
