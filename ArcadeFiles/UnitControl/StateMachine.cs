using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
public class StateMachine:GameObject
{
    public Dictionary<UnitState, State> states= new Dictionary<UnitState, State>();
    UnitState currentState;

    public UnitState CurrentState {
        get { return currentState; }
        set { currentState = value; }
    }
    public StateMachine(Unit owner)
    {
        states.Add(UnitState.IDLE, new Idle(this));
        states.Add(UnitState.IN_AIR, new InAir(this));
        states.Add(UnitState.RUN, new Run(this));

        currentState = UnitState.IDLE;
    }


    void Update() {
        states[currentState].StateActivity();
    }

}
