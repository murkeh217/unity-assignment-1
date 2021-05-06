using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LeaderboardController : MonoBehaviour
{
    public GameObject leaderboardRow;
    [SerializeField] private int totalRows = 100;
    [SerializeField] private int activeRows = 10;
    private GameObject go;
    float y = 1.0f;

    private void OnEnable()
    {
        //PoolManager.InitializePool(leaderboardRow, 100, false);

//reversing for loop
        for (int i = 1; i < totalRows; i++)
        {
            go = PoolManager.Instantiate(leaderboardRow);
            go.transform.SetParent(transform,false);
            
            //spawning in a line of y axis with spacing
            float currentPos = go.transform.position.y;
            go.transform.Translate(0f, currentPos - y, 0f);
            y += 0.5f;
            
            go.transform.GetChild(0).GetComponent<Text>().text = i.ToString();
            go.transform.GetChild(1).GetComponent<Text>().text = "aaa";
            go.transform.GetChild(2).GetComponent<Text>().text = i.ToString() + 100;



        }
        
        
    }

    private void OnDisable()
    {
        //for (int i = 1; i < totalRows + 1; i++)
        //{
          //  PoolManager.Destroy(go);
        //}
    }

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}