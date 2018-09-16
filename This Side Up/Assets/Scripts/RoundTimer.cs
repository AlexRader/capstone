using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class RoundTimer : MonoBehaviour
{
    public float RoundTime;
    public GameObject flag1, flag2;
    bool roundEnd = false;
    public TextMeshProUGUI gameText, hiddenBoi;
    int convert;
    private void Start()
    {
        hiddenBoi.enabled = false;
    }
    // Update is called once per frame
    void Update ()
    {
        RoundTime -= Time.deltaTime;
        convert = (int)RoundTime;
        if (RoundTime > 0)
            gameText.text = "Time left: " + convert.ToString();
        if (RoundTime <= 0 && !roundEnd)
            EndRound();
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        if (roundEnd)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
                Time.timeScale = 1;
            }
        }

    }

    void EndRound()
    {
        roundEnd = true;
        hiddenBoi.enabled = true;
        if (flag1.transform.position.y > flag2.transform.position.y)
            gameText.text = "Red team wins!";
        else if (flag1.transform.position.y < flag2.transform.position.y)
            gameText.text = "Blue team wins!";
        else
            gameText.text = "It's a tie!";
        Time.timeScale = 0;
    }
}
