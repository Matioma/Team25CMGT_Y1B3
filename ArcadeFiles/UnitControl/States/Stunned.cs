using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
class Stuned: State
{
    int stunnedMillis = 3000;
    int timer = 3000;

    public Stuned(StateMachine pOwner)
    {
        owner = pOwner;
       
    }
    public override void StateActivity()
    {
        timer -= Time.deltaTime;
        Console.WriteLine(timer);
        if (timer <= 0)
        {
            owner.CurrentState = UnitState.IDLE;
            timer = stunnedMillis;
        }
    }

    public override void StateTransition(State state)
    {
        throw new NotImplementedException();
    }

}
