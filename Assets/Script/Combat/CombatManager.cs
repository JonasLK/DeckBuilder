using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{

    public static DeckManager deckManager;
    public static TurnManager turnManager;
    public static Player player;
    public static PileAndHandManager pileAndHandManager;
    public static GameObject handParent;
    public static Camera camera;

    public static List<Enemy> enemies;

    public  delegate void DelayStart();
    public static DelayStart delayedStart;
    public static DelayStart delayStart;

    public bool targeting;

    public void Awake()
    {
        enemies = new List<Enemy>();
        deckManager = GameObject.FindGameObjectWithTag("DeckManager").GetComponent<DeckManager>();
        turnManager = GetComponent<TurnManager>();
        player = GetComponent<Player>();
        pileAndHandManager = GetComponent<PileAndHandManager>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        StartCoroutine(DelayedStart());
    }

    public static IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(0.3f);
        delayStart();
        delayedStart();
        foreach(Enemy enemy in enemies)
        {
            print(enemy);
        }
        turnManager.StartCombatChain();
    }

    public static void CheckForPlayerVictory()
    {
        if(enemies.Count <= 0)
        {
            PlayerVictory();
        }
    }

    public static void PlayerVictory()
    {
        print("PlayerVictory");
    }

    //state machine here
}