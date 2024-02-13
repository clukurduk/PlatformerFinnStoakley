using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int playerShots;


    public static GameManager Instance {  get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        playerShots = 2;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
