using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
using GXPEngine.ArcadeFiles.PowerUps;






public class Player : Unit
{
    int frame = 0;


    public float spawnX = 0;
    public float Score{
        get { return (x - spawnX); }

    }
    

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
    public InventorySlot slotObject;


    //private EasyDraw easyDraw;

    public Dictionary<Type, AnimationSprite> popUps = new Dictionary<Type, AnimationSprite>(); 


    public Player(string spriteSheet, int cols, int rows)
    {
        visuals = new AnimationSprite(spriteSheet, cols, rows,-1,false,false);
        AddChild(visuals);
        AddHitBox();


        _powerUpManager = new PowerUpManager(this);
        AddChild(new Controller(2));
        
        AddChild(stateMachine);


        popUps.Add(typeof(Pill), new AnimationSprite("art/popup_pill.png", 1, 1));
        popUps.Add(typeof(AcidBottle), new AnimationSprite("art/popup_acid.png", 1, 1));
        //popUps.Add(typeof(Pill), new AnimationSprite("popup_carrot.png", 1, 1));
        popUps.Add(typeof(MetalWheel), new AnimationSprite("art/popup_wheel.png", 1, 1));
        //popUps.Add(typeof(Pill), new AnimationSprite("popup_glasses.png", 1, 1));

        foreach (var pair in popUps) {
            AnimationSprite animationSprite = pair.Value;
            animationSprite.SetOrigin(animationSprite.width / 2, animationSprite.height);
            animationSprite.SetScaleXY(0.4f);
            animationSprite.SetXY(0,-60);
            pair.Value.alpha = 0;
            AddChild(pair.Value);
        }
    }


    public override void Update() {
        resetPlayerEffects();
        _powerUpManager.ApplyActiveEffects();

        base.Update();


        dx *= 0.9f;
        if (Math.Abs(dx) <= 0.1f) {
            dx = 0;

        }

        //easyDraw.DrawSprite(new Sprite("Colors.png", false,false)) ;
    }


    public override void RotateWheel(bool pRight)
    {
        base.RotateWheel(pRight);
        stateMachine.CurrentState = UnitState.RUN;
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
