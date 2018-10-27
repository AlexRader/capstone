using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerOrderNetworked : Photon.MonoBehaviour
{
    private InputNames playerInputs;

    public playerOrderNetworked()
    {
        playerInputs.horizontalMove = "horMove";
        playerInputs.verticalMove   = "vertMove";
        playerInputs.horizontalAim  = "horAim";
        playerInputs.verticalAim    = "vertAim";
        playerInputs.leftBumper     = "LB";
        playerInputs.rightBumper    = "RB";
        playerInputs.leftTrigger    = "LT";
        playerInputs.rightTrigger   = "RT";
    }

    public struct InputNames
    {
        public string   horizontalMove,
                        verticalMove,
                        horizontalAim,
                        verticalAim,
                        leftBumper,
                        rightBumper,
                        leftTrigger,
                        rightTrigger;

    };

    public InputNames returnDefaultInputs()
    {
        return playerInputs;
    }
}
