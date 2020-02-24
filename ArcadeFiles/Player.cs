using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
using GXPEngine.ArcadeFiles.PowerUps;




public class Player : Unit
{
    int frame = 0;
    UnitState playerState=  UnitState.IDLE;

    ArcadeCamera _cameraRef = null;
    ArcadeCamera PlayerCamera {
        get {
            if (_cameraRef != null)
                return _cameraRef;
            else {
                Console.WriteLine("Player does not have a camera");
                return null;
            }
        }
        set {
            if (_cameraRef != null)
            {
                _cameraRef.LateDestroy(); // removes the previous camera to add a new one
                Console.Write("Warning! - replacing a camera from a player");
            }
            _cameraRef = value;
            AddChild(value);
        }
    }


    private PowerUpManager _powerUpManager;



    public Player(string spriteSheet, int cols, int rows)
    {
        visuals = new AnimationSprite(spriteSheet, cols, rows,-1,false,false);
        AddChild(visuals);
        AddHitBox();
        _powerUpManager = new PowerUpManager(this);
        AddChild(new Controller(2));
    }


    public override void Update() {
        resetPlayerEffects();
        _powerUpManager.ApplyActiveEffects();

        base.Update();



        switch (playerState) {
            case UnitState.IDLE:
                visuals.Mirror(true, false);
                break;
            case UnitState.RUN:
                if (dx > 0)
                {
                    visuals.Mirror(true, false);
                    Animate(300/ActualMaxSpeed);
                    //visuals.NextFrame();
                }
                else if (dx < 0)
                {
                    visuals.Mirror(false, false);
                    Animate(300 / ActualMaxSpeed);
                    
                    //visuals.NextFrame();
                }
                else {
                    playerState = UnitState.IDLE;
                }
                break;
        }
        dx = 0;
    }


    public override void RotateWheel(bool pRight)
    {
        base.RotateWheel(pRight);

        playerState = UnitState.RUN;
    }



    public void AddCamera(int x, int y, int width, int height) {
        //PlayerCamera = new ArcadeCamera(x, y, width, height);
    }
    public void PickPowerUP(PowerUp pPowerUp) {
        _powerUpManager.PickPowerUp(pPowerUp);
    }
    override public void PressPowerUpButton() {
        _powerUpManager.UsePowerUp();
    }

    /// <summary>
    /// Sets the defaults fields values
    /// </summary>
    private void resetPlayerEffects() {
        ActualMaxSpeed = DefaultMaxSpeed;
    }
}
