using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
interface IState
{
    void StateActivity();
    void StateTransition(State state);
}
