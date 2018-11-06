using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityTimer : MonoBehaviour
{
    public GameObject playerRef;
    public int abilityChildNumb;
    TextMeshProUGUI myText;
    float timer;
    bool below0;

    //these are for tenth of a second number output
    int timeConvert;
    float outputTime;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        below0 = false;
        myText = transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
        //long story shot get the ability at x child Number
        playerRef = transform.root.gameObject.GetComponent<CameraControl>().playerRef.transform.GetChild(abilityChildNumb).gameObject;
        playerRef.SendMessage("setuiElement", gameObject);
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (!below0)
        {
            if (timer <= 0)
            {
                below0 = true;
                myText.text = "";
            }
            else
            {
                timeConvert = (int)(timer * 10);
                outputTime = (float)timeConvert / 10;
                myText.text = "" + outputTime;
            }
        }
    }

    void resetTimer(float time)
    {
        timer = time;
        below0 = false;
    }
}
