using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

    public interface IControllable
    {
        void UsePowerUp();
        void Jump();

        void MoveHorizontally(bool pRight);
    }
