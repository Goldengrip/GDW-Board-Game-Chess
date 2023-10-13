using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    public GameObject chessPiecePrefab;

    public TMP_Text winnerTxt;
    public TMP_Text restartTxt;
    public Button button;

    private GameObject[,] allPositions = new GameObject[8, 8]; // All posible positions on the board

    private GameObject[] whitePieces = new GameObject[16]; // The 16 pieces for white
    private GameObject[] blackPieces = new GameObject[16]; // The 16 pieces for black

    public int currentPlayerTurn; // 0 is white, 1 is black
    public bool gameOver;


    private void Start()
    {
        whitePieces = new GameObject[] { InstantiatePiece("WhitePawn", 0, 1), InstantiatePiece("WhitePawn", 1, 1), 
            InstantiatePiece("WhitePawn", 2, 1), InstantiatePiece("WhitePawn", 3, 1), 
            InstantiatePiece("WhitePawn", 4, 1), InstantiatePiece("WhitePawn", 5, 1), 
            InstantiatePiece("WhitePawn", 6, 1), InstantiatePiece("WhitePawn", 7, 1), 
            InstantiatePiece("WhiteRook", 0, 0), InstantiatePiece("WhiteKnight", 1, 0),
            InstantiatePiece("WhiteBishop", 2, 0), InstantiatePiece("WhiteQueen", 3, 0), 
            InstantiatePiece("WhiteKing", 4, 0), InstantiatePiece("WhiteBishop", 5, 0),
            InstantiatePiece("WhiteKnight", 6, 0), InstantiatePiece("WhiteRook", 7, 0),};
        blackPieces = new GameObject[] { InstantiatePiece("BlackPawn", 0, 6), InstantiatePiece("BlackPawn", 1, 6),
            InstantiatePiece("BlackPawn", 2, 6), InstantiatePiece("BlackPawn", 3, 6),
            InstantiatePiece("BlackPawn", 4, 6), InstantiatePiece("BlackPawn", 5, 6),
            InstantiatePiece("BlackPawn", 6, 6), InstantiatePiece("BlackPawn", 7, 6),
            InstantiatePiece("BlackRook", 0, 7), InstantiatePiece("BlackKnight", 1, 7),
            InstantiatePiece("BlackBishop", 2, 7), InstantiatePiece("BlackQueen", 3, 7),
            InstantiatePiece("BlackKing", 4, 7), InstantiatePiece("BlackBishop", 5, 7),
            InstantiatePiece("BlackKnight", 6, 7), InstantiatePiece("BlackRook", 7, 7),};


        SetPositions(whitePieces);
        SetPositions(blackPieces);
    }

    private void SetPositions(GameObject[] pieces)
    {
        foreach (var piece in pieces)
        {
            SetPos(piece);
        }
    }

    public void SetPos(GameObject piece)
    {
        Piece pieceScript = piece.GetComponent<Piece>();

        allPositions[pieceScript.posX, pieceScript.posY] = piece;
    }

    private GameObject InstantiatePiece(string name, int x, int y)
    {
        GameObject pieceObj = Instantiate(chessPiecePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Piece pieceScript = pieceObj.GetComponent<Piece>();
        pieceScript.name = name;
        pieceScript.GetSprite();
        pieceScript.posX = x;
        pieceScript.posY = y;
        pieceScript.SetPosition();
        return pieceObj;
    }

    public void RemovePiecePos(int x, int y)
    {
        allPositions[x, y] = null;
    }

    public GameObject GetPiecePos(int x, int y)
    {
        return allPositions[x, y];
    }

    public bool IsPositionOnBoard(int x, int y)
    {
        if(x < 0 || y < 0 || x >= allPositions.GetLength(0) || y >= allPositions.GetLength(1))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void ChangeTurn()
    {
        if(currentPlayerTurn == 0)
        {
            currentPlayerTurn = 1;
        }
        else
        {
            currentPlayerTurn = 0;
        }
    }

    public void RestartGame()
    {
        gameOver = false;

        // This can change to the main menu instead when it's done
        SceneManager.LoadScene("MainGame");
    }

    public void Winner(int teamWinner)
    {
        gameOver = true;

        winnerTxt.enabled = true;
        if(teamWinner == 0)
        {
            winnerTxt.text = "White wins!";
        }
        else
        {
            winnerTxt.text = "Black wins!";
        }

        restartTxt.enabled = true;
        button.enabled = true;
    }
}
