using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;



class PowerUpManager
{
    Player _owner = null;

    protected PowerUp inventoryPowerUp = null;

    Dictionary<Type, PowerUp> activeEffects = new Dictionary<Type, PowerUp>();

    PowerUpManager(Player owner) {
        _owner = owner;
    }


    private void ApplyActiveEffects()
    {
        List<Type> effectsThatEnded = new List<Type>();
        foreach (var pair in activeEffects)
        {
            switch (pair.Key.ToString())
            {

                case "Pill":
                    ///Checks if pill bonus has active Time
                    if (pair.Value.PowerUpTimeLeft > 0)
                    {
                        activeEffects[pair.Key].PowerUpTimeLeft -= Time.deltaTime;
                        var pillEffect = activeEffects[pair.Key] as Pill;
                        //ActualMaxSpeed += pillEffect.SpeedBonus;

                    }
                    else
                    {
                        effectsThatEnded.Add(pair.Key);
                        break;
                    }
                    break;
                case "MetalWheel":
                    ///Checks if pill bonus has active Time
                    if (pair.Value.PowerUpTimeLeft > 0)
                    {
                        activeEffects[pair.Key].PowerUpTimeLeft -= Time.deltaTime;
                        //var pillEffect = activeEffects[pair.Key] as MetalWheel;
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
        foreach (var Type in effectsThatEnded)
        {
            activeEffects.Remove(Type);
        }
    }

}
