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
        playerInputs.leftBumper     = "L1";
        playerInputs.rightBumper    = "R1";
        playerInputs.leftTrigger    = "L2";
        playerInputs.rightTrigger   = "R2";
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
