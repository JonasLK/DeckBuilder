using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("PlayerInfo")]
    public int currentHealth = 100;
    public int maxHealth = 100;
    public int blockAmount;
    public int cardsDrawnStartTurn = 1;
    public int cardsDrawnStartCombat = 5;
    public int startEnergy = 3;
    public int energy = 3;
    public int maxHandSize = 7;

    [Header("CombatInfo")]
    public int strength;
    public int weak;
    public int exposed;
    public bool loseHalfBlock;

    [Header("UI")]
    public TextMeshProUGUI healthDisplay;
    public TextMeshProUGUI shieldDisplay;
    public TextMeshProUGUI energyDisplay;
    public TextMeshProUGUI strengthenDisplay;
    public TextMeshProUGUI weakDisplay;
    public TextMeshProUGUI exposedDisplay;

    public void Awake()
    {
        UpdateHealthDisplay();
        UpdateEnergyDisplay();
    }

    public void TakeDamage(int damageTaken)
    {
        if (blockAmount > 0)
        {
            blockAmount -= damageTaken;
            if (blockAmount < 0)
            {
                currentHealth += blockAmount;
            }
        }
        else
        {
            currentHealth -= damageTaken;
        }

        UpdateHealthDisplay();

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void UpdateHealthDisplay()
    {
        healthDisplay.text = currentHealth.ToString();
        shieldDisplay.text = blockAmount.ToString();
    }

    public void UpdateStrengthDisplay()
    {
        if(strengthenDisplay.gameObject.activeSelf == false && strength > 0)
        {
            strengthenDisplay.gameObject.SetActive(true);
        }
        strengthenDisplay.text = strength.ToString();
    }

    public void UpdateWeakDisplay()
    {
        if(weak > 0 && weakDisplay.gameObject.activeSelf == false)
        {
            weakDisplay.gameObject.SetActive(true);
        }
        else if(weak == 0 && weakDisplay.gameObject.activeSelf == true)
        {
            weakDisplay.gameObject.SetActive(false);
        }
        weakDisplay.text = weak.ToString();
    }

    public void UpdateExposedDisplay()
    {
        if (exposed > 0 && exposedDisplay.gameObject.activeSelf == false)
        {
            exposedDisplay.gameObject.SetActive(true);
        }
        else if (weak == 0 && exposedDisplay.gameObject.activeSelf == true)
        {
            exposedDisplay.gameObject.SetActive(false);
        }
        exposedDisplay.text = exposed.ToString();
    }

    public void UpdateEnergyDisplay()
    {
        energyDisplay.text = energy.ToString() + "/" + startEnergy.ToString();
    }

    public void Death()
    {
        print("oh no, I'm die");
    }
}