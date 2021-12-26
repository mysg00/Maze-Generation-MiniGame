using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicursalPath : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject projectilePrefab;
    public GameObject startingTile;
    public static List<GameObject> generatedPrefabs = new List<GameObject>();
    Vector3 spawnPosition = new Vector3(15,0,-20);
    Vector3 verticalOffset = new Vector3(0,1,0);
    int[] fArr = new int[4];
    int[] sArr = new int[3];
    public GameObject lPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //generate an array of random values for the maze distances in a forward direction
        generateFrontDirectionArray();
        //generate an array of random values for the maze distances in the side direction
        generateSideDirectionArray(); 

        //generate starting tile with its projectile
        Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
        lPrefab = Instantiate(projectilePrefab, spawnPosition + verticalOffset, Quaternion.identity);
        generatedPrefabs.Add(lPrefab);


        //generate the tiles for the unicursal path
        for (int i = 0; i < fArr.Length; i++) {
            createTileForward(fArr[i]);
            if (i != 3) {
                createTileSide(sArr[i]);
            }
        }
    }

    //method to create array with random values for the front direction array
    void generateFrontDirectionArray() {
        int sum = 0;
        int randValue;
        for (int i = 0; i < 3; i++) {
            randValue = Random.Range(2,4);
            fArr[i] = randValue;
            sum += randValue;
        }
        fArr[3] = 11 - sum;
    }

    //method to create array with random values for the side direction array
    void generateSideDirectionArray() {
        int sum = 0;
        bool lastValueZero = true;

        int randValue = Random.Range(7,13);
        sArr[0] = randValue;
        sum += randValue;
        
        while (lastValueZero) {
            randValue = Random.Range(2,randValue);
            sArr[1] = (-1) * randValue;
            if ((sum - randValue) != 6) {
                sum -= randValue;
                lastValueZero = false;
            }
        }

        sArr[2] = 6 - sum;
    }

    //instantiate tiles in the forward direction
    void createTileForward(int index) {
        for (int i = 0; i < index; i++) {
            Vector3 offset = new Vector3(0,0,8);
            Vector3 verticalOffset = new Vector3(0,1,0);
            spawnPosition = spawnPosition + offset;
            Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
            if (i == (index - 1)) {
                lPrefab = Instantiate(projectilePrefab, spawnPosition + verticalOffset, Quaternion.identity);
                generatedPrefabs.Add(lPrefab);
            }
        }
    }

    //instantiate tiles in the left or right direction
    void createTileSide(int index) {
        bool isLeft = false;
        if (index < 0) {
            index *= (-1);
            isLeft = true;
        }
        for (int i = 0; i < index; i++) {
            Vector3 offset;
            if (isLeft) {
                offset = new Vector3(-8,0,0);
            } else {
                offset = new Vector3(8,0,0);
            }
            Vector3 verticalOffset = new Vector3(0,1,0);
            spawnPosition = spawnPosition + offset;
            Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
            if (i == (index - 1)) {
                lPrefab = Instantiate(projectilePrefab, spawnPosition + verticalOffset, Quaternion.identity);
                generatedPrefabs.Add(lPrefab);
            }
        }  
    }
}
