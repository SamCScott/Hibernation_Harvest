using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BankItems : MonoBehaviour
{
    public Text bankW;
    public Text bankH;
    public Text bankF;
    public bool isSafe = false;
    //public GameObject bankZone;

    void Start()
    {
        GameManager.Instance.itemBank[0] = 0;
        GameManager.Instance.itemBank[1] = 0;
        GameManager.Instance.itemBank[2] = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.timeScale == 1)
            {
                BankingItems();
            }
            //Debug.Log("ITEMS DEPOSITED; GOOD JOB");
        }
        bankW.text = GameManager.Instance.itemBank[0].ToString();
        bankH.text = GameManager.Instance.itemBank[1].ToString();
        bankF.text = GameManager.Instance.itemBank[2].ToString();
    }

    void BankingItems()
    {
        if (isSafe == true)
        {
            if (GameManager.Instance.pickUp[0] >= 1)
            {
                GameManager.Instance.itemBank[0] += GameManager.Instance.pickUp[0];
                GameManager.Instance.pickUp[0] = 0;
            }
            if (GameManager.Instance.pickUp[1] >= 1)
            {
                GameManager.Instance.itemBank[1] += GameManager.Instance.pickUp[1];
                GameManager.Instance.pickUp[1] = 0;
            }
            if (GameManager.Instance.pickUp[2] >= 1)
            {
                GameManager.Instance.itemBank[2] += GameManager.Instance.pickUp[2];
                GameManager.Instance.pickUp[2] = 0;
            }
            //Debug.Log("ITEMS DEPOSITED; GOOD JOB");
        }

        if (GameManager.Instance.stage <= 1 && GameManager.Instance.itemBank[0] >= 1 &&
           GameManager.Instance.itemBank[1] >= 1 &&
           GameManager.Instance.itemBank[2] >= 1)
        {
            GameManager.Instance.tutorialComplete = true;
            //Debug.Log("STAGE COMPLETE");
        }
        else if (GameManager.Instance.stage > 1 && GameManager.Instance.itemBank[0] >= 5 &&
                                                  GameManager.Instance.itemBank[2] >= 5)
        {
            GameManager.Instance.levelComplete = true;
            GameManager.Instance.stage++;
            //Debug.Log("STAGE COMPLETE");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.ToString() == "Player")
        {
            //Debug.Log("YOU ARE HOME!");
            isSafe = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.ToString() == "Player")
        {
            //Debug.Log("LEFT HOME, GO GET SOME STUFF!");
            isSafe = false;
        }
    }
}
