using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FillTextVariable : MonoBehaviour
{
    public int childNumber;

    void Awake()
    {
        StartCoroutine(AwakeCoroutine());
    }

    IEnumerator AwakeCoroutine()
    {
        if(childNumber == 0)
        {
            yield return new WaitForSeconds(0.1f);
            GetComponent<TextMeshProUGUI>().text = GetComponentInParent<CardBase>().cardName;
        }
        else if (childNumber == 1)
        {
            GetComponent<TextMeshProUGUI>().text = GetComponentInParent<CardBase>().cardDiscription;
        }
        else if (childNumber == 2)
        {
            CombatManager.handParent = this.gameObject;
        }
        else if (childNumber == 3)
        {
            yield return new WaitForSeconds(0.1f);
            GetComponent<TextMeshProUGUI>().text = GetComponentInParent<CardBase>().cost.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
