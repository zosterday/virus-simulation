using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//TODO:
//end conditions are either time runs out or all the cells are infected 


public class GameManager : MonoBehaviour
{
    public static bool IsSimActive { get; private set; }

    [SerializeField]
    private TextMeshProUGUI healthyCountText;

    [SerializeField]
    private TextMeshProUGUI infectedCountText;

    [SerializeField]
    private GameObject simEndPanel;

    private int healthyCount;

    private int infectedCount;

    // Start is called before the first frame update
    void Start()
    {
        IsSimActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!IsSimActive)
        //{
            //if (SpawnManager.IsSpawningDone)
            //{
            //    IsSimActive = true;
            //}
            //else
            //{
            //    return;
            //}
        //}

        if (!IsSimActive)
        {
            return;
        }


    }

    public void UpdateHealthyCount(int amount)
    {
        healthyCount += amount;
    }

    public void UpdateInfectedCount(int amount)
    {
        infectedCount += amount;
    }
}
