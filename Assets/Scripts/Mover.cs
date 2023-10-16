using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Mover : MonoBehaviour
{
    public MainGame mainGame;

    public GameObject reference = null;

    public int moverPosX;
    public int moverPosY;

    //Check if the piece is attacking another piece or just moving around
    public bool attacking = false;

    private void Start()
    {
        if (attacking)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    public void OnMouseUp()
    {
        mainGame = GameObject.Find("GameController").GetComponent<MainGame>();

        if(attacking )
        {
            GameObject piece = mainGame.GetPiecePos(moverPosX, moverPosY);

            if(piece.name == "WhiteKing")
            {
                mainGame.Winner(1);
            }
            if (piece.name == "BlackKing")
            {
                mainGame.Winner(0);
            }

            Destroy( piece );
        }

        Piece pieceScript = reference.GetComponent<Piece>();

        mainGame.RemovePiecePos(pieceScript.posX, pieceScript.posY);

        pieceScript.posX = moverPosX;
        pieceScript.posY = moverPosY;
        pieceScript.SetPosition();
        pieceScript.pawnFirstTurn = false;

        mainGame.SetPos(reference);

        mainGame.ChangeTurn();

        pieceScript.DestroyMover();
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }
}
