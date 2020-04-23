using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image filledHealth;

    public void HealthChanged(int health, int maxHealth)
    {
        filledHealth.fillAmount = (float)health / (float)maxHealth;
    }
}
