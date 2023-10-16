using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CardController : MonoBehaviour
{
    public int maxCards = 3;

    public int numTurnsToGiveCard = 2;

    public GameObject cardInventoryParent;

    public GameObject[] cardsPrefabs;

    [HideInInspector]
    public List<GameObject> whiteInventory = new List<GameObject>(), blackInventory = new List<GameObject>(), activeCards = new List<GameObject>();

    private int currentPlayerTurn;

    private int whiteTurnCounter, blackTurnCounter;

    public void ChangeTurn(int turn)
    {
        currentPlayerTurn = turn;

        HideAllCards();

        if(currentPlayerTurn == 0)
        {
            whiteTurnCounter++;

            if(whiteTurnCounter >= numTurnsToGiveCard && whiteInventory.Count < maxCards)
            {
                int x = UnityEngine.Random.Range(0, cardsPrefabs.Length);
                whiteInventory.Add(cardsPrefabs[x]);

                whiteTurnCounter = 0;
            }

            ShowWhiteCards();
        }
        else if(currentPlayerTurn == 1)
        {
            blackTurnCounter++;

            if (blackTurnCounter >= numTurnsToGiveCard && blackInventory.Count <= maxCards)
            {
                int x = UnityEngine.Random.Range(0, cardsPrefabs.Length);
                blackInventory.Add(cardsPrefabs[x]);

                blackTurnCounter = 0;
            }

            ShowBlackCards();
        }
    }

    private void ShowWhiteCards()
    {
        int i = 0;
        foreach(var card in whiteInventory)
        {
            GameObject cardObject = Instantiate(card);
            cardObject.transform.SetParent(cardInventoryParent.transform);

            cardObject.GetComponent<Card>().cardID = i;
            cardObject.GetComponent<Card>().team = 0;

            activeCards.Add(cardObject);
            i++;
        }
    }

    private void ShowBlackCards()
    {
        int i = 0;
        foreach (var card in blackInventory)
        {
            GameObject cardObject = Instantiate(card);
            cardObject.transform.SetParent(cardInventoryParent.transform);

            cardObject.GetComponent<Card>().cardID = i;
            cardObject.GetComponent<Card>().team = 1;

            activeCards.Add(cardObject);
            i++;
        }
    }

    private void HideAllCards()
    {
        foreach(var card in activeCards)
        {
            Destroy(card);
        }
    }
}
