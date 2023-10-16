using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

    private int recallPosX, recallPosY;

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
        if (mainGame.promoting == true)
        {
            if(this.name != "BlackQueen" && this.name != "WhiteQueen" && this.name != "BlackKing" && this.name != "WhiteKing")
            {
                Promote();
                mainGame.promoting = false;
                mainGame.usefulTxt.text = "";
                return;
            }
            else
            {
                mainGame.promoting = false;
                mainGame.usefulTxt.text = "Can't promote this piecce";
            }
        }

        if(mainGame.recalling == true)
        {
            SetRecallPos();
            mainGame.recalling = false;
            mainGame.recallButton.SetActive(true);
            mainGame.recallTurnCountTxt.gameObject.SetActive(true);
            mainGame.usefulTxt.text = "";
            return;
        }

        if (!mainGame.gameOver && mainGame.currentPlayerTurn == team)
        {
            DestroyMover();

            InitiateMover();
        }
    }

    private void SetRecallPos()
    {
        if(team == 0)
        {
            mainGame.whiteRecallPiece = this.gameObject;
        }
        else
        {
            mainGame.blackRecallPiece = this.gameObject;
        }

        recallPosX = posX;
        recallPosY = posY;
    }

    public bool Recall()
    {
        if(mainGame.GetPiecePos(recallPosX, recallPosY) == null)
        {
            mainGame.RemovePiecePos(posX, posY);

            posX = recallPosX;
            posY = recallPosY;
            SetPosition();

            mainGame.SetPos(this.gameObject);

            return true;
        }
        else
        {
            return false;
        }
    }

    private void Promote()
    {
        //mainGame.ShowPromotionScreen(this.name, posX, posY);

        if (this.name == "BlackPawn" || this.name == "WhitePawn")
        {
            mainGame.promotionPiece = this.gameObject;

            mainGame.background.gameObject.SetActive(true);
        }
        else if (this.name == "BlackKnight" || this.name == "WhiteKnight" || this.name == "BlackBishop" || this.name == "WhiteBishop")
        {
            RookPromotion();
        }
        else if (this.name == "BlackRook" || this.name == "WhiteRook")
        {
            QueenPromotion();
        }
    }

    public void KnightPromotion()
    {
        mainGame.background.gameObject.SetActive(false);
        if (team == 0)
        {
            this.name = "WhiteKnight";
        }
        else if (team == 1)
        {
            this.name = "BlackKnight";
        }

        GetSprite();
    }

    public void BishopPromotion()
    {
        mainGame.background.gameObject.SetActive(false);
        if (team == 0)
        {
            this.name = "WhiteBishop";
        }
        else if (team == 1)
        {
            this.name = "BlackBishop";
        }

        GetSprite();
    }

    private void RookPromotion()
    {
        if (team == 0)
        {
            this.name = "WhiteRook";
        }
        else if (team == 1)
        {
            this.name = "BlackRook";
        }

        GetSprite();
    }

    private void QueenPromotion()
    {
        if (team == 0)
        {
            this.name = "WhiteQueen";
        }
        else if (team == 1)
        {
            this.name = "BlackQueen";
        }

        GetSprite();
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

        if (mainGame.phase)
        {
            while (mainGame.IsPositionOnBoard(x, y))
            {
                if(mainGame.GetPiecePos(x, y) == null)
                {
                    MoverSpawn(x, y);
                }
                else if(mainGame.GetPiecePos(x, y).GetComponent<Piece>().team != team)
                {
                    break;
                }
                x += xNum;
                y += yNum;
            }
            
        }
        else if (mainGame.wallLoop)
        {
            while (mainGame.IsPositionOnBoard(x, y) && mainGame.GetPiecePos(x, y) == null)
            {
                MoverSpawn(x, y);
                x += xNum;
                y += yNum;
            }
            if(!mainGame.IsPositionOnBoard(x, 1))
            {
                if(x < 0)
                {
                    x = mainGame.allPositions.GetLength(0) - 1;
                }
                else
                {
                    x = 0;
                }
                while (mainGame.IsPositionOnBoard(x, y) && mainGame.GetPiecePos(x, y) == null)
                {
                    MoverSpawn(x, y);
                    x += xNum;
                    y += yNum;
                }
            }
        }
        else
        {
            while (mainGame.IsPositionOnBoard(x, y) && mainGame.GetPiecePos(x, y) == null)
            {
                MoverSpawn(x, y);
                x += xNum;
                y += yNum;
            }
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
