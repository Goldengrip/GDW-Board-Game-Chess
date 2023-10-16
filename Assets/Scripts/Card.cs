using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private MainGame mainGame;
    private CardController cardController;

    [HideInInspector]
    public int cardID;

    public int team;

    private void Start()
    {
        mainGame = GameObject.Find("GameController").GetComponent<MainGame>();
        cardController = GameObject.Find("CardController").GetComponent<CardController>();
    }

    public void DoubleMove()
    {
        mainGame.DoubleMove();

        RemoveCard();
    }

    public void Phase()
    {
        mainGame.Phase();

        RemoveCard();
    }

    public void Promotion()
    {
        mainGame.PromotionSelection();

        RemoveCard();
    }

    public void WallLoop()
    {
        mainGame.WallLoop();

        RemoveCard();
    }

    public void Recall()
    {
        mainGame.Recall();

        RemoveCard();
    }

    private void RemoveCard()
    {
        if(team == 0)
        {
            cardController.whiteInventory.RemoveAt(cardID);
        }
        else if(team == 1)
        {
            cardController.blackInventory.RemoveAt(cardID);
        }
        
        Destroy(this.gameObject);
    }
}
