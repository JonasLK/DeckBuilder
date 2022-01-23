using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/Card", order = 1)]
public class Card : ScriptableObject
{
    public GameObject cardObject;

    public string cardName;
    public string cardDiscription;

    [Header("Card Type")]
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
    public int attackTimes = 1;
    public int targetAmount = 1;

    /*[Header("X-card")]
    public bool xAmount;
    public int repeatAmount;*/

    [Header("Targeting")]
    public bool targeting;
    public bool targetAll;
    public bool targetSelf;
    public bool targetEnemy;
    public bool targetMulti;

    [Header("AddCard")]
    public List<Card> cardsToAdd;
    public int cardsToDraw;

    [Header("BuffStuff")]
    public int strengthIncrease;

    [Header("DebuffStuff")]
    public int weakenDuration;
    public int exposeDuration;

    public void CardBaseAwake(CardBase cardBase)
    {
        CreateCard(cardBase);
        cardBase.cardName = cardName;
        cardBase.cardDiscription = cardDiscription;
    }
    public void CreateCard(CardBase cardBase)
    {
        cardBase.upgraded = upgraded;
        cardBase.forbidden = forbidden;
        cardBase.nonPlayable = nonPlayable;
        cardBase.cost = cost;

        cardBase.targeting = targeting;
        cardBase.targetAll = targetAll;
        cardBase.targetSelf = targetSelf;
        cardBase.targetEnemy = targetEnemy;
        cardBase.targetMulti = targetMulti;

        if (strengthenCard == true)
        {
            //Debug.Log("Strength added");
            cardBase.strengthenCard = strengthenCard;
            cardBase.strengthIncrease = strengthIncrease;
            cardBase.cardAction += cardBase.Strenghten;
        }

        if (loseHalfBlockCard == true)
        {
            //Debug.Log("LoseHalfBlock added");
            cardBase.loseHalfBlockCard = loseHalfBlockCard;
            cardBase.cardAction += cardBase.LoseHalfBlock;
        }

        if (weakenCard == true)
        {
            //Debug.Log("Weaken added");
            cardBase.weakenCard = weakenCard;
            cardBase.weakenDuration = weakenDuration;
            cardBase.cardAction += cardBase.Weaken;
        }

        if (exposeCard == true)
        {
            //Debug.Log("Exposed added");
            cardBase.exposeCard = exposeCard;
            cardBase.exposeDuration = exposeDuration;
            cardBase.cardAction += cardBase.Expose;
        }

        if (blockCard == true)
        {
            //Debug.Log("Block added");
            cardBase.blockCard = blockCard;
            cardBase.blockValue = blockValue;
            cardBase.cardAction += cardBase.ApplyBlock;
        }

        if (damageCard == true)
        {
            //Debug.Log("Damage added");
            cardBase.damageCard = damageCard;
            cardBase.attackValue = attackValue;
            cardBase.attackTimes = attackTimes;
            cardBase.cardAction += cardBase.DoDamage;
        }

        if(drawCardCard == true)
        {
            //Debug.Log("DrawCard added");
            cardBase.drawCardCard = drawCardCard;
            cardBase.cardsToDraw = cardsToDraw;
            cardBase.cardAction += cardBase.DrawCard;
        }

        if (addCardCard == true)
        {
            //Debug.Log("AddCardCard added");
            cardBase.addCardCard = addCardCard;
            foreach(Card cardToAdd in cardsToAdd)
            {
                cardBase.cardsToAdd.Add(cardToAdd);
            }
            cardBase.cardAction += cardBase.AddCardCard;
        }
    }
}