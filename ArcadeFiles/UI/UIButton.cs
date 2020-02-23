using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
class UIButton : UIElement
{
    bool isSelected = false;
    bool IsSelecte {
        get { return isSelected; }
        set {
            isSelected = value;

            if (value)
            {
                background.SetFrame(1);
            }
            else {
                background.SetFrame(0);
            }
        }
    }

    string targetLevel = "";

    public string TargetLevel {
        get { return targetLevel; }
        set { targetLevel = value; }
    }
    /*public void setTargetLevel(string pTargetLevel) {
        targetLevel = pTargetLevel;
    }*/

    public void ActivateButton() {
        if (TargetLevel != null && TargetLevel != "") {
            Console.WriteLine("ButtonActivated ");
            GameManager.Instance.OpenLevel(TargetLevel);

        }

    }

    public void Select() {
        IsSelecte = true;
    }
    public void Deselect()
    {
        IsSelecte = false;
    }

    public UIButton(string file,int cols, int rows):base(file,cols, rows) {

    }
}

