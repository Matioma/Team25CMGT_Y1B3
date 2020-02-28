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
        Console.WriteLine(unit.isLookingRight);
        if (unit.OnGround) {
            if (unit.dx > 0)
            {
                unit.visuals.Mirror(!unit.isLookingRight, false);
                unit.Animate(300 / unit.ActualMaxSpeed, 10, 11, false);
            }
            else if (unit.dx < 0)
            {
                unit.visuals.Mirror(!unit.isLookingRight, false);
                unit.Animate(300 / unit.ActualMaxSpeed, 10, 11, false);
            }
            else
            {
                owner.CurrentState = UnitState.IDLE;
            }
        }
        /*if (unit.dx > 0)
        {
            unit.visuals.Mirror(!unit.isLookingRight, false);
            unit.Animate(300 / unit.ActualMaxSpeed, 10, 11, false);
        }
        else if (unit.dx < 0)
        {
            unit.visuals.Mirror(!unit.isLookingRight, false);
            unit.Animate(300 / unit.ActualMaxSpeed, 10, 11, false);
        }
        else
        {
            owner.CurrentState = UnitState.IDLE;
        }*/
    }

    public override void StateTransition(State state)
    {
        throw new NotImplementedException();
    }
}
