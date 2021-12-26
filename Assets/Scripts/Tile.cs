using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public bool hasBeenTraversed;
    public GameObject gridTile;
    public Tile north, east, west, south;
    public int row, column;
    
    public Tile(GameObject self, bool traversed, int pRow, int pColumn, Tile nObj, Tile eObj, Tile sObj, Tile wObj) {
        gridTile = self;
        hasBeenTraversed = traversed;
        row = pRow;
        column = pColumn;
        north = nObj;
        east = eObj;
        west = wObj;
        south = sObj;
    }


}
