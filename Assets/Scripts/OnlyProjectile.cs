using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyProjectile : MonoBehaviour
{

    public GameObject sceneObject;

    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }


    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collider) {

        if (collider.gameObject.CompareTag("MazeTile")) {
            Destroy(collider.gameObject);
            Destroy(gameObject);
            sceneObject.GetComponent<SceneLoader>().LoadWinScene();
        }

        if (collider.gameObject.CompareTag("NormalTile")) {
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }

    }
}
