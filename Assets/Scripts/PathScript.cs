using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathScript : MonoBehaviour
{

    public GameObject grid1, grid2, grid3, grid4, grid5, grid6, grid7, grid8, grid9, grid10,
    grid11, grid12, grid13, grid14, grid15, grid16, grid17, grid18, grid19, grid20,
    grid21, grid22, grid23, grid24, grid25;

    public Tile t1, t2, t3, t4, t5, t6, t7, t8, t9, t10,
    t11, t12, t13, t14, t15, t16, t17, t18, t19, t20,
    t21, t22, t23, t24, t25;

    Stack nStack = new Stack();
    Stack sStack = new Stack();
    Tile[] arrayPath;
    public static Tile[] arr;
    
    public static List<Tile> solutionList;

    int count = 0;
    bool check = true;
    int randomValue;


    // Start is called before the first frame update
    void Start()
    {

        instantiateTiles();
        instantiateNorth();
        instantiateEast();
        setUpPath();
        

    }

    void testTiles(Tile t) {
        if (t.north == null) {
            Debug.Log("north is null");
        } else {
            changeColor(t.north.gridTile);
        }
        if (t.east == null) {
            Debug.Log("east is null");
        } else {
            changeColor(t.east.gridTile);
        }
        if (t.south == null) {
            Debug.Log("south is null");
        } else {
            changeColor(t.south.gridTile);
        }
        if (t.west == null) {
            Debug.Log("west is null");
        } else {
            changeColor(t.west.gridTile);
        }
    }

    //note that a perfect maze can be formed but look overally convoluted without borders
    //Todo: possibly add some borders for consecutive tiles
    void setUpPath() {
        //choose a random number
        int randomColumn = Random.Range(1,6);
        
        //array can just be changed to the first row
        Tile currentTile = arr[randomColumn-1];
        changeColor(currentTile.gridTile);
        currentTile.hasBeenTraversed = true;
        sStack.Push(currentTile);
        
        List<Tile> neighborList = findNeighbors(currentTile);
        if (neighborList[0] == currentTile) {
            Debug.Log("need to backtrack");
            sStack.Pop();
            currentTile.hasBeenTraversed = false;
        }
        
        while (currentTile.row != 5) {
            //checks if the neighborList is valid
            if (!(neighborList[0] == currentTile)) {
                foreach (Tile t in neighborList) {
                    nStack.Push(t);
                }
            } else {
                sStack.Pop();
            }
            currentTile = (Tile) nStack.Pop();
            currentTile.hasBeenTraversed = true;
            sStack.Push(currentTile);
            neighborList = findNeighbors(currentTile);
        }

        solutionList = new List<Tile>();
        while (sStack.Count > 0) {
            solutionList.Add((Tile) sStack.Pop());
        }
        foreach (Tile t in solutionList) {
            changeColor(t.gridTile);
        }
        

    }

    //test it out with the editor: set some to null and use findneighbors and use hasBeenTraversed
    List<Tile> findNeighbors(Tile t) {

        List<Tile> neighborsTemp = new List<Tile>();
        List<Tile> neighbors = new List<Tile>();
        int randomSelect;
        int count = 0;

        if (t.north == null) { 
            count++;
        } else {
            if(!(t.north.hasBeenTraversed)) {
                neighborsTemp.Add(t.north);
            }
        }
        if (t.east == null) { 
            count++;
        } else {
            if(!(t.east.hasBeenTraversed)) {
                neighborsTemp.Add(t.east);
            }
        }
        if (t.south == null) { 
            count++;
        } else {
            if(!(t.south.hasBeenTraversed)) {
                neighborsTemp.Add(t.south);
            }
        }
        if (t.west == null) { 
            count++;
        } else {
            if(!(t.west.hasBeenTraversed)) {
                neighborsTemp.Add(t.west);
            }
        }

        if (neighborsTemp.Count != 0) {
            for (int i = 0; i < neighborsTemp.Count - 1; i++) {
                randomSelect = Random.Range(0,neighborsTemp.Count);
                neighbors.Add(neighborsTemp[randomSelect]);
                neighborsTemp.RemoveAt(randomSelect);
            }
            neighbors.Add(neighborsTemp[0]);
        } else {
            neighbors.Add(t);
        }
        return neighbors;
        
    }

    void printOutRowCol(List<Tile> al) {
        Debug.Log("size of list: " + al.Count);
        foreach (Tile t in al) {
            int row = t.row;
            int col = t.column;
            Debug.Log("row: " + row + " col: " + col);
        }
    }

    void changeColor(GameObject obj) {
        var renderer = obj.GetComponent<Renderer>();
        renderer.material.SetColor("_Color", Color.green);
        Collider tileCollider = obj.GetComponent<Collider>();
        tileCollider.enabled = true;
        obj.tag = "MazeTile";
    }

    void instantiateTiles() {
        //nesw
        t1 = new Tile(grid1, false, 1, 1, null, null, null, null);
        t2 = new Tile(grid2, false, 1, 2, null, null, null, t1);
        t3 = new Tile(grid3, false, 1, 3, null, null, null, t2);
        t4 = new Tile(grid4, false, 1, 4, null, null, null, t3);
        t5 = new Tile(grid5, false, 1, 5, null, null, null, t4);

        t6 = new Tile(grid6, false, 2, 1, null, null, t1, null);
        t7 = new Tile(grid7, false, 2, 2, null, null, t2, t6);
        t8 = new Tile(grid8, false, 2, 3, null, null, t3, t7);
        t9 = new Tile(grid9, false, 2, 4, null, null, t4, t8);
        t10 = new Tile(grid10, false, 2, 5, null, null, t5, t9);

        t11 = new Tile(grid11, false, 3, 1, null, null, t6, null);
        t12 = new Tile(grid12, false, 3, 2, null, null, t7, t11);
        t13 = new Tile(grid13, false, 3, 3, null, null, t8, t12);
        t14 = new Tile(grid14, false, 3, 4, null, null, t9, t13);
        t15 = new Tile(grid15, false, 3, 5, null, null, t10, t14);

        t16 = new Tile(grid16, false, 4, 1, null, null, t11, null);
        t17 = new Tile(grid17, false, 4, 2, null, null, t12, t16);
        t18 = new Tile(grid18, false, 4, 3, null, null, t13, t17);
        t19 = new Tile(grid19, false, 4, 4, null, null, t14, t18);
        t20 = new Tile(grid20, false, 4, 5, null, null, t15, t19);

        t21 = new Tile(grid21, false, 5, 1, null, null, t16, null);
        t22 = new Tile(grid22, false, 5, 2, null, null, t17, t21);
        t23 = new Tile(grid23, false, 5, 3, null, null, t18, t22);
        t24 = new Tile(grid24, false, 5, 4, null, null, t19, t23);
        t25 = new Tile(grid25, false, 5, 5, null, null, t20, t24);

        arr = new Tile[] {t1, t2, t3, t4, t5, t6, t7, t8, t9, t10,
        t11, t12, t13, t14, t15, t16, t17, t18, t19, t20,
        t21, t22, t23, t24, t25};

    }

    //instantiate north separately after the tile objects were created
    void instantiateNorth() {
        t1.north = t6;
        t2.north = t7;
        t3.north = t8;
        t4.north = t9;
        t5.north = t10;

        t6.north = t11;
        t7.north = t12;
        t8.north = t13;
        t9.north = t14;
        t10.north = t15;

        t11.north = t16;
        t12.north = t17;
        t13.north = t18;
        t14.north = t19;
        t15.north = t20;

        t16.north = t21;
        t17.north = t22;
        t18.north = t23;
        t19.north = t24;
        t20.north = t25;
    }

    //instantiate east tiles after the gameobjects were created
    void instantiateEast() {
        t1.east = t2;
        t2.east = t3;
        t3.east = t4;
        t4.east = t5;

        t6.east = t7;
        t7.east = t8;
        t8.east = t9;
        t9.east = t10;

        t11.east = t12;
        t12.east = t13;
        t13.east = t14;
        t14.east = t15;

        t16.east = t17;
        t17.east = t18;
        t18.east = t19;
        t19.east = t20;

        t21.east = t22;
        t22.east = t23;
        t23.east = t24;
        t24.east = t25;
    }


}
