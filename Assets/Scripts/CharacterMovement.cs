/*
    code for movement taken in reference to 'ExplosiveJames' Reddit
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{

    public SceneLoader sceneLoader;
    public GameObject sceneObject;

    //properties for the movement of the character
    public float playerSpeed = 5.0f;
    public Rigidbody rBody;
    public float jumpSpeed = 2.0f;
    public bool isGrounded;
    public GameObject endPlatform;
    bool onlyOnce = false;
    public GameObject projectile;
    public GameObject spawnPoint;
    int numProjectiles;
    public Text UItext;
    bool readyToShoot;
    public Texture2D cursorTexture;
    public static Vector3 target;
    public GameObject holderObj;
    public GameObject cameraObj;
    public Button UIbutton;

    // Start is called before the first frame update
    void Start()
    {
        isGrounded = true;
        rBody = GetComponent<Rigidbody>();
        UItext = GameObject.Find("UIText").GetComponent<Text>();
        readyToShoot = false;
        numProjectiles = 0;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveKeyboard();

    }

    void Update() {
        if (readyToShoot) {
            Cursor.visible = true;
            if (numProjectiles > 0) {
                if(Input.GetMouseButtonDown(0)) {
                    numProjectiles--;
                    Shoot(numProjectiles, target);
                }
            }
            if(numProjectiles == 0) {
                if (GameObject.Find("Projectile(Clone)") == null) {
                    sceneObject.GetComponent<SceneLoader>().LoadLoseScene();
                }
            }
        }

    }

    void Shoot(int numProjectiles, Vector3 direction) {
        GameObject bulletInstance = Instantiate(projectile, (cameraObj.transform.position + transform.forward), Quaternion.Euler(new Vector3(0, 0, 0)));
        bulletInstance.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * 15.0f;
        UItext.text = numProjectiles + " / 8";
    }


    //keyboard movement instructions
    private void moveKeyboard(){

        Vector3 currentVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        float cameraRotation = Camera.main.transform.rotation.eulerAngles.y;
        rBody.position += Quaternion.Euler(0, cameraRotation, 0) * currentVector * playerSpeed * Time.deltaTime;

        if(Input.GetKey(KeyCode.Space) && isGrounded) {
            Vector3 jumpVelocity = new Vector3(0, jumpSpeed, 0);
            rBody.velocity += jumpVelocity;
        }

    }

    void OnCollisionEnter(Collision collider) {
        //checks that the object is grounded with the terrain
        if (collider.gameObject.CompareTag("Terrain") || collider.gameObject.CompareTag("MazeTile")) {
            isGrounded = true;
        }
        
        if (collider.gameObject.CompareTag("Unicursal")) {
            transform.position = new Vector3(15, 1, -29);
        }

        //checks that if the gameobject touches the canyon, then the lose scene is loaded
        if (collider.gameObject.CompareTag("Destroy")) {
            sceneLoader.LoadLoseScene();
        }

        if (collider.gameObject.CompareTag("Projectile")) {
            if (!readyToShoot) { 
                numProjectiles++;
                UItext.text = numProjectiles + " / 8";
                Destroy(collider.gameObject);
            }
        }

        if (collider.gameObject.CompareTag("EndPlatform")) {
            if (onlyOnce == false) {
                Debug.Log("collide with end platform");
                endPlatform.transform.position = new Vector3(66.7f, 15f, 142.7f);
                transform.position = new Vector3(66.7f, 17f, 142.7f);
                
                foreach(GameObject obj in UnicursalPath.generatedPrefabs) {
                    Destroy(obj);
                }

                Tile[] tileArray = PathScript.arr;
                for (int i = 0; i < tileArray.Length;i++) {
                    Collider tileCollider = tileArray[i].gridTile.GetComponent<Collider>();
                    tileCollider.enabled = true;
                }

                readyToShoot = true;
                onlyOnce = true;
            }
        }

    }
    
    //used to ensure that player jumps once per spacebar
    void OnCollisionExit(Collision collider) {
        if (collider.gameObject.CompareTag("Terrain") || collider.gameObject.CompareTag("MazeTile")) {
            isGrounded = false;
        }
    }






}
