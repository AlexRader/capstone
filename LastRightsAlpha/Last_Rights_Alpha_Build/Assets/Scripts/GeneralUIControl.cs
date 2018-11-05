using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GeneralUIControl : MonoBehaviour
{
    public const float MAX_ROUND_TIMER = 90.0f;
    public float roundLength;
    roundScript playerRefs;
    GameObject[] varRefs;

    bool roundWinner;
    // Start is called before the first frame update
    void Start()
    {
        playerRefs = GetComponent<roundScript>();
        roundWinner = false;
        varRefs = GameObject.FindGameObjectsWithTag("Camera");
        roundLength = MAX_ROUND_TIMER;
    }

    // Update is called once per frame
    void Update()
    {
        if (roundLength >= 0)
        {
            RoundTimer();
            UpdateUI();
        }
        else
        {
            playerRefs.SendMessage("WinConditions");
        }
    }

    void RoundTimer()
    {
        roundLength -= Time.deltaTime;
    }
    void UpdateUI()
    {
        foreach(GameObject cam in varRefs)
        {
            cam.GetComponentInChildren<Canvas>().GetComponentInChildren<TextMeshProUGUI>().text = "Time: " + (int)roundLength;
        }
    }

    void RoundReset()
    {
        roundLength = MAX_ROUND_TIMER;
    }
}
