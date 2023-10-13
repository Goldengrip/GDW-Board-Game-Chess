using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Piece : MonoBehaviour
{
    private MainGame mainGame;
    public GameObject mover;

    private int team;// 0 is white, 1 is black

    public Sprite whitePawnSprite, whiteRookSprite, whiteKnightSprite,
        whiteBishopSprite, whiteKingSprite, whiteQueenSprite, 
        blackPawnSprite, blackRookSprite, blackKnightSprite, 
        blackBishopSprite, blackKingSprite, blackQueenSprite;

    [HideInInspector]
    public int posX, posY;

    private void Start()
    {
        mainGame = GameObject.Find("GameController").GetComponent<MainGame>();
    }

    // Give each peice the correct sprite based off name
    public void GetSprite()
    {
        switch (this.name)
        {
            //White Peices
            case "WhitePawn": GetComponent<SpriteRenderer>().sprite = whitePawnSprite; team = 0; break;
            case "WhiteRook": GetComponent<SpriteRenderer>().sprite = whiteRookSprite; team = 0; break;
            case "WhiteKnight": GetComponent<SpriteRenderer>().sprite = whiteKnightSprite; team = 0; break;
            case "WhiteBishop": GetComponent<SpriteRenderer>().sprite = whiteBishopSprite; team = 0; break;
            case "WhiteKing": GetComponent<SpriteRenderer>().sprite = whiteKingSprite; team = 0; break;
            case "WhiteQueen": GetComponent<SpriteRenderer>().sprite = whiteQueenSprite; team = 0; break;
            //Black Peices
            case "BlackPawn": GetComponent<SpriteRenderer>().sprite = blackPawnSprite; team = 1; break;
            case "BlackRook": GetComponent<SpriteRenderer>().sprite = blackRookSprite; team = 1; break;
            case "BlackKnight": GetComponent<SpriteRenderer>().sprite = blackKnightSprite; team = 1; break;
            case "BlackBishop": GetComponent<SpriteRenderer>().sprite = blackBishopSprite; team = 1; break;
            case "BlackKing": GetComponent<SpriteRenderer>().sprite = blackKingSprite; team = 1; break;
            case "BlackQueen": GetComponent<SpriteRenderer>().sprite = blackQueenSprite; team = 1; break;
        }
    }

    public void SetPosition()
    {
        transform.position = new Vector3(posX, posY, 0);
    }

    private void OnMouseUp()
    {
        if (!mainGame.gameOver && mainGame.currentPlayerTurn == team)
        {
            DestroyMover();

            InitiateMover();
        }
    }
    
    public void DestroyMover()
    {
        GameObject[] movers = GameObject.FindGameObjectsWithTag("Mover");

        for (int i = 0; i < movers.Length; i++)
        {
            Destroy(movers[i]);
        }
    }

    public void InitiateMover()
    {
        switch (this.name)
        {
            case "BlackQueen":
            case "WhiteQueen":
                LineMover(1, 0);
                LineMover(0, 1);
                LineMover(1, 1);
                LineMover(-1, 0);
                LineMover(0, -1);
                LineMover(-1, -1);
                LineMover(-1, 1);
                LineMover(1, -1);
                break;
            case "BlackKnight":
            case "WhiteKnight":
                LMover();
                break;
            case "BlackBishop":
            case "WhiteBishop":
                LineMover(1, 1);
                LineMover(1, -1);
                LineMover(-1, 1);
                LineMover(-1, -1);
                break;
            case "BlackKing":
            case "WhiteKing":
                SurroundMover();
                break;
            case "BlackRook":
            case "WhiteRook":
                LineMover(1, 0);
                LineMover(0, 1);
                LineMover(-1, 0);
                LineMover(0, -1);
                break;
            case "BlackPawn":
                PawnMover(posX, posY -1);
                break;
            case "WhitePawn":
                PawnMover(posX, posY +1);
                break;
        }
    }

    public void LineMover(int xNum, int yNum)
    {
        int x = posX + xNum;
        int y = posY + yNum;

        while (mainGame.IsPositionOnBoard(x, y) && mainGame.GetPiecePos(x,y) == null)
        {
            MoverSpawn(x,y);
            x += xNum;
            y += yNum;
        }

        if(mainGame.IsPositionOnBoard(x,y) && mainGame.GetPiecePos(x,y).GetComponent<Piece>().team != team)
        {
            MoverAttackSpawn(x,y);
        }
    }

    public void LMover()
    {
        PointMover(posX + 1, posY + 2);
        PointMover(posX - 1, posY + 2);
        PointMover(posX + 1, posY - 2);
        PointMover(posX - 1, posY - 2);
        PointMover(posX + 2, posY + 1);
        PointMover(posX - 2, posY + 1);
        PointMover(posX + 2, posY - 1);
        PointMover(posX - 2, posY - 1);
    }

    public void SurroundMover()
    {
        PointMover(posX, posY + 1);
        PointMover(posX, posY - 1);
        PointMover(posX + 1, posY);
        PointMover(posX - 1, posY);
        PointMover(posX + 1, posY + 1);
        PointMover(posX + 1, posY - 1);
        PointMover(posX - 1, posY + 1);
        PointMover(posX - 1, posY - 1);
    }

    public void PointMover(int x, int y)
    {
        if(mainGame.IsPositionOnBoard(x, y))
        {
            GameObject piece = mainGame.GetPiecePos(x, y);

            if(piece == null)
            {
                MoverSpawn(x, y);
            }
            else if(piece.GetComponent<Piece>().team != team)
            {
                MoverAttackSpawn(x, y);
            }
        }
    }

    public void PawnMover(int x, int y)
    {
        if (mainGame.IsPositionOnBoard(x, y))
        {
            if(mainGame.GetPiecePos(x,y) == null)
            {
                MoverSpawn(x,y);
            }

            if(mainGame.IsPositionOnBoard(x + 1, y) && mainGame.GetPiecePos(x + 1, y) != null 
                && mainGame.GetPiecePos(x + 1, y).GetComponent<Piece>().team != team)
            {
                MoverAttackSpawn(x + 1, y);
            }

            if (mainGame.IsPositionOnBoard(x - 1, y) && mainGame.GetPiecePos(x - 1, y) != null
                && mainGame.GetPiecePos(x - 1, y).GetComponent<Piece>().team != team)
            {
                MoverAttackSpawn(x - 1, y);
            }
        }
    }

    public void MoverSpawn(int x, int y)
    {
        GameObject m = Instantiate(mover, new Vector3(x, y, 0), Quaternion.identity);

        Mover moverScript = m.GetComponent<Mover>();
        moverScript.SetReference(gameObject);
        moverScript.moverPosX = x;
        moverScript.moverPosY = y;
    }
    public void MoverAttackSpawn(int x, int y)
    {
        GameObject m = Instantiate(mover, new Vector3(x, y, 0), Quaternion.identity);

        Mover moverScript = m.GetComponent<Mover>();
        moverScript.attacking = true;
        moverScript.SetReference(gameObject);
        moverScript.moverPosX = x;
        moverScript.moverPosY = y;
    }
}
