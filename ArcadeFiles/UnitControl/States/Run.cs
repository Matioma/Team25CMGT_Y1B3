using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
class Run:State
{
    public Run(StateMachine pOwner){
        owner = pOwner;
    }
    public override void StateActivity()
    {
        Unit unit = owner.parent as Unit;
        if (unit.dx > 0)
        {
            unit.visuals.Mirror(false, false);
            unit.Animate(300 / unit.ActualMaxSpeed, 0, 9, true);
        }
        else if (unit.dx < 0)
        {
            unit.visuals.Mirror(true, false);
            unit.Animate(300 / unit.ActualMaxSpeed, 0, 9, true);
        }
        else
        {
            owner.CurrentState = UnitState.IDLE;
        }
    }

    public override void StateTransition(State state)
    {
        throw new NotImplementedException();
    }
}
