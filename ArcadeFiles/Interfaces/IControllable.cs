using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

    public interface IControllable
    {
        void PressPowerUpButton();
        void PressJumpButton();

        void RotateWheel(bool pRight);
    }
