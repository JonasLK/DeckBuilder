using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public float timeBetweenActions;

    public bool playerTurn;

    public float startDelay;
    public float enemyTurnTime;

    private bool startCombat;
    private int counter;
    [HideInInspector] public List<Card> cardHolderList;

    public CombatStateChain currentAction;

    public enum CombatStateChain
    {
        AwaitingInput,
        startCombat,
        enemyIntent,
        playerAction,
        endPlayerTurn,
        startEnemyTurn,
        enemyAction,
        startTurn,
        playerVictory,
        playerDefeat
    }

    public void StartCombatChain()
    {
        currentAction = CombatStateChain.startCombat;
    }

    void Update()
    {
        switch (currentAction)
        {
            case CombatStateChain.AwaitingInput:
                print("case CombatStateChain.AwaitingInput:");
                break;

            case CombatStateChain.startCombat:
                print("case CombatStateChain.startCombat:");
                StartCombat();
                break;

            case CombatStateChain.enemyIntent:
                foreach(Enemy enemy in CombatManager.enemies)
                {
                    enemy.EnemyIntent();
                }
                currentAction = CombatStateChain.playerAction;
                print("case CombatStateChain.enemyIntent:");
                break;

            case CombatStateChain.playerAction:
                playerTurn = true;
                print("case CombatStateChain.playerAction:");
                break;
            case CombatStateChain.endPlayerTurn:
                int cardsInHand = CombatManager.pileAndHandManager.hand.Count;
                for (int i = 0; i < cardsInHand; i++)
                {
                    CombatManager.pileAndHandManager.discardPile.Add(CombatManager.pileAndHandManager.hand[0]);
                    CombatManager.pileAndHandManager.RemoveCardFromHand(CombatManager.pileAndHandManager.hand[0], CombatManager.pileAndHandManager.gameObjectHand[0]);
                    CombatManager.pileAndHandManager.UpdateDiscardCountText();
                }
                currentAction = CombatStateChain.startEnemyTurn;
                break;

            case CombatStateChain.startEnemyTurn:
                foreach (Enemy enemy in CombatManager.enemies)
                {
                    enemy.StartEnemyTurn();
                }
                currentAction = CombatStateChain.enemyAction;
                break;

            case CombatStateChain.enemyAction:
                foreach (Enemy enemy in CombatManager.enemies)
                {
                    enemy.EnemyAction();
                }
                currentAction = CombatStateChain.startTurn;
                print("case CombatStateChain.enemyAction:");
                break;

            case CombatStateChain.startTurn:
                StartTurn();
                currentAction = CombatStateChain.playerAction;
                print("case CombatStateChain.startTurn:");
                break;

            case CombatStateChain.playerVictory:
                //reward screen ???
                print("case CombatStateChain.playerVictory:");
                break;

            case CombatStateChain.playerDefeat:
                //screen with buttons to main menu, replay and quit ???
                print("case CombatStateChain.playerDefeat:");
                break;

            default:
                Debug.LogError("stateChanger reached default state");
                break;
        }
    }
    public void StartCombat()
    {
        LoadDeck();
        for (int i = 0; i < CombatManager.player.cardsDrawnStartCombat; i++)
        {
            CombatManager.pileAndHandManager.AddCardToHand(CombatManager.pileAndHandManager.deckPile[0]);
        }
        currentAction = CombatStateChain.enemyIntent;
    }

    public void LoadDeck()
    {
        foreach(Card card in CombatManager.deckManager.deck)
        {
            cardHolderList.Add(card);
        }

        for (int i = 0; i < CombatManager.deckManager.deck.Count; i++)
        {
            Card temp = cardHolderList[Random.Range(0, cardHolderList.Count)];
            CombatManager.pileAndHandManager.deckPile.Add(temp);
            cardHolderList.Remove(temp);
        }
    }

    public void StartTurn()
    {
        if(CombatManager.player.blockAmount != 0)
        {
            if(CombatManager.player.loseHalfBlock == true)
            {
                CombatManager.player.blockAmount = Mathf.RoundToInt(CombatManager.player.blockAmount / 2);
            }
            else
            {
                CombatManager.player.blockAmount = 0;
            }
        }
        CombatManager.player.UpdateHealthDisplay();

        CombatManager.player.energy = CombatManager.player.startEnergy;
        CombatManager.player.UpdateEnergyDisplay();

        CombatManager.pileAndHandManager.DrawCardFromDeckPile(CombatManager.player.cardsDrawnStartTurn);

        if(CombatManager.player.weak > 0)
        {
            CombatManager.player.weak -= 1;
            CombatManager.player.UpdateWeakDisplay();
        }

        if(CombatManager.player.exposed > 0)
        {
            CombatManager.player.exposed -= 1;
            CombatManager.player.UpdateExposedDisplay();
        }
    }

    public void EndTurn()
    {
        playerTurn = false;
        currentAction = CombatStateChain.endPlayerTurn;
    }
}