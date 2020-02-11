using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
using GXPEngine.Core;

public class Level:GameObject
{
    int columns=20;
    int rows =15;

    int tileSize = 64;

    int[] tiles ={
        3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,
        0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,
        0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,5,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
    };


    public Level()
    {
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < columns; j++) {
                int arrayIndex = (i*columns) + j;


                if (tiles[arrayIndex] != 0) {
                    var tile = new Tile("tiles.png", 5, 1);
                    tile.setSpriteSheetIndex(tiles[arrayIndex]-1);
                    tile.SetXY(j*tileSize,i *tileSize);
                    tile.setSpriteExtent(tileSize, tileSize);
                    AddChild(tile);
                }
            }
        }
    }
}
