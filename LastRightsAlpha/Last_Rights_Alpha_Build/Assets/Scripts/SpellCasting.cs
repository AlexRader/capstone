using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCasting : MonoBehaviour
{
    enum setReturn { lt = 1, rt = 2, lb = 3, rb = 4 };
    public info passedInfo;
    public GameObject aimReticle;
    public GameObject LT, RT, LB, RB;
    public bool LTset, RTset, LBset, RBset;
    public bool casting;
    
    // Use this for initialization
    void Start ()
    {
        LTset = RTset = LBset = RBset = true;
        casting = false;
        RT.SendMessage("setReturnTo", (int)setReturn.rt);
        LB.SendMessage("setReturnTo", (int)setReturn.lb);
        RB.SendMessage("setReturnTo", (int)setReturn.rb);
        LT.SendMessage("setReturnTo", (int)setReturn.lt);
    }

    // Update is called once per frame
    void Update ()
    {
		if (passedInfo.lb && !casting && LBset)
        {
            LB.SendMessage("setStartCast");
            LBset = false;
            casting = true;
        }
        else if (passedInfo.lt > .9 && !casting && LTset)
        {
            LT.SendMessage("setStartCast");
            LTset = false;
            casting = true;
        }
        else if (passedInfo.rb && !casting && RBset)
        {
            RB.SendMessage("setStartCast");
            RBset = false;
            casting = true;
        }
        else if (passedInfo.rt < -.9 && !casting && RTset)
        {
            RT.SendMessage("setStartCast");
            RTset = false;
            casting = true;
        }
    }

    private void FixedUpdate()
    {
        if (!casting)
            aimReticle.transform.position = (new Vector3(passedInfo.horizontal, passedInfo.vertical, 0).normalized * 2) + transform.position;
    }

    void ResetCasting()
    {
        casting = false;
    }

    void setBool(int i)
    {
        switch (i)
        {
            case 1:
                LTset = true;
                break;
            case 2:
                RTset = true;
                break;
            case 3:
                LBset = true;
                break;
            case 4:
                RBset = true;
                break;
        }

    }
}
