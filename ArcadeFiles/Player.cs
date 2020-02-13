using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
using GXPEngine.Core;

public class Player : Unit
{
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
    


    public Player(string spriteSheet, int cols, int rows)
    {
        visuals = new AnimationSprite(spriteSheet, cols, rows,-1,false,false);
        AddChild(visuals);
        AddHitBox();
    }


    public override void Update() {
        base.Update();

        
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
            Console.WriteLine(inventoryPowerUp.message);
            ApplyEffect();
            inventoryPowerUp.LateDestroy();
            inventoryPowerUp = null;
        }
    }

    private void ApplyEffect() {
        if (inventoryPowerUp is Pill) {
            var pill = inventoryPowerUp as Pill;
            ActualMaxSpeed += pill.SpeedBonus;
        }
        Console.WriteLine("ActualMaxSpeed");
    }
}
