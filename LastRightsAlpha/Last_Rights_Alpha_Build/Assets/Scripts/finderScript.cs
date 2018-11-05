using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class finderScript : MonoBehaviour
{
    GameObject Sarcaphogas;
    public TextMeshProUGUI team1, team2;
    // Start is called before the first frame update
    void Start()
    {
        Sarcaphogas = GameObject.FindGameObjectWithTag("MainCapture");
        Debug.Log(Sarcaphogas.name);
    }

    // Update is called once per frame
    void Update()
    {
        team1.text = "Team 1 %: " + Sarcaphogas.GetComponent<CapturePercentage>().outPercent1;
        team2.text = "Team 2 %: " + Sarcaphogas.GetComponent<CapturePercentage>().outPercent2;
    }
}
