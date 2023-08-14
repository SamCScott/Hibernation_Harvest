using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int playerMaxHealth = 5;
    public int playerHealth;

    public int stage = 0;
    public bool levelComplete = false;
    public bool tutorialComplete = false;

    public int[] pickUp = new int[] { 0, 0, 0 };
    public int[] itemBank = new int[] { 0, 0, 0 };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
