using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalVariables;

public class GamePlayWindow : MonoBehaviour
{
    [SerializeField] GameObject x;
    [SerializeField] GameObject o;

    [SerializeField] GameObject myTurnText;
    [SerializeField] GameObject myTurnIcon;
    [SerializeField] GameObject yourTurnText;
    [SerializeField] GameObject tryAgainText;
    [SerializeField] GameObject gameInstructionsText;
    [SerializeField] GameObject youWonText;
    [SerializeField] GameObject iWonText;

    [SerializeField] GameObject playAgainButton;

    [SerializeField] List<Cell> cells = new List<Cell>();

    [SerializeField] Transform allShapes;

    List<Cell> vertical1 = new List<Cell>();
    List<Cell> vertical2 = new List<Cell>();
    List<Cell> vertical3 = new List<Cell>();

    List<Cell> horizontal1 = new List<Cell>();
    List<Cell> horizontal2 = new List<Cell>();
    List<Cell> horizontal3 = new List<Cell>();

    List<Cell> diagonal1 = new List<Cell>();
    List<Cell> diagonal2 = new List<Cell>();

    public bool yourTurn = true;

    public bool gameOver = false;

    void Start()
    {
        FillAligners();

        SetYourTurnText(true);
        gameInstructionsText.SetActive(true);
        tryAgainText.SetActive(false);
    }

    public void ClickPlayAgainButton()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].free = true;
            cells[i].shape = Shapes.none;
        }

        for (int i = 0; i < allShapes.childCount; i++)
        {
            Destroy(allShapes.GetChild(i).gameObject);
        }

        gameInstructionsText.SetActive(true);
        tryAgainText.SetActive(false);
        yourTurn = true;
        gameOver = false;

        myTurnText.SetActive(false);
        yourTurnText.SetActive(true);
        playAgainButton.SetActive(false);
        iWonText.SetActive(false);
        youWonText.SetActive(false);
    }

    public void ClickButton(int index)
    {
        if (!gameOver && yourTurn && cells[index].free)
        {
            cells[index].free = false;
            cells[index].shape = Shapes.x;
            GameObject shape = Instantiate(x, cells[index].transform.position, Quaternion.identity);
            shape.transform.SetParent(allShapes);
            yourTurn = false;
            CheckAligns();

            if (!gameOver)
            {
                StartCoroutine(MakeBotTurn());
            }
        }
    }

    void SetYourTurnText(bool status)
    {
        if (status)
        {
            myTurnText.SetActive(false);
            yourTurnText.SetActive(true);
        } else
        {
            myTurnText.SetActive(true);
            yourTurnText.SetActive(false);
        }
    }

    IEnumerator MakeBotTurn()
    {
        myTurnText.SetActive(true);
        yourTurnText.SetActive(false);

        yield return new WaitForSeconds(1);

        List<Cell> freeCells = new List<Cell>();

        for (int i = 0; i < cells.Count; i++)
        {
            if (cells[i].free)
            {
                freeCells.Add(cells[i]);
            }
        }

        if (freeCells.Count > 0)
        {
            int randomIndex = Random.Range(0, freeCells.Count);
            freeCells[randomIndex].free = false;
            freeCells[randomIndex].shape = Shapes.o;
            GameObject shape = Instantiate(o, freeCells[randomIndex].transform.position, Quaternion.identity);
            shape.transform.SetParent(allShapes);
            yourTurn = true;

            myTurnText.SetActive(false);
            yourTurnText.SetActive(true);

            CheckAligns();
        } else
        {
            gameInstructionsText.SetActive(false);
            tryAgainText.SetActive(true);

            StartCoroutine(ResetGame());
        }
    }

    IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(1);

        ClickPlayAgainButton();
    }

    void CheckAligns()
    {
        CheckVerticalAlign();
        CheckHorizontalAlign();
        CheckDiagonalAlign();
    }

    void CheckVerticalAlign()
    {
        int xShapes = 0;
        int oShapes = 0;

        for (int i = 0; i < vertical1.Count; i++)
        {
            if (vertical1[i].shape == Shapes.x)
            {
                xShapes++;
            } else if (vertical1[i].shape == Shapes.o)
            {
                oShapes++;
            }
        }
        if (xShapes == 3)
        {
            YouWon();
        } else if (oShapes == 3)
        {
            IWon();
        }
        xShapes = 0;
        oShapes = 0;

        for (int i = 0; i < vertical2.Count; i++)
        {
            if (vertical2[i].shape == Shapes.x)
            {
                xShapes++;
            }
            else if (vertical2[i].shape == Shapes.o)
            {
                oShapes++;
            }
        }
        if (xShapes == 3)
        {
            YouWon();
        }
        else if (oShapes == 3)
        {
            IWon();
        }
        xShapes = 0;
        oShapes = 0;

        for (int i = 0; i < vertical3.Count; i++)
        {
            if (vertical3[i].shape == Shapes.x)
            {
                xShapes++;
            }
            else if (vertical3[i].shape == Shapes.o)
            {
                oShapes++;
            }
        }
        if (xShapes == 3)
        {
            YouWon();
        }
        else if (oShapes == 3)
        {
            IWon();
        }
    }

    void CheckHorizontalAlign()
    {
        int xShapes = 0;
        int oShapes = 0;

        for (int i = 0; i < horizontal1.Count; i++)
        {
            if (horizontal1[i].shape == Shapes.x)
            {
                xShapes++;
            }
            else if (horizontal1[i].shape == Shapes.o)
            {
                oShapes++;
            }
        }
        if (xShapes == 3)
        {
            YouWon();
        }
        else if (oShapes == 3)
        {
            IWon();
        }
        xShapes = 0;
        oShapes = 0;

        for (int i = 0; i < horizontal2.Count; i++)
        {
            if (horizontal2[i].shape == Shapes.x)
            {
                xShapes++;
            }
            else if (horizontal2[i].shape == Shapes.o)
            {
                oShapes++;
            }
        }
        if (xShapes == 3)
        {
            YouWon();
        }
        else if (oShapes == 3)
        {
            IWon();
        }
        xShapes = 0;
        oShapes = 0;

        for (int i = 0; i < horizontal3.Count; i++)
        {
            if (horizontal3[i].shape == Shapes.x)
            {
                xShapes++;
            }
            else if (horizontal3[i].shape == Shapes.o)
            {
                oShapes++;
            }
        }
        if (xShapes == 3)
        {
            YouWon();
        }
        else if (oShapes == 3)
        {
            IWon();
        }
    }

    void CheckDiagonalAlign()
    {
        int xShapes = 0;
        int oShapes = 0;

        for (int i = 0; i < diagonal1.Count; i++)
        {
            if (diagonal1[i].shape == Shapes.x)
            {
                xShapes++;
            }
            else if (diagonal1[i].shape == Shapes.o)
            {
                oShapes++;
            }
        }
        if (xShapes == 3)
        {
            YouWon();
        }
        else if (oShapes == 3)
        {
            IWon();
        }
        xShapes = 0;
        oShapes = 0;

        for (int i = 0; i < diagonal2.Count; i++)
        {
            if (diagonal2[i].shape == Shapes.x)
            {
                xShapes++;
            }
            else if (diagonal2[i].shape == Shapes.o)
            {
                oShapes++;
            }
        }
        if (xShapes == 3)
        {
            YouWon();
        }
        else if (oShapes == 3)
        {
            IWon();
        }
    }

    void YouWon()
    {
        Debug.Log("You Won");
        gameInstructionsText.SetActive(false);
        youWonText.SetActive(true);
        gameOver = true;
        playAgainButton.SetActive(true);
    }

    void IWon()
    {
        Debug.Log("I Won");
        gameInstructionsText.SetActive(false);
        iWonText.SetActive(true);
        gameOver = true;
        playAgainButton.SetActive(true);
    }

    void FillAligners()
    {
        vertical1.Add(cells[0]);
        vertical1.Add(cells[1]);
        vertical1.Add(cells[2]);
        vertical2.Add(cells[3]);
        vertical2.Add(cells[4]);
        vertical2.Add(cells[5]);
        vertical3.Add(cells[6]);
        vertical3.Add(cells[7]);
        vertical3.Add(cells[8]);

        horizontal1.Add(cells[0]);
        horizontal1.Add(cells[3]);
        horizontal1.Add(cells[6]);
        horizontal2.Add(cells[1]);
        horizontal2.Add(cells[4]);
        horizontal2.Add(cells[7]);
        horizontal3.Add(cells[2]);
        horizontal3.Add(cells[5]);
        horizontal3.Add(cells[8]);

        diagonal1.Add(cells[0]);
        diagonal1.Add(cells[4]);
        diagonal1.Add(cells[8]);
        diagonal2.Add(cells[2]);
        diagonal2.Add(cells[4]);
        diagonal2.Add(cells[6]);
    }
}
