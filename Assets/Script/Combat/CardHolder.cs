using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardHolder", menuName = "ScriptableObjects/CardHolder", order = 1)]
public class CardHolder : ScriptableObject
{
    public Card card;
    public CardBase cardBase;
    public GameObject cardGameObject;
}
