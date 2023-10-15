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

    private List<GameObject> whiteInventory = new List<GameObject>(), blackInventory = new List<GameObject>(), activeCards = new List<GameObject>();

    private int currentPlayerTurn;

    private int whiteTurnCounter, blackTurnCounter;

    public void ChangeTurn(int turn)
    {
        currentPlayerTurn = turn;

        HideAllCards();

        if(currentPlayerTurn == 0)
        {
            whiteTurnCounter++;

            if(whiteTurnCounter == numTurnsToGiveCard)
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

            if (blackTurnCounter == numTurnsToGiveCard)
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
        foreach(var card in whiteInventory)
        {
            GameObject cardObject = Instantiate(card);
            cardObject.transform.SetParent(cardInventoryParent.transform);

            activeCards.Add(cardObject);
        }
    }

    private void ShowBlackCards()
    {
        foreach (var card in blackInventory)
        {
            GameObject cardObject = Instantiate(card);
            cardObject.transform.SetParent(cardInventoryParent.transform);

            activeCards.Add(cardObject);
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
