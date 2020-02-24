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
                //_inventoryPowerUp.LateDestroy();
            }
            _inventoryPowerUp = value;

            if(value != null)
            {
                _inventoryPowerUp.Picked();
            }
        }
    } 

    Dictionary<Type, PowerUp> activeEffects = new Dictionary<Type, PowerUp>();


    public PowerUpManager(Player owner)
    {
        _owner = owner;
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
            AddEffect(InventoryPowerUp);
            //ActivatePowerUp();
            InventoryPowerUp.Use();
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
            //InventoryPowerUp.LateDestroy();
        }
        else {
            activeEffects.Add(InventoryPowerUp.GetType(), InventoryPowerUp);
            activeEffects[InventoryPowerUp.GetType()].PowerUpTimeLeft = (int)InventoryPowerUp.PowerUpDuration * 1000;
        }
    }



    private void AddEffect( PowerUp powerUp)
    {
        //check if such player has current effect
        if (activeEffects.ContainsKey(powerUp.GetType()))
        {
            //Reset the effect time
            activeEffects[powerUp.GetType()].PowerUpTimeLeft = (int)powerUp.PowerUpDuration * 1000;
            //InventoryPowerUp.LateDestroy();
        }
        else
        {
            activeEffects.Add(powerUp.GetType(), powerUp);
            activeEffects[powerUp.GetType()].PowerUpTimeLeft = (int)powerUp.PowerUpDuration * 1000;
        }
    }

}
