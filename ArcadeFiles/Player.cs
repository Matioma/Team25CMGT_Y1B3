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

    Camera _cameraRef = null;
    Camera PlayerCamera {
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
    protected PowerUp inventoryPowerUp = null;

    
    //Holds the active power up and Seconds left until finished
    Dictionary<Type, PowerUp> activeEffects = new Dictionary<Type, PowerUp>();

    public Player(string spriteSheet, int cols, int rows)
    {
        visuals = new AnimationSprite(spriteSheet, cols, rows,-1,false,false);
        AddChild(visuals);
        AddHitBox();
    }


    public override void Update() {
        resetPlayerEffects();
        ApplyActiveEffects();
        base.Update();



        switch (playerState) {
            case UnitState.IDLE:
                visuals.Mirror(true, false);
                break;
            case UnitState.RUN:
                if (dx > 0)
                {
                    visuals.Mirror(true, false);
                    Animate(60);
                    //visuals.NextFrame();
                }
                else if (dx < 0)
                {
                    visuals.Mirror(false, false);
                    Animate(60);
                    
                    //visuals.NextFrame();
                }
                else {
                    playerState = UnitState.IDLE;
                }
                break;
        }

        dx = 0;
    }


    public override void MoveRight()
    {
        dx = ActualMaxSpeed;
        playerState = UnitState.RUN;
    }
    public override void MoveLeft()
    {
        dx = -ActualMaxSpeed;
        playerState = UnitState.RUN;
    }



    public void AddCamera(int x, int y, int width, int height) {
        PlayerCamera = new Camera(x, y, width, height);
    }


    public void PickPowerUP(PowerUp pPowerUp) {
       
        if (inventoryPowerUp != null) {
            inventoryPowerUp.LateDestroy();
        }

        inventoryPowerUp = pPowerUp;
    }


    override public void UsePowerUp() {
        if (inventoryPowerUp == null)
        {
            Console.WriteLine("No power Up picked");
        }
        else {
            ApplyEffect();
            inventoryPowerUp.LateDestroy();
            inventoryPowerUp = null;
        }
    }

    private void ApplyActiveEffects()
    {
        List<Type> effectsThatEnded =new List<Type>();
        foreach (var pair in activeEffects) {
            switch(pair.Key.ToString()){
                
                case "Pill":
                    ///Checks if pill bonus has active Time
                    if (pair.Value.PowerUpTimeLeft > 0)
                    {
                        activeEffects[pair.Key].PowerUpTimeLeft -= Time.deltaTime;
                        var pillEffect = activeEffects[pair.Key] as Pill;
                        ActualMaxSpeed += pillEffect.SpeedBonus;

                    }
                    else {
                        effectsThatEnded.Add(pair.Key);
                        break;
                    }
                    break;
                case "MetalWheel":
                    Console.WriteLine(pair.Key);
                    ///Checks if pill bonus has active Time
                    if (pair.Value.PowerUpTimeLeft > 0)
                    {
                        activeEffects[pair.Key].PowerUpTimeLeft -= Time.deltaTime;
                        var pillEffect = activeEffects[pair.Key] as MetalWheel;
                       // ActualMaxSpeed += pillEffect.SpeedBonus;

                    }
                    else
                    {
                        effectsThatEnded.Add(pair.Key);
                        break;
                    }
                    break;
            }
        }

        //Remove Finished Effects
        foreach (var Type in effectsThatEnded) {
            activeEffects.Remove(Type);
        }
    }

    /// <summary>
    /// Sets the defaults fields values
    /// </summary>
    private void resetPlayerEffects() {
        ActualMaxSpeed = DefaultMaxSpeed;
    }

    private void ApplyEffect() {
        switch (inventoryPowerUp) {
            case Pill pill:
                if (activeEffects.ContainsKey(pill.GetType()))
                {
                    activeEffects[pill.GetType()].PowerUpTimeLeft = (int)pill.SpeedDuration * 1000;
                    pill.LateDestroy();
                }
                else {
                    pill.PowerUpTimeLeft = (int)pill.SpeedDuration * 1000;
                    activeEffects.Add(pill.GetType(), pill);
                }
                break;
            case MetalWheel metalWheel:
                if (activeEffects.ContainsKey(metalWheel.GetType()))
                {
                    activeEffects[metalWheel.GetType()].PowerUpTimeLeft = (int)metalWheel.MetalWheelTime * 1000;
                    metalWheel.LateDestroy();
                }
                else
                {
                    metalWheel.PowerUpTimeLeft = (int)metalWheel.MetalWheelTime * 1000;
                    activeEffects.Add(metalWheel.GetType(), metalWheel);
                }
                break;
        }
    }




}
