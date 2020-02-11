using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Core;
class HitBox : Sprite
{


    static float _colliderAlpha = 0.8f;
    static bool _debugCollision = false;


    public static float ColliderAlpha {
        get { return _colliderAlpha; }
        set { _colliderAlpha = value;
            if(value >1 || value < 0) { 
                Console.WriteLine("Collider Alpha has invalid value, it should be in range [0;1]");
            }
        }
    }
    public static bool CollisionDebugging{
        get{ return _debugCollision; }
        set {
            if (value != _debugCollision)
                _debugCollision = value;
            if (_debugCollision){
                Console.WriteLine("DebugMode on");
            }
            else {
                Console.WriteLine("DebugMode off");
            }
        }
    }

    public HitBox() : base("hitbox.png") {
    }

    void Update() {
        toggleDebugMode();
    }

    public void setHitBoxSize(Vector2 size)
    {
        width = (int)size.x;
        height = (int)size.y;
    } 

    void toggleDebugMode() {
        if (_debugCollision) {
            this.alpha = ColliderAlpha;
        }
        else{
            alpha = 0.0f;
        }

    }



    void OnCollision(GameObject col) {
    }

}

