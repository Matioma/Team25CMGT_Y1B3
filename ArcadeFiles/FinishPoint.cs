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
        AddHitBox(visuals.width,visuals.height);
    }


    public override void Update() {
        var level = parent as Level;


        var num = 0;

        int scoreTop = 0;
        int scoreBottom = 0;

        for (int i=0; i< level.playersList.Count; i++) {
            Player player = level.playersList[i] as Player;
            switch (i)
            {
                case 0:
                    scoreTop = (int)player.Score;
                    break;
                case 1:
                    scoreBottom = (int)player.Score;
                    break;
            }
        }
        

        foreach (var obj in level.playersList) {
            if (getHitBox().HitTest(obj.getHitBox())) {

                if (num == 0)
                {
                    GameManager.Instance.OpenLevel(_target1Win, scoreTop, scoreBottom);
                }
                else {
                    GameManager.Instance.OpenLevel(_target2Win, scoreTop, scoreBottom);
                }
            }
            num++;
        }
    }
}
