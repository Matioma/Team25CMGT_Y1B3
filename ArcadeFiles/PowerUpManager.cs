using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
using GXPEngine.ArcadeFiles.PowerUps;

public class PowerUpManager
{
    Player _owner = null;

    private PowerUp _inventoryPowerUp = null;
    Dictionary<Type, PowerUp> activeEffects = new Dictionary<Type, PowerUp>();


    public PowerUpManager(Player owner)
    {
        _owner = owner;
    }


    public void PickPowerUp(PowerUp powerUp) {
        if (_inventoryPowerUp != null)
        {
            _inventoryPowerUp.LateDestroy();
        }

        _inventoryPowerUp = powerUp;
    }

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

    public void UsePowerUp()
    {
        if (_inventoryPowerUp == null)
        {
            Console.WriteLine("No power Up picked");
        }
        else
        {
            ActivatePowerUp();
            _inventoryPowerUp.LateDestroy();
            _inventoryPowerUp = null;
        }
    }

    private void ActivatePowerUp()
    {

        //check if such player has current effect
        if (activeEffects.ContainsKey(_inventoryPowerUp.GetType()))
        {
            //Reset the effect time
            activeEffects[_inventoryPowerUp.GetType()].PowerUpTimeLeft = (int)_inventoryPowerUp.PowerUpDuration * 1000;
            _inventoryPowerUp.LateDestroy();
        }
        else {
            activeEffects.Add(_inventoryPowerUp.GetType(), _inventoryPowerUp);
            activeEffects[_inventoryPowerUp.GetType()].PowerUpTimeLeft = (int)_inventoryPowerUp.PowerUpDuration * 1000;
        }
    }

}
