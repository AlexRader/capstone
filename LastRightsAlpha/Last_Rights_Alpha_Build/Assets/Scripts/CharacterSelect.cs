using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : playerOrder
{
    public const float TimeToSwitchSprite = 1.0f;
    private InputNames inputs;
    public int playerNum;
    public int Team;
    public float TimeToSwitch, lastDirection;

    float horizontal,
          vertical,
          horAim,
          vertAim,
          lt,
          rt;

    bool  lb,
          rb;
    // Start is called before the first frame update
    void Start()
    {
        TimeToSwitch = TimeToSwitchSprite;
        setInputs(playerNum);
        lastDirection = 0;
    }

    // Update is called once per frame
    void Update()
    {
        getInputs();
        switchSprite();
    }

    void setInputs(int var)
    {
        inputs.horizontalMove += var.ToString();
        inputs.verticalMove += var.ToString();
        inputs.horizontalAim += var.ToString();
        inputs.verticalAim += var.ToString();
        inputs.leftBumper += var.ToString();
        inputs.rightBumper += var.ToString();
        inputs.leftTrigger += var.ToString();
        inputs.rightTrigger += var.ToString();
    }

    void getInputs()
    {
        horizontal = Input.GetAxis(inputs.horizontalMove);
        vertical = Input.GetAxis(inputs.verticalMove);

        horAim = Input.GetAxis(inputs.horizontalAim);
        vertAim = Input.GetAxis(inputs.verticalAim);

        lb = Input.GetButtonDown(inputs.leftBumper);
        rb = Input.GetButtonDown(inputs.rightBumper);
        lt = Input.GetAxis(inputs.leftTrigger);
        rt = Input.GetAxis(inputs.rightTrigger);
    }

    void switchSprite()
    {

    }
}
