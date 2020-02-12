using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;

abstract class Unit : ArcadeObject
{
    protected int speedX = 0;
    protected int MaxSpeedY = 1;

    public int MaxSpeed {
        get{ return speedX; }
        set { speedX = value; }
    }
    protected int speedY = 0;


    protected bool onGround = false;

    protected float jumpForce = 15;

    public override void Update() {
        base.Update();
        ApplyGravity();
    }

    public void MoveRight()
    {
        hitBox.MoveUntilCollision(speedX, 0f);
        Console.WriteLine(speedX);
    }
    public void MoveLeft()
    {
        hitBox.MoveUntilCollision(-speedX, 0f);
    }

    public void Jump() {
        if (onGround)
        {
            speedY = -(int)jumpForce;
        }
        
    }

    void ApplyGravity() {
        if (speedY < MaxSpeedY)
        {
            speedY += 1;
        }
        else {
            speedY = MaxSpeedY;
        }
       //speedY += 1;
    }

    public void UsePowerUp(int controller) {
        Console.WriteLine(controller + "-used powerup");

    }
}

