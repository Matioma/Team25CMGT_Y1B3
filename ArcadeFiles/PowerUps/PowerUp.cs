using System;
using System.Collections.Generic;
public abstract class PowerUp: ArcadeObject
{
    public string message = "TestMessage";


    public float PowerUpDuration;

    protected PowerUpManager owner;


    private int _powerUpTimeLeft = 0;
    public int PowerUpTimeLeft {
        get { return _powerUpTimeLeft; }
        set {
            _powerUpTimeLeft = value;
        }
    }

    public virtual void Update() {
        var level = parent as Level;
        DoCollisionCheck(level.playersList);
        Animate(300);
    }

    public void Used() {
        onPowerUpUse();
    }
    public void Picked(PowerUpManager pOwner) {
        onPickUp();
        owner = pOwner;
    }

    protected virtual void onPowerUpUse()
    {
        Console.Write("Power Up used base");
        this.LateDestroy();
    }

    protected virtual void onPickUp() {
        Console.WriteLine(this+ "Picked UP base");
        this.LateRemove();
    }

    public abstract void ApplyEffect(Unit target);

    protected override void DoCollisionCheck(List<ArcadeObject> arcadeObjects)
    {
        foreach (var arcadeObject in arcadeObjects) {
            if (getHitBox().HitTest(arcadeObject.getHitBox())) {
                var player =arcadeObject as Player;
                if (player != null) {
                    player.PickPowerUP(this);
                }
            }
        }
    }
}
