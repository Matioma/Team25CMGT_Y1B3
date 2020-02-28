using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;

class ReadySetGo:GameObject
{
    int timer=1500;
    int imageIndex = 0;


    static ReadySetGo _instance;
    public static ReadySetGo Instance {
        get {
            if (_instance == null) {
                _instance = new ReadySetGo();
            }
            return _instance;
        }
        set{
            if(_instance == null)
            {
                _instance = value;
            }
        }
    }

    AnimationSprite animationSprite;

    

    List<AnimationSprite> animationSprites = new List<AnimationSprite>();
    ReadySetGo(){
        

        animationSprites.Add(new AnimationSprite("art/gamestart_1.png", 1, 2));
        animationSprites.Add(new AnimationSprite("art/gamestart_2.png", 1, 2));
        animationSprites.Add(new AnimationSprite("art/gamestart_3.png", 1, 2));

        animationSprite = animationSprites[imageIndex];
        animationSprite.SetOrigin(animationSprite.width / 2, animationSprite.height);
        AddChild(animationSprite);

        //SetOrigin(width / 2, height);
    }



    void Update() {
        timer -= Time.deltaTime;

        if (timer <= 0) {
            imageIndex++;
            if (imageIndex >= animationSprites.Count)
            {
                _instance = null;
                LateDestroy();
            }
            else if (imageIndex < animationSprites.Count - 1)
            {
                SwitchVisuals();
                timer = 1500;
            }
            else {
                SwitchVisuals();
                timer = 300;
            }
            
        }
    }


    void SwitchVisuals() {
        animationSprite.LateRemove();
        animationSprite = animationSprites[imageIndex];
        animationSprite.SetOrigin(animationSprite.width / 2, animationSprite.height);
        AddChild(animationSprite);
    }

}

