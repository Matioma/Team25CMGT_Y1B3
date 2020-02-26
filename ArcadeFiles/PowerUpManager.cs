using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
using GXPEngine.ArcadeFiles.PowerUps;

public class PowerUpManager
{


    Player _owner = null;
    public Player Owner {
        get { return _owner; }
    }



    PowerUp _inventoryPowerUp = null;
    private PowerUp InventoryPowerUp {
        get { return _inventoryPowerUp; }
        set {
            if (_inventoryPowerUp != null)
            {
                //_inventoryPowerUp.LateDestroy();
            }
            _inventoryPowerUp = value;

            if (value != null)
            {
                _inventoryPowerUp.Picked(this);
            }
        }
    }

    Dictionary<Type, PowerUp> activeEffects = new Dictionary<Type, PowerUp>();




    public PowerUpManager(Player owner)
    {
        _owner = owner;
    }

    public void PickPowerUp(PowerUp powerUp) {
        if (powerUp is SlowEffect)
        {
            AddEffect(powerUp);
            powerUp.Picked(this);
            powerUp.Used();

            return;
        }


        InventoryPowerUp = powerUp;
        if (InventoryPowerUp is Pill || InventoryPowerUp is MetalWheel) {
            UsePowerUp();
        }

    }
    public void UsePowerUp()
    {
        if (InventoryPowerUp == null)
        {
            Console.WriteLine("No power Up picked");
        }
        else
        {
            AddEffect(InventoryPowerUp);
            InventoryPowerUp.Used();
            InventoryPowerUp = null;
        }
    }

    /// <summary>
    /// Applies current effects
    /// </summary>
    public void ApplyActiveEffects()
    {
        List<Type> effectsThatEnded = new List<Type>();
        foreach (var pair in activeEffects)
        {
            if (pair.Value.PowerUpTimeLeft > 0)
            {
                activeEffects[pair.Key].PowerUpTimeLeft -= Time.deltaTime;
                activeEffects[pair.Key].ApplyEffect(_owner);

            }
            else
            {
                effectsThatEnded.Add(pair.Key);
            }

            if (pair.Value.PowerUpTimeLeft <= 0) {
                if (Owner.popUps.ContainsKey(pair.Key))
                {
                    Owner.popUps[pair.Key].alpha = 0;
                }

            }
        }
        //Remove Finished Effects
        foreach (var Type in effectsThatEnded)
        {
            activeEffects.Remove(Type);
        }
    }


    /// <summary>
    /// Uses the power up from the inventory
    /// </summary>
    private void ActivatePowerUp()
    {
        //check if such player has current effect
        if (activeEffects.ContainsKey(InventoryPowerUp.GetType()))
        {
            //Reset the effect time
            activeEffects[InventoryPowerUp.GetType()].PowerUpTimeLeft = (int)InventoryPowerUp.PowerUpDuration * 1000;


            //InventoryPowerUp.LateDestroy();
        }
        else {
            activeEffects.Add(InventoryPowerUp.GetType(), InventoryPowerUp);
            activeEffects[InventoryPowerUp.GetType()].PowerUpTimeLeft = (int)InventoryPowerUp.PowerUpDuration * 1000;
        }
    }



    public void AddEffect(PowerUp powerUp)
    {
        //check if such player has current effect
        if (activeEffects.ContainsKey(powerUp.GetType()))
        {
            //Reset the effect time
            activeEffects[powerUp.GetType()].PowerUpTimeLeft = (int)powerUp.PowerUpDuration * 1000;



        }
        else
        {
            activeEffects.Add(powerUp.GetType(), powerUp);
            activeEffects[powerUp.GetType()].PowerUpTimeLeft = (int)powerUp.PowerUpDuration * 1000;
        }
    }


    public void PowerUpUsed(PowerUp powerUp)
    {
        Owner.popUps[powerUp.GetType()].alpha = 1;
    }
}

