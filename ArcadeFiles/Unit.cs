using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
using GXPEngine.Core;

abstract class Unit : ArcadeObject
{
    protected int dy = 0;
    protected int _dx = 0;

    /*public int DX {
        set { dy = value; }
    }*/



    protected int MaxSpeedY = 1;


    protected int _maxSpeedX = 0;
    public int MaxSpeedX {
        get{ return _maxSpeedX; }
        set { _maxSpeedX = value; }
    }


    protected int _jumpForce = 0;
    public int JumpForce {
        get { return _jumpForce; }
        set {
            if (value > 0)
            {
                _jumpForce = -value;
            }
            else {
                _jumpForce = value;
            }

        }
    }


    protected bool _onGround = false;

    public bool OnGround
    {
        get { return _onGround; }
        set {
            _onGround = value;
        }
    }


   


    public virtual void Update()
    {
        Vector2 worldPosition = hitBox.TransformPoint(hitBox.x, hitBox.y);
        SetXY(worldPosition.x, worldPosition.y);
        /*if (OnGround) {
            if(dy>= 0)
                SetXY(worldPosition.x, worldPosition.y-1);
        }else { 
            SetXY(worldPosition.x, worldPosition.y);
        }*/

        hitBox.SetXY(0, 0);
        ApplyGravity();
        
    }

    public void MoveRight()
    {
        _dx = _maxSpeedX;
    }
    public void MoveLeft()
    {
        _dx = -_maxSpeedX;
    }

    public void Jump() {
        if (_onGround)
        {
            dy = JumpForce;
        }
        
    }

    void ApplyGravity() {
        if (dy < MaxSpeedY)
        {
            dy += 2;
        }
        else {
            dy = MaxSpeedY;
        }
    }

    public void UsePowerUp(int controller) {
        Console.WriteLine(controller + "-used powerup");
    }
}

