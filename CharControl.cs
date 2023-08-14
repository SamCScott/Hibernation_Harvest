using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharControl : MonoBehaviour
{
    [SerializeField]
    //movement values
    readonly float moveSpeed = 6.5f;
    readonly float rotSpeed = 4.5f;
    Vector3 forward, right, movement;

    //fishing and fighting variables
    public Transform hitBox;

    public Text wood;
    public Text honey;
    public Text fish;
    public Text health;

    public Animator charAnim;


    void StageCheck()
    {
        GameManager.Instance.stage = SceneManager.GetActiveScene().buildIndex;
        if (GameManager.Instance.stage == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    void Start()
    {
        StageCheck();
        charAnim = gameObject.GetComponent<Animator>();


        GameManager.Instance.levelComplete = false;
        GameManager.Instance.tutorialComplete = false;

        GameManager.Instance.pickUp[0] = 0;
        GameManager.Instance.pickUp[1] = 0;
        GameManager.Instance.pickUp[2] = 0;

        GameManager.Instance.playerHealth = GameManager.Instance.playerMaxHealth;

        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);

        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }


    void FixedUpdate()
    {
        Move();
        Turn();
        HealthUp();
        Fishing();

        wood.text = GameManager.Instance.pickUp[0].ToString();
        honey.text = GameManager.Instance.pickUp[1].ToString();
        fish.text = GameManager.Instance.pickUp[2].ToString();

        if (GameManager.Instance.playerHealth <= 0)
        {
            GameManager.Instance.playerHealth = 0;
        }
        health.text = GameManager.Instance.playerHealth.ToString();
    }

    void Move()
    {
        if (Input.GetButton("Vertical"))
        {
            float upMovement = Input.GetAxisRaw("Vertical");
            Vector3 moveVec = new Vector3(upMovement, 0, 0);
            moveVec = moveVec.normalized;

            transform.position += upMovement * moveSpeed * Time.deltaTime * transform.forward;
            charAnim.SetInteger("walk", 1);
        }
        else
        {
            charAnim.SetInteger("walk", 0);
        }
    }
    void Turn()
    {
        if (Input.GetButton("Horizontal"))
        {
            float rotAction = Input.GetAxisRaw("Horizontal");
            transform.Rotate(0, rotAction * rotSpeed, 0);
        }
    }

    void HealthUp()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (GameManager.Instance.pickUp[1] >= 1 && GameManager.Instance.playerHealth < GameManager.Instance.playerMaxHealth)
            {
                GameManager.Instance.pickUp[1]--;
                GameManager.Instance.playerHealth++;
            }
            else if (GameManager.Instance.pickUp[1] < 1 && GameManager.Instance.playerHealth < GameManager.Instance.playerMaxHealth)
            {
                Debug.Log("NO HONEY; NO HEAL");
            }
            else
            {
                Debug.Log("FULL HEALTH; NO NEED TO HEAL!");
            }
        }
    }

    void Fishing()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            hitBox.gameObject.SetActive(true);
            //Debug.Log("The key is down Dumbass");
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            hitBox.gameObject.SetActive(false);
            //Debug.Log("The key is up Dumbass");
        }
    }

    private void OnTriggerEnter(Collider other)
    {


        switch (other.gameObject.tag.ToString())
        {
            case "pickUpWood":

                Destroy(other.gameObject);
                GameManager.Instance.pickUp[0]++;
                break;
            case "pickUpHoney":

                Destroy(other.gameObject);
                GameManager.Instance.pickUp[1]++;
                break;
            case "pickUpFish":
                Destroy(other.gameObject);
                GameManager.Instance.pickUp[2]++;
                break;
        }

    }

}
