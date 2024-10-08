using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    static private GameManager instance;

    public GameObject player;
    public Camera mainCamera;

    public GameManager GetInstance() { return instance; }

    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}