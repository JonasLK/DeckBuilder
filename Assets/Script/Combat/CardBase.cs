using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBase : MonoBehaviour
{
    public delegate void CardAction();
    public CardAction cardAction;
    public delegate void WhenUpgraded();
    public WhenUpgraded whenUpgraded;

    [HideInInspector]public Card card;
    [HideInInspector] public string cardName;
    [HideInInspector] public string cardDiscription;

    [Header ("Card Type")]
    public bool damageCard;
    public bool blockCard;
    public bool strengthenCard;
    public bool drawCardCard;
    public bool weakenCard;
    public bool exposeCard;
    public bool loseHalfBlockCard;
    public bool addCardCard;

    [Header("Card Info")]
    public bool upgraded;
    public bool forbidden;
    public bool nonPlayable;
    public int cost;
    public int blockValue;
    public int attackValue;
    public int attackTimes;
    public int targetAmount = 1;

    /*[Header("X-card")]
    public bool xAmount;
    public int repeatAmount;*/

    [Header("Targeting")]
    public bool targetAll;
    public bool targeting;
    public bool targetSelf;
    public bool targetEnemy;
    public bool targetMulti;
    public List<Enemy> targets;

    [Header("AddCard")]
    public List<Card> cardsToAdd;
    public int cardsToDraw;

    [Header("BuffStuff")]
    public int strengthIncrease;

    [Header("DebuffStuff")]
    public int weakenDuration;
    public int exposeDuration;

    private int forCounter;

    public void Update()
    {
        if (targeting == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnTargetSelect();
            }
        }
    }

    public void DoDamage()
    {
        foreach (Enemy enemy in targets)
        {
            for (int i = 0; i < attackTimes; i++)
            {
                if (CombatManager.player.weak > 0)
                {
                    enemy.TakeDamage(Mathf.RoundToInt((attackValue + CombatManager.player.strength) / 2));
                }
                else
                {
                    enemy.TakeDamage(attackValue + CombatManager.player.strength);
                }
            }
        }
    }

    public void ApplyBlock()
    {
        CombatManager.player.blockAmount += blockValue;
        CombatManager.player.UpdateHealthDisplay();
    }

    public void Strenghten()
    {
        CombatManager.player.strength += strengthIncrease;
        CombatManager.player.UpdateStrengthDisplay();
    }

    public void DrawCard()
    {
        CombatManager.pileAndHandManager.DrawCardFromDeckPile(cardsToDraw);
    }

    public void LoseHalfBlock()
    {
        CombatManager.player.loseHalfBlock = true;
    }
    public void AddCardCard()
    {
        foreach(Card card in cardsToAdd)
        {
            CombatManager.pileAndHandManager.AddCardToHand(card);
        }
    }

    public void Weaken()
    {
        foreach (Enemy target in targets)
        {
            target.weak += weakenDuration;
        }
    }

    public void Expose()
    {
        foreach (Enemy target in targets)
        {
            target.exposed += exposeDuration;
        }
    }

    public void OnClick()
    {
        print("OnClick");
        foreach(Card card in CombatManager.pileAndHandManager.hand)
        {
            targets.Clear();
            if(card != this)
            {
                targeting = false;
            }
        }
        targeting = !targeting;
    }

    public void OnTargetSelect()
    {
        RaycastHit hit;
        Ray ray = CombatManager.camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Enemy")) && targetEnemy == true)
        {
            if(targetAll == false)
            {
                targets.Add(hit.transform.gameObject.GetComponent<Enemy>());
                if(targets.Count == targetAmount)
                {
                    targeting = false;
                    PlayableCheck();
                }
            }
            else
            {
                foreach(Enemy enemy in CombatManager.enemies)
                {
                    targets.Add(enemy);
                }
                targeting = false;
                PlayableCheck();
            }
        }
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Player")) && targetSelf == true)
        {
            targeting = false;
            PlayableCheck();
        }
    }

    public void PlayableCheck()
    {
        if(nonPlayable == false)
        {
            /*if(xAmount == false)
            {*/
                if(cost > CombatManager.player.energy)
                {
                    return;
                }
                else
                {
                    cardAction();
                    CombatManager.player.energy -= cost;
                    CombatManager.player.UpdateEnergyDisplay();
                    CombatManager.pileAndHandManager.PlayCard(forbidden, card, gameObject);
                }
            /*}
            else
            {
                cardAction();
                repeatAmount = CombatManager.player.energy;
                CombatManager.player.energy = 0;
                CombatManager.player.UpdateEnergyDisplay();
                CombatManager.pileAndHandManager.PlayCard(forbidden, gameObject);
            }*/
        }
    }
}