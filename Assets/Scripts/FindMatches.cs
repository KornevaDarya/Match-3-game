using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FindMatches : MonoBehaviour
{
    private Board board;
    public List<GameObject> currentMatches = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
    }

    public void FindAllMatches()
    {
        StartCoroutine(FindAllMatchesCo());
    }
    private void AddToListAndMatch(GameObject dot)
    {
        if (!currentMatches.Contains(dot))
        {
            currentMatches.Add(dot);
        }
        dot.GetComponent<Dot>().isMatched = true;
    }

    private void GetNearbyPieces(GameObject dot1, GameObject dot2, GameObject dot3)
    {
        AddToListAndMatch(dot1);
        AddToListAndMatch(dot2);
        AddToListAndMatch(dot3);
    }

    private IEnumerator FindAllMatchesCo()
    {
        yield return new WaitForSeconds(.2f);
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.width; j++)
            {
                GameObject currentDot = board.allDots[i, j];
                
                if (currentDot != null)
                {
                    Dot currentDotDot = currentDot.GetComponent<Dot>();
                    if (i > 0 && i < board.width - 1)
                    {
                        GameObject leftDot = board.allDots[i - 1, j];
                        GameObject rightDot = board.allDots[i + 1, j];
                        if (leftDot != null && rightDot != null)
                        {
                            Dot rightDotDot = rightDot.GetComponent<Dot>();
                            Dot leftDotDot = leftDot.GetComponent<Dot>();
                            if (leftDot != null && rightDot != null)
                            {
                                if (leftDot.tag == currentDot.tag && rightDot.tag == currentDot.tag)
                                {
                                    GetNearbyPieces(leftDot, currentDot, rightDot);
                                }
                            }
                        }
                    }
                    if (j > 0 && j < board.width - 1)
                    {
                        GameObject upDot = board.allDots[i, j + 1];
                        GameObject downDot = board.allDots[i, j - 1];
                        if (upDot != null && downDot != null)
                        {
                            Dot upDotDot = upDot.GetComponent<Dot>();
                            Dot downDotDot = downDot.GetComponent<Dot>();
                            if (upDot != null && downDot != null)
                            {
                                if (upDot.tag == currentDot.tag && downDot.tag == currentDot.tag)
                                {
                                    GetNearbyPieces(upDot, currentDot, downDot);
                                }
                            }
                        }
                    }

                }
            }
        }
    }

    List<GameObject> GetColumnPieces(int column)
    {
        List<GameObject> dots = new List<GameObject>();
        for (int i = 0; i < board.width; i++)
        {
            if (board.allDots[column, i] != null)
            {
                dots.Add(board.allDots[column, i]);
                board.allDots[column, i].GetComponent<Dot>().isMatched = true;
            }
        }
        return dots;
    }

    List<GameObject> GetRowPieces(int row)
    {
        List<GameObject> dots = new List<GameObject>();
        for (int i = 0; i < board.width; i++)
        {
            if (board.allDots[i, row] != null)
            {
                dots.Add(board.allDots[i, row]);
                board.allDots[i, row].GetComponent<Dot>().isMatched = true;
            }
        }
        return dots;
    }
}
