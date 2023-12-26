using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Entity: MonoBehaviour
{
    [Header("Unity Objects")]
    public Animator animator;
    public SpriteRenderer renderer;
    public Collider2D collider;
    public float wallCheckDistance;
    public float speed;
    public float flySpeed;
    public float jumpForce;
    public float wallJumpingDuration = 0.1f;

    public bool canFly = false;

    public float armor;
    public List<Idiomas> idiomas;

    public float life = 0;
    public float maxLife;
    public int forca = 0;
    public int destresa = 0;
    public int sorte = 0;
    public int persepcao = 0;
    public int inteligencia = 0;
    public int carisma = 0;
    public int energia = 0;
    public int jumpCount = 0;

    public bool inGround = false;
    public bool isWallSliding = false;
    public bool isFacingRight = false;
    public bool isWallJumping = false;
    public bool isJumping = false;

    public GameObject floatingTextPrefab;

    public void TakeDamage(float damage){
        life -= damage;
        animator.SetTrigger("TakeDamage");
        ShowFloatingText(damage);
        if(life <= 0)
            StartCoroutine(Die());
    }
    private IEnumerator Die()
    {
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    public void Healing(float Heal)
    {
        life += Heal;
        if (life > maxLife)
            life = maxLife;
        ShowFloatingText(Heal);
    }
    private void ShowFloatingText(float amount)
    {
        var instance = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
        instance.GetComponent<TextMeshPro>().text = amount.ToString();
        if(amount < 0)
        {
            instance.GetComponent<TextMeshPro>().color = Color.red;
        }
        else
        {
            instance.GetComponent<TextMeshPro>().color = Color.green;
        }
    }
}