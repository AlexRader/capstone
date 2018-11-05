using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturePercentage : MonoBehaviour
{
    public List<GameObject> myTargets;
    int team1C, team2C;
    int converter;
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

    void CapturePercentageUpdate()
    {
        if (myTargets.Count > 0)
        {
            foreach(GameObject plr in myTargets)
            {
                if (plr.GetComponent<Damage>().team == 1)
                    ++team1C;
                else
                    ++team2C;
            }
            if (team1C > 0 && team2C > 0)
            { }
            else if (team1C > 0)
            {
                percentage1 += Time.deltaTime;
            }
            else
            {
                percentage2 += Time.deltaTime;
            }

        }
    }
}
