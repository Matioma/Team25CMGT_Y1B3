using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
class InAir:State
{
    public InAir(StateMachine pOwner)  {
        owner = pOwner;
    }
    public override void StateActivity()
    {
        Unit unit = owner.parent as Unit;
        if (unit.dx > 0)
        {
            unit.visuals.Mirror(false, false);
        }
        else if (unit.dx < 0)
        {
            unit.visuals.Mirror(true, false);
        }

        unit.Animate(300 / unit.ActualMaxSpeed, 6, 1, false);
        if (unit.OnGround)
        {
            owner.CurrentState = UnitState.IDLE;
        }
    }

    public override void StateTransition(State state)
    {
        throw new NotImplementedException();
    }
}
