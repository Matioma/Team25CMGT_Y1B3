using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
class Idle:State
{
    public Idle(StateMachine pOwner) {
        owner = pOwner;
    }

    public override void StateActivity() {
        Unit unit = owner.parent as Unit;

        unit.visuals.Mirror(false, false);
        unit.Animate(300 / unit.ActualMaxSpeed, 0, 1, true);
    }
    public override void StateTransition(State state)
    {
        throw new NotImplementedException();
    }
}
