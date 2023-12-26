using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat
{
    public Player player;

    public GameObject _attackCheck;
    public LayerMask _enemyLayer;
    public List<string> _comboSequence = new List<string>();
    private float _comboTimer = 0f;
    private float _comboMaxTime = 2.0f;
    private float _maxCombo = 10f;
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

    private void CheckAndAddCombo(KeyCode key, string combo)
    {
        if (Input.GetKeyDown(key))
        {
            AddCombo(combo);
            _attackCooldown = _attackCooldownTimer;
        }
    }
    private void AddCombo(string combo)
    {
        _comboSequence.Add(combo);
        if(_comboSequence.Count > _maxCombo)
        {
            ResetCombo();
            return;
        }
        _comboTimer = _comboMaxTime;

        string lastCombo = _comboSequence[_comboSequence.Count - 1];
        string beforeCombo;

        try
        {
            beforeCombo = _comboSequence[_comboSequence.Count - 2];
        }
        catch (Exception e)
        {
            beforeCombo = "";
        }

        float damage = 0f;
        float DeltaS = 0f;

        switch (lastCombo)
        {
            case "I":
                player.animation.sprite.color -= new Color(0f, 0.1f, 0.1f, 0f);
                player.animation.sprite.color += new Color(0.1f, 0f, 0f, 0f);
                player.animation.AttackUpdate(_comboSequence.Count,1);
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
                player.animation.sprite.color -= new Color(0.1f, 0f, 0.1f, 0f);
                player.animation.sprite.color += new Color(0f, 0.1f, 0f, 0f);
                player.animation.AttackUpdate(_comboSequence.Count, 2);
                damage = (player.forca * 0.5f) + player.inventory.CurrentWeapon.damage + 1;
                _attackCooldown = _attackCooldownTimer - (player.destresa - player.inventory.CurrentWeight);
                break;

            //P = Azul = Magico
            case "P":
                player.animation.sprite.color -= new Color(0.1f, 0.1f, 0f, 0f);
                player.animation.sprite.color += new Color(0f, 0f, 0.1f, 0f);
                player.animation.AttackUpdate(_comboSequence.Count, 3);
                DeltaS = ((player.sorte + player.inventory.CurrentWeapon.critChance) % (UnityEngine.Random.Range(1, 100) / 100)) / 100;
                damage = (player.energia * (player.sorte / DeltaS));
                break;
            case "J":
                player.animation.sprite.color -= new Color(0.1f, 0.1f, 0.1f, 0f);
                player.animation.sprite.color += new Color(0.1f, 0.1f, 0.1f, 0f);
                player.animation.AttackUpdate(_comboSequence.Count, 4);
                break;
        }
        MakeDamage(damage);
        if (_comboSequence.Count == 10)
        {
            ResetCombo();
            return;
        }

        player.animator.SetTrigger("Attack");
    }
    private void MakeDamage(float damage)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackCheck.transform.position, 0.2f, _enemyLayer);
        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<Enemy>())
            {
                collider.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }
}