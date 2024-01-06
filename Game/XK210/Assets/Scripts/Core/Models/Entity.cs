using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Entity: MonoBehaviour
{
    public delegate void OnLifeChangedDelegate(float life);
    public event OnLifeChangedDelegate OnLifeChanged;
    public float maxLife;
    private float _life;
    public float Life
    {
        get { return _life; }
        set
        {
            if (_life != value)
            {
                _life = value;
                OnLifeChanged?.Invoke(_life);
                Debug.Log(_life);
                if (_life <= 0)
                    OnDeathAnimationEnd();
            }
        }
    }

    public delegate void OnPostureChangedDelegate(float posture);
    public event OnPostureChangedDelegate OnPostureChanged;
    public float maxPosture;
    private float _posture;
    public float Posture
    {
        get { return _posture; }
        set
        {
            if(_posture != value)
            {
                _posture = value;
                OnPostureChanged?.Invoke(_posture);
            }
        }
    }

    public delegate void OnStaminaChangedDelegate(float stamina);
    public event OnStaminaChangedDelegate OnStaminaChanged;
    public float maxStamina;
    private float _stamina;
    public float Stamina
    {
        get { return _stamina; }
        set
        {
            if(_stamina != value)
            {
                _stamina = value;
                OnStaminaChanged?.Invoke(_stamina);
            }
        }
    }

    public Animator animator;
    public SpriteRenderer renderer;
    public Collider2D collider;
    public Collider2D hitBox;
    public float wallCheckDistance;
    public float speed;
    public float flySpeed;
    public float jumpForce;
    public float wallJumpingDuration = 0.1f;

    public bool canFly = false;

    public float armor;
    public List<Idiomas> idiomas;

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

    public void TakeDamage(float damage)
    {
        _life = damage;
        animator.SetTrigger("TakeDamage");
        ShowFloatingText(damage);
        
    }
    public void Healing(float Heal)
    {
        _life += Heal;
        if (_life > maxLife)
            _life = maxLife;
        ShowFloatingText(Heal);
    }
    private void ShowFloatingText(float amount)
    {
        var instance = ObjectPooler.Instance.AddPooledObject(); // Assuming you have an ObjectPooler class
        instance.transform.position = transform.position;
        instance.SetActive(true);

        TMP_Text text = instance.GetComponentInChildren<TMP_Text>();
        if (amount < 0)
        {
            text.text = "<shake>" + amount.ToString() + "</shake>";
            text.color = Color.red;
        }
        else
        {
            text.text = "<swing>" + amount.ToString() + "</swing>";
            text.color = Color.green;
        }
    }
    public void OnDeathAnimationEnd()
    {
        Destroy(gameObject);
    }
    public void Born()
    {
        _life = maxLife;
    }
}