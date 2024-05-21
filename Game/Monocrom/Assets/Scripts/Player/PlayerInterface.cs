using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInterface : MonoBehaviour
{
    public Slider sl_HelthBar;
    public Slider sl_StaminaBar;

    public void SetHealthBarValue(float value)
    {
        sl_HelthBar.value = value;
    }
    public void InitializeHealthBar(float maxHealth)
    {
        sl_HelthBar.maxValue = maxHealth;
        sl_HelthBar.value = maxHealth;
    }
    public void SetStaminaBarValue(float value)
    {
        sl_StaminaBar.value = value;
    }
    public void InitializeStaminaBar(float maxStamina)
    {
        sl_StaminaBar.maxValue = maxStamina;
        sl_StaminaBar.value = maxStamina;
    }
}
