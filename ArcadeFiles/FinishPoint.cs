using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
class FinishPoint:ArcadeObject
{
    public string _target1Win="";
    public string _target2Win="";
    

    /*public FinishPoint() {


    }*/
    public FinishPoint(string spriteSheet, int cols, int rows)
    {
        visuals = new AnimationSprite(spriteSheet, cols, rows, -1, false, false);
        AddChild(visuals);
        AddHitBox();
    }


    public override void Update() {
        var level = parent as Level;


        var num = 0;
        foreach (var obj in level.playersList) {
            if (getHitBox().HitTest(obj.getHitBox())) {
                if (num == 0)
                {
                    GameManager.Instance.OpenLevel(_target1Win);
                }
                else {
                    GameManager.Instance.OpenLevel(_target2Win);
                }
            }
            num++;
        }
    }
}
