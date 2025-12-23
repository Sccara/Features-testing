using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int[] board = new int[9];
    public Button[] cellButtons;
    public TextMeshProUGUI statusText; 
    public Button restartButton;

    int currentPlayer = 1; 

    readonly int[][] winLines = new int[][]
    {
        new int[]{0,1,2},
        new int[]{3,4,5},
        new int[]{6,7,8},
        new int[]{0,3,6},
        new int[]{1,4,7},
        new int[]{2,5,8},
        new int[]{0,4,8},
        new int[]{2,4,6}
    };

    void Start()
    {
        InitBoard();
        restartButton.onClick.AddListener(RestartGame);
        UpdateStatusText();
    }

    void InitBoard()
    {
        for (int i = 0; i < 9; i++)
        {
            board[i] = 0;
            int index = i;

            cellButtons[i].onClick.RemoveAllListeners();
            cellButtons[i].onClick.AddListener(() => OnCellClicked(index));
    
            var txt = cellButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (txt) txt.text = "";
            cellButtons[i].interactable = true;
        }
        currentPlayer = 1;
    }

    public void OnCellClicked(int index)
    {
        if (board[index] != 0) return;
        board[index] = currentPlayer;
        var txt = cellButtons[index].GetComponentInChildren<TextMeshProUGUI>();
        if (txt) txt.text = currentPlayer == 1 ? "X" : "O";
        cellButtons[index].interactable = false;

        int winner = CheckWinner();
        if (winner != 0)
        {
            EndGame(winner);
            return;
        }

        if (IsBoardFull())
        {
            EndGame(0);
            return;
        }

        currentPlayer *= -1;
        UpdateStatusText();
    }

    int CheckWinner()
    {
        foreach (var line in winLines)
        {
            int a = board[line[0]];
            if (a != 0 && a == board[line[1]] && a == board[line[2]])
            {
                return a; 
            }
        }
        return 0;
    }

    bool IsBoardFull()
    {
        for (int i = 0; i < 9; i++) if (board[i] == 0) return false;
        return true;
    }

    void EndGame(int winner)
    {
        if (winner == 1) statusText.text = "X Победили!";
        else if (winner == -1) statusText.text = "O Победили!";
        else statusText.text = "Ничья!";

        for (int i = 0; i < 9; i++) cellButtons[i].interactable = false;
    }

    public void RestartGame()
    {
        InitBoard();
        UpdateStatusText();
    }

    void UpdateStatusText()
    {
        statusText.text = (currentPlayer == 1) ? "Ход X" : "Ход O";
    }
}
