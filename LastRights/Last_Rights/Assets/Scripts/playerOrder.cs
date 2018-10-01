using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerOrder : MonoBehaviour
{
    private InputNames playerInputs;

    public playerOrder()
    {
        playerInputs.horizontalMove = "horMove";
        playerInputs.verticalMove   = "vertMove";
        playerInputs.leftBumper     = "LB";
        playerInputs.rightBumper    = "RB";
        playerInputs.leftTrigger    = "LT";
        playerInputs.rightTrigger   = "RT";
    }

    public struct InputNames
    {
        public string   horizontalMove,
                        verticalMove,
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
