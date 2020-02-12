using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;

abstract class Unit : ArcadeObject
{
    protected int speedX = 0;
    protected int MaxSpeedY = 1;
    protected int speedY = 0;


    protected bool onGround = false;

    protected float jumpForce = 10;

    public override void Update() {
        base.Update();
        ApplyGravity();
    }

    public void Move(int speed)
    {
        hitBox.MoveUntilCollision(speed, 0f);
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
}

