using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturePercentage : MonoBehaviour
{
    public List<GameObject> myTargets;
    int team1C, team2C;
    int converter;
    public float outPercent1, outPercent2;
    public float percentage1, percentage2;

    // Start is called before the first frame update
    void Start()
    {
        team1C = team2C = 0;
        percentage1 = percentage2 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CapturePercentageUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.isTrigger)
        {
            myTargets.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.isTrigger)
        {
            myTargets.Remove(collision.gameObject);
        }
    }

    void CapturePercentageUpdate()
    {
        if (myTargets.Count > 0)
        {
            foreach(GameObject plr in myTargets)
            {
                if (plr.GetComponent<Damage>().team == 1 && !plr.GetComponent<Damage>().res)
                    ++team1C;
                else if (plr.GetComponent<Damage>().team == 2 && !plr.GetComponent<Damage>().res)
                    ++team2C;
            }
            if (team1C > 0 && team2C > 0)
            { }
            else if (team1C > 0)
            {
                percentage1 += Time.deltaTime * (team1C * 1.5f);
                converter = (int)(percentage1 * 10);
                outPercent1 = (float)converter / 10;
            }
            else
            {
                percentage2 += Time.deltaTime * (team2C * 1.5f);
                converter = (int)(percentage2 * 10);
                outPercent2 = (float)converter / 10;
            }

            team1C = 0;
            team2C = 0;
        }
    }

    void RoundReset()
    {
        percentage1 = 0;
        outPercent1 = 0;
        percentage2 = 0;
        outPercent2 = 0;
    }
}
