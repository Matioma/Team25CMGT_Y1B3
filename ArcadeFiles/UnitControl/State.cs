using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
public abstract class State : IState
{
    protected StateMachine owner;
    public abstract void StateActivity();
    public abstract void StateTransition(State state);
}
