using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
using GXPEngine.ArcadeFiles.PowerUps;

public class PowerUpManager
{
    Player _owner = null;


    PowerUp _inventoryPowerUp = null;
    private PowerUp InventoryPowerUp {
        get { return _inventoryPowerUp; }
        set {
            if (_inventoryPowerUp != null)
            {
                _inventoryPowerUp.LateDestroy();
            }
            _inventoryPowerUp = value;

            if(value != null)
            {
                _inventoryPowerUp.Picked();
            }
                
            //Console.WriteLine(value+ "-Test");
            
            //Console.WriteLine("Currently Possed Power up" + _inventoryPowerUp) ;
        }
    } 

    Dictionary<Type, PowerUp> activeEffects = new Dictionary<Type, PowerUp>();


    public PowerUpManager(Player owner)
    {
        _owner = owner;
    }


    void Update() {
        //Console.WriteLine(_inventoryPowerUp);

    }

    public void PickPowerUp(PowerUp powerUp) {
        InventoryPowerUp = powerUp;
    }
    public void UsePowerUp()
    {
        if (InventoryPowerUp == null)
        {
            Console.WriteLine("No power Up picked");
        }
        else
        {
            Console.WriteLine("used power up");
            ActivatePowerUp();
            InventoryPowerUp.Use();
                    //_inventoryPowerUp.powerUpActivated();
                    //_inventoryPowerUp.LateDestroy();
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
            InventoryPowerUp.LateDestroy();
        }
        else {
            activeEffects.Add(InventoryPowerUp.GetType(), InventoryPowerUp);
            activeEffects[InventoryPowerUp.GetType()].PowerUpTimeLeft = (int)InventoryPowerUp.PowerUpDuration * 1000;
        }
    }

}
