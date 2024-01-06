using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Player player;

    public GameObject _attackCheck;
    public LayerMask _enemyLayer;
    public List<string> _comboSequence = new List<string>();
    private float _comboTimer = 0f;
    [SerializeField]private float _comboMaxTime = 90.0f;
    [SerializeField]private float _maxCombo = 10f;
    private float _attackCooldown = 0.5f;
    private float _attackCooldownTimer = 0f;
    private float _attackSpeedDump = 0f;
    public void UpdateComboTimer()
    {
        if(_comboSequence.Count > 0)
        {
            _comboTimer += Time.deltaTime;
            if(_comboTimer > _comboMaxTime)
            {
                ResetCombo();
            }
        }
    }
    public void ResetCombo()
    {
        Debug.Log("Reset Combo");
        _comboSequence.Clear();
        _comboTimer = 0f;
        player.animation.AttackUpdate(0, 0);
        _attackCooldown = _attackSpeedDump;
    }

    public void UpdateAttackCooldown()
    {
        if (_attackCooldown > 0f)
        {
            _attackCooldown -= Time.deltaTime;
        }
    }

    public void ProcessAttackInput()
    {
        if (_attackCooldown <= 0f)
        {
            CheckAndAddCombo(KeyCode.I, "I");
            CheckAndAddCombo(KeyCode.O, "O");
            CheckAndAddCombo(KeyCode.P, "P");
            CheckAndAddCombo(KeyCode.J, "J");
        }
    }
    public float staminaUse = 0f;
    private void CheckAndAddCombo(KeyCode key, string combo)
    {
        
        if (Input.GetKeyDown(key))
        {
            if ((player.Stamina - staminaUse) <= 0f)
            {
                return;
            }
            player.Stamina -= staminaUse;
            AddCombo(combo);

            _attackCooldown = _attackCooldownTimer;
        }
    }
    private void AddCombo(string combo)
    {
        _comboSequence.Add(combo);
        Debug.Log(combo);

        if(_comboSequence.Count >= _maxCombo)
        {
            ResetCombo();
            return;
        }
        _comboTimer += Time.deltaTime;
        if(_comboTimer > _comboMaxTime)
        {
            ResetCombo();
            return;
        }

        string lastCombo = _comboSequence[_comboSequence.Count - 1];
        string beforeCombo;

        float damage = 0f;
        float DeltaS = 0f;

       
        switch (lastCombo)
        {
            case "I":
                ChangeSpriteColor(new Color(0.1f, -0.1f, -0.1f, 0f));
                player.animator.SetInteger("AttackType", 1);
                DeltaS = ((player.sorte + player.inventory.CurrentWeapon.critChance) % (UnityEngine.Random.Range(1, 100) / 100)) / 100;
                if (DeltaS > 50)
                {
                    damage = (player.forca * (player.sorte / DeltaS * 2)) + player.inventory.CurrentWeapon.damage + 1;
                }
                else
                {
                    damage = (player.forca) + player.inventory.CurrentWeapon.damage + 1;
                }
                break;
            //O = Verde = Rapido
            case "O":
                ChangeSpriteColor(new Color(-0.1f, 0.1f, -0.1f, 0f));
                damage = (player.forca * 0.5f) + player.inventory.CurrentWeapon.damage + 1;
                _attackCooldown = _attackCooldownTimer - (player.destresa - player.inventory.CurrentWeight);
                break;

            //P = Azul = Magico
            case "P":
                ChangeSpriteColor(new Color(-0.1f, -0.1f, 0.1f, 0f));
                DeltaS = ((player.sorte + player.inventory.CurrentWeapon.critChance) % (UnityEngine.Random.Range(1, 100) / 100)) / 100;
                damage = (player.energia * (player.sorte / DeltaS));
                break;
            case "J":
                ChangeSpriteColor(new Color(0.1f, 0.1f, 0.1f, 0f));
                break;
        }
        MakeDamage(damage);
        Debug.Log(_comboSequence.Count);
        if (_comboSequence.Count >= 10)
        {
            ResetCombo();
            return;
        }

        player.animator.SetTrigger("Attack");
    }
    private void ChangeSpriteColor(Color change)
    {
        player.animation.sprite.color += change;
    }
    private void MakeDamage(float damage)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackCheck.transform.position, _attackRadius, _enemyLayer);
        Debug.Log(colliders.Length+" Enemeyes Hit");
        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<EnemyBase>())
            {
                collider.gameObject.GetComponent<EnemyBase>().TakeDamage(-damage);
            }
        }
    }
    private float _attackRadius = 0.5f;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackCheck.transform.position, _attackRadius);
    }
}