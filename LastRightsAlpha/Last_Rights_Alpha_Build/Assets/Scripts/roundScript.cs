﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class roundScript : MonoBehaviour
{
    public int team1, team2, team1Max, team2Max;
    public GameObject[] team1Rounds, team2Rounds;
    public GameObject[] playersReset;
    public GameObject[] cameras;
    public Text myText;

    CapturePercentage capRef;
    GeneralUIControl uiRef;
	// Use this for initialization
	void Start ()
    {
        uiRef = GetComponent<GeneralUIControl>();
        capRef = GameObject.FindGameObjectWithTag("MainCapture").GetComponent<CapturePercentage>();
        cameras = GameObject.FindGameObjectsWithTag("Camera");
        PlayerPrefs.GetInt("team1RoundsWon");
        PlayerPrefs.GetInt("team2RoundsWon");
        PlayerPrefs.SetInt("team1RoundsWon", 0);
        PlayerPrefs.SetInt("team2RoundsWon", 0);
        for (int i = 0; i < PlayerPrefs.GetInt("team1RoundsWon"); ++i)
        {
            
            team1Rounds[i].SetActive(true);
        }
        for (int i = 0; i < PlayerPrefs.GetInt("team2RoundsWon"); ++i)
        {
            team2Rounds[i].SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (team1 <= 0)
        {
            setRounds(2);
        }
        else if (team2 <= 0)
        {
            setRounds(1);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void setRounds(int team)
    {
        if (team == 2)
        {
            team2 -= 1;
            if (team2 <= 0)
            {
                //PlayerPrefs.SetInt("team1RoundsWon", PlayerPrefs.GetInt("team1RoundsWon") + 1);
                playerReset();
                team2 = team2Max;
                team1 = team1Max;
                WinConditions();
            }
        }
        else
        {
            team1 -= 1;
            if (team1 <= 0)
            {
                //PlayerPrefs.SetInt("team2RoundsWon", PlayerPrefs.GetInt("team2RoundsWon") + 1);
                playerReset();
                team2 = team2Max;
                team1 = team1Max;
                WinConditions();
            }
        }
        if (PlayerPrefs.GetInt("team1RoundsWon") >= 2)
        {
            myText.text = "Team 1 Wins!";
            Time.timeScale = 0;
        }
        else if (PlayerPrefs.GetInt("team2RoundsWon") >= 2)
        {
            myText.text = "Team 2 Wins!";
            Time.timeScale = 0;
        }
    }

    void playerReset()
    {
        Debug.Log("happaned");
        team2 = team2Max;
        team1 = team1Max;
        for (int i = 0; i < PlayerPrefs.GetInt("team1RoundsWon"); ++i)
        {
            team1Rounds[i].SetActive(true);
        }
        for (int i = 0; i < PlayerPrefs.GetInt("team2RoundsWon"); ++i)
        {
            team2Rounds[i].SetActive(true);
        }
        foreach (GameObject plr in playersReset)
        {
            plr.GetComponent<Damage>().SendMessage("reset");
        }
    }

    void AddPlayerBack(int theTeam)
    {
        if (theTeam == 1)
        {
            team1 += 1;
        }
        else
            team2 += 1;
    }

    void WinConditions()
    {
        int roundWinner = 0;
        //also put percentage and stuff
        if (team1 > team2 || capRef.outPercent1 >= capRef.outPercent2)
            roundWinner = 1;
        else if (team1 < team2 || capRef.outPercent2 > capRef.outPercent1)
            roundWinner = 2;
        foreach (GameObject cam in cameras)
        {
            cam.GetComponentInChildren<Canvas>().SendMessage("RoundWinner", roundWinner);
        }

        capRef.SendMessage("RoundReset");
        uiRef.SendMessage("RoundReset");
        playerReset();
    }

}
