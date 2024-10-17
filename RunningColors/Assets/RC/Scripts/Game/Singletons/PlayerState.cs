using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    static private PlayerState instance;
    static public PlayerState GetInstance() { return instance; }

    [Header("Paint")]
    public int paintCurrentAmount;
    public int paintMaxAmount;
    public PaintType paintCurrentType;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
