using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTime : MonoBehaviour
{
    int maxFish = 15;
    int spawnedFish;

    public Rigidbody fish;
    public Rigidbody wood;
    public Rigidbody honey;

    //public GameObject tree;
    public Vector3 center;
    public Vector3 size;

    private void Start()
    {
        spawnedFish = 0;
    }

    public void WoodPos()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2),   //creates a random vector3 for
                                           Random.Range(-size.y / 2, size.y / 2),   //the plane's spawning position
                                           Random.Range(-size.z / 2, size.z / 2));  // within the spawn point
        Instantiate(wood, pos, gameObject.transform.rotation);                     //creates the plane

        if (spawnedFish <= maxFish)
        {
            wood.AddForce(transform.up * 1000);

        }
    }

    public void FishPos()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2),   //creates a random vector3 for
                                           Random.Range(-size.y / 2, size.y / 2),   //the plane's spawning position
                                           Random.Range(-size.z / 2, size.z / 2));  // within the spawn point
        Instantiate(fish, pos, gameObject.transform.rotation);                     //creates the plane
        fish.AddForce(transform.up * 1000);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);    //creates a visual representation of the 
        Gizmos.DrawCube(center, size);              //spawn pointin the editor view for my use
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag.ToString())
        {
            case "action":
                if (this.gameObject.tag.ToString() == "fishing")
                {
                    Debug.Log("SPAWN FISH");
                    spawnedFish++;
                    FishPos();
                }
                else if (this.gameObject.tag.ToString() == "cutTree")
                {
                    Debug.Log("SPAWN WOOD");
                    Destroy(gameObject);

                    WoodPos();
                }
                break;
        }
    }
}
