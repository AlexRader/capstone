using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasUI : MonoBehaviour
{
    public GameObject[] myRounds;
    int activatedRounds;
    SpriteRenderer mSprite;
    Color red = new Color(255, 0, 0, 60);
    Color blue = new Color(0, 0, 255, 60);
    // Start is called before the first frame update
    void Start()
    {
        activatedRounds = 0;
    }


    void RoundWinner(int team)
    {
        if (activatedRounds < 4)
        {
            myRounds[activatedRounds].SetActive(true);
            if (team == 1)
                myRounds[activatedRounds].GetComponent<SpriteRenderer>().color = red;
            else
                myRounds[activatedRounds].GetComponent<SpriteRenderer>().color = blue;
        }
        else
            finalWinner(team);
    }
    void finalWinner(int team)
    {
        foreach (GameObject rounds in myRounds)
        {
            rounds.SetActive(true);
            if (team == 1)
                rounds.GetComponent<SpriteRenderer>().color = red;
            else
                rounds.GetComponent<SpriteRenderer>().color = blue;
        }
    }
}
