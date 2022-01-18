using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class CardCreator : MonoBehaviour
{
    [Header("EditMode")]
    public bool EditCardMode = false;

    [Header("Must")]
    public List<GameObject> cardsToAdd;

    [Header("Other")]
    public CardBase cardBase;
    [SerializeField] private bool damage;
    [SerializeField] private bool defence;
    [SerializeField] private bool strength;
    [SerializeField] private bool loseHalfBlock;
    [SerializeField] private bool addCardCard;
    [SerializeField] private bool weak;
    [SerializeField] private bool exposed;

    void Awake()
    {
        cardBase = GetComponent<CardBase>();
        damage = cardBase.damageCard;
        defence = cardBase.blockCard;
        strength = cardBase.strengthenCard;
        loseHalfBlock = cardBase.loseHalfBlockCard;
        addCardCard = cardBase.addCardCard;
        weak = cardBase.weakenCard;
        exposed = cardBase.exposeCard;
    }

    public void AddOrRemoveDamageProperty(bool damage)
    {
        if(damage == true)
        {
            cardBase.cardAction += cardBase.DoDamage;
            print("DamagePropertyAdded");
        }
        else
        {
            cardBase.cardAction -= cardBase.DoDamage;
            print("DamagePropertyRemoved");
        }
    }

    public void AddOrRemoveBlockProperty(bool defence)
    {
        if (defence == true)
        {
            cardBase.cardAction += cardBase.ApplyBlock;
            print("DefencePropertyAdded");
        }
        else
        {
            cardBase.cardAction -= cardBase.ApplyBlock;
            print("DefencePropertyRemoved");
        }
    }

    public void AddOrRemoveStrengthenProperty(bool strength)
    {
        if (strength == true)
        {
            cardBase.cardAction += cardBase.Strenghten;
            print("StregthenPropertyAdded");
        }
        else
        {
            cardBase.cardAction -= cardBase.Strenghten;
            print("StregthenPropertyRemoved");
        }
    }

    public void AddOrRemoveHalfBlockProperty(bool halfBlock)
    {
        if (halfBlock == true)
        {
            cardBase.cardAction += cardBase.LoseHalfBlock;
            print("LoseHalfBlockPropertyAdded");
        }
        else
        {
            cardBase.cardAction -= cardBase.LoseHalfBlock;
            print("LoseHalfBlockPropertyRemoved");
        }
    }

    public void AddOrRemoveAddCardProperty(bool addCard)
    {
        if (addCard == true)
        {
            cardBase.cardAction += cardBase.AddCardCard;
            print("AddCardPropertyAdded");
        }
        else
        {
            cardBase.cardAction -= cardBase.AddCardCard;
            print("AddCardPropertyRemoved");
        }
    }

    public void AddOrRemoveWeakenProperty(bool weak)
    {
        if (weak == true)
        {
            cardBase.cardAction += cardBase.Weaken;
            print("WeakenPropertyAdded");
        }
        else
        {
            cardBase.cardAction -= cardBase.Weaken;
            print("WeakenPropertyRemoved");
        }
    }

    public void AddOrRemoveExposeProperty(bool exposed)
    {
        if (exposed == true)
        {
            cardBase.cardAction += cardBase.Expose;
            print("ExposePropertyAdded");
        }
        else
        {
            cardBase.cardAction -= cardBase.Expose;
            print("ExposePropertyRemoved");
        }
    }

    void Update()
    {
        if(EditCardMode == true)
        {
            if (cardBase.damageCard != damage)
            {
                AddOrRemoveDamageProperty(cardBase.damageCard);
                damage = cardBase.damageCard;
            }

            if (cardBase.blockCard != defence)
            {
                AddOrRemoveBlockProperty(cardBase.blockCard);
                defence = cardBase.blockCard;
            }

            if (cardBase.strengthenCard != strength)
            {
                AddOrRemoveStrengthenProperty(cardBase.strengthenCard);
                strength = cardBase.strengthenCard;
            }

            if (cardBase.loseHalfBlockCard != loseHalfBlock)
            {
                AddOrRemoveHalfBlockProperty(cardBase.loseHalfBlockCard);
                loseHalfBlock = cardBase.loseHalfBlockCard;
            }

            if(cardBase.addCardCard != addCardCard)
            {
                AddOrRemoveAddCardProperty(cardBase.addCardCard);
                addCardCard = cardBase.addCardCard;
            }

            if (cardBase.weakenCard != weak)
            {
                AddOrRemoveWeakenProperty(cardBase.weakenCard);
                weak = cardBase.weakenCard;
            }

            if (cardBase.exposeCard != exposed)
            {
                AddOrRemoveExposeProperty(cardBase.exposeCard);
                exposed = cardBase.exposeCard;
            }
        }
    }
}
