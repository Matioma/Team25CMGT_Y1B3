using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
using GXPEngine.Core;

abstract class Unit : ArcadeObject
{
    private int dx = 0;
    private int dy = 0;

    protected int speedX = 0;
    protected int MaxSpeedY = 3;

    public int MaxSpeed {
        get{ return speedX; }
        set { speedX = value; }
    }
    protected int speedY = 0;


    protected bool onGround = false;

    protected int jumpForce = 15;
    public int JumpForce {
        get { return jumpForce; }
        set {
            if (value > 0)
            {
                jumpForce = -value;
            }
            else {
                jumpForce = value;
            }
        }

    }



    public override void Update() {
        Vector2 worldPosition = hitBox.TransformPoint(hitBox.x, hitBox.y);
        SetXY(worldPosition.x, worldPosition.y-1);
        hitBox.SetXY(0, 0);
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
            speedY = JumpForce;
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
    }

    public void UsePowerUp(int controller) {
        Console.WriteLine(controller + "-used powerup");

    }
}

