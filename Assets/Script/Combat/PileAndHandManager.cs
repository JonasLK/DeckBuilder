using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileAndHandManager : MonoBehaviour
{
    public List<Card> deckPile;
    public List<Card> hand;
    [HideInInspector]public List<GameObject> gameObjectHand;
    public List<Card> discardPile;
    public List<Card> exilePile;

    public int maxHandSize;
    private int counter;
    private Card firstDeckCard;
    private List<Card> cardHolderList;
    void Awake()
    {
        CombatManager.delayedStart += UpdateMaxHandSize;
    }
    
    void Update()
    {
        
    }

    public void AddCardToHand(Card cardToAdd)
    {
        hand.Add(cardToAdd);
        GameObject temp = Instantiate(cardToAdd.cardObject, CombatManager.handParent.transform);
        temp.GetComponent<CardBase>().card = cardToAdd;
        gameObjectHand.Add(temp);
        cardToAdd.CardBaseAwake(temp.GetComponent<CardBase>());
        deckPile.Remove(cardToAdd);
    }

    public void RemoveCardFromHand(Card cardToAdd, GameObject self)
    {
        hand.Remove(cardToAdd);
        gameObjectHand.Remove(self);
        Destroy(self);
    }

    public void UpdateMaxHandSize()
    {
        maxHandSize = CombatManager.player.maxHandSize;
    }

    public void DrawCardFromDeckPile(int cardAmount)
    {
        if(deckPile.Count <= 0 && discardPile.Count <= 0)
        {
            return;
        }

        for (int i = 0; i < cardAmount; i++)
        {
            firstDeckCard = deckPile[0];
            if(firstDeckCard != null)
            {
                print("drawing Card");
                if(hand.Count < maxHandSize)
                {
                    AddCardToHand(firstDeckCard);
                }
                else
                {
                    exilePile.Add(firstDeckCard);
                    deckPile.Remove(firstDeckCard);
                }
            }
            else
            {
                print("shuffeling deck");
                ShuffleDiscardPileIntoDeck();
                firstDeckCard = deckPile[0];
                AddCardToHand(firstDeckCard);
            }
        }
    }

    public void PlayCard(bool exile, Card playedCard, GameObject self)
    {
        if(exile == true)
        {
            exilePile.Add(playedCard);
        }
        else
        {
            discardPile.Add(playedCard);
        }
        RemoveCardFromHand(playedCard, self);
    }

    public void ShuffleDiscardPileIntoDeck()
    {
        foreach (Card card in discardPile)
        {
            cardHolderList.Add(card);
        }

        for (int i = 0; i < discardPile.Count; i++)
        {
            Card temp = cardHolderList[Random.Range(0, cardHolderList.Count)];
            deckPile.Add(temp);
            cardHolderList.Remove(temp);
        }
    }
}
