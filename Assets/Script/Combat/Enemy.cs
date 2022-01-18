using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public delegate void DecidedEnemyAction();
    public DecidedEnemyAction decidedEnemyAction;

    [Header("EnemyInfo")]
    public int health;
    public int blockAmount;
    public int weak;
    public int exposed;
    public int strength;

    [Header("UI")]
    public TextMeshProUGUI healthDisplay;
    public TextMeshProUGUI shieldDisplay;
    public TextMeshProUGUI strengthenDisplay;
    public TextMeshProUGUI weakDisplay;
    public TextMeshProUGUI exposedDisplay;
    public GameObject attackDisplay;
    public GameObject blockDisplay;
    public GameObject buffDisplay;
    public GameObject debuffDisplay;

    [Header("EnemyValues")]
    public int minAttackValue;
    public int maxAttackValue;
    public int minBlockValue;
    public int maxBlockValue;
    public int minStrengthenAmount;
    public int maxStrengthenAmount;
    public int minWeakenDuration;
    public int maxWeakenDuration;
    public int minExposeDuration;
    public int maxExposeDuration;

    [Header("ActionChances")]
    public int attackChance;
    public int blockChance;
    public int strengthenChance;
    public int weakenChance;
    public int exposeChance;

    [HideInInspector]public GameObject self;

    private int enemyActionInt;

    public void Awake()
    {
        self = gameObject;
        CombatManager.delayStart += DelayedStart;
    }

    public void DelayedStart()
    {
        CombatManager.enemies.Add(self.GetComponent<Enemy>());
        UpdateHealthDisplay();
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        if(blockAmount > 0)
        {
            blockAmount -= damage;
            if(blockAmount < 0)
            {
                health += blockAmount;
                blockAmount = 0;
            }
        }
        else
        {
            health -= damage;
        }

        if(health <= 0)
        {
            health = 0;
            Death();
        }

        UpdateHealthDisplay();
    }

    public void UpdateHealthDisplay()
    {
        healthDisplay.text = health.ToString();
        shieldDisplay.text = blockAmount.ToString();
    }

    public void UpdateStrengthDisplay()
    {
        if (strengthenDisplay.gameObject.activeSelf == false && strength > 0)
        {
            strengthenDisplay.gameObject.SetActive(true);
        }
        strengthenDisplay.text = strength.ToString();
    }

    public void UpdateWeakDisplay()
    {
        if (weak > 0 && weakDisplay.gameObject.activeSelf == false)
        {
            weakDisplay.gameObject.SetActive(true);
        }
        else if (weak == 0 && weakDisplay.gameObject.activeSelf == true)
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

    public void EnemyAction()
    {
        if(attackDisplay.activeSelf == true)
        {
            attackDisplay.SetActive(false);
        }
        if (blockDisplay.activeSelf == true)
        {
            blockDisplay.SetActive(false);
        }
        if (buffDisplay.activeSelf == true)
        {
            buffDisplay.SetActive(false);
        }
        if (debuffDisplay.activeSelf == true)
        {
            debuffDisplay.SetActive(false);
        }
        decidedEnemyAction();
        EnemyIntent();
        UpdateHealthDisplay();
    }

    public void EnemyIntent()
    {
        int action = Random.Range(1, (attackChance + blockChance + strengthenChance + weakenChance + exposeChance + 1));
        if(action<= attackChance)
        {
            enemyActionInt = Random.Range(minAttackValue, maxAttackValue + 1) + strength;
            if(CombatManager.player.exposed > 0)
            {
                enemyActionInt = Mathf.RoundToInt(enemyActionInt * 1.5f);
            }
            decidedEnemyAction = DoDamage;
            attackDisplay.GetComponentInChildren<TextMeshProUGUI>().text = enemyActionInt.ToString();
            attackDisplay.SetActive(true);
            print(gameObject.name + " has decided to attack");
        }
        else if(action<= blockChance + attackChance)
        {
            enemyActionInt = Random.Range(minBlockValue, maxBlockValue + 1);
            decidedEnemyAction = Block;
            blockDisplay.GetComponentInChildren<TextMeshProUGUI>().text = enemyActionInt.ToString();
            blockDisplay.SetActive(true);
            print(gameObject.name + " has decided to Block");
        }
        else if(action<= strengthenChance + blockChance + attackChance)
        {
            enemyActionInt = Random.Range(minStrengthenAmount, maxStrengthenAmount + 1);
            decidedEnemyAction = Strengthen;
            buffDisplay.SetActive(true);
            print(gameObject.name + " has decided to buff itself");
        }
        else if(action<= weakenChance + strengthenChance + blockChance + attackChance)
        {
            enemyActionInt = Random.Range(minWeakenDuration, maxWeakenDuration + 1);
            decidedEnemyAction = Weaken;
            debuffDisplay.SetActive(true);
            print(gameObject.name + " has decided to weaken player");
        }
        else if(action<= exposeChance + weakenChance + strengthenChance + blockChance + attackChance)
        {
            enemyActionInt = Random.Range(minExposeDuration, maxExposeDuration + 1);
            decidedEnemyAction = Expose;
            debuffDisplay.SetActive(true);
            print(gameObject.name + " has decided to expose player");
        }
    }

    public void StartEnemyTurn()
    {
        if(blockAmount > 0)
        {
            blockAmount = 0;
            UpdateHealthDisplay();
        }
        if(weak > 0)
        {
            weak -= 1;
            if(weak == 0)
            {
                weakDisplay.gameObject.SetActive(false);
            }
        }
        if (exposed > 0)
        {
            exposed -= 1;
            if (exposed == 0)
            {
                exposedDisplay.gameObject.SetActive(false);
            }
        }
    }

    public void Death()
    {
        print("oh no, I'm die");
        healthDisplay.gameObject.SetActive(false);
        shieldDisplay.gameObject.SetActive(false);
        CombatManager.enemies.Remove(gameObject.GetComponent<Enemy>());
        CombatManager.CheckForPlayerVictory();
        gameObject.SetActive(false);
    }

    public void DoDamage()
    {
        CombatManager.player.TakeDamage(enemyActionInt);
    }

    public void Block()
    {
        blockAmount += enemyActionInt;
    }

    public void Strengthen()
    {
        strength += enemyActionInt;
    }

    public void Weaken()
    {
        CombatManager.player.weak += enemyActionInt;
    }

    public void Expose()
    {
        CombatManager.player.exposed += enemyActionInt;
    }
}