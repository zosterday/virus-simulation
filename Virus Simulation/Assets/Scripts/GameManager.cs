using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get
        {
            if (instance is null)
            {
                throw new System.InvalidOperationException("Instance of GameManager is null");
            }

            return instance;
        }
    }

    public bool IsSimActive { get; private set; }

    [SerializeField]
    private TextMeshProUGUI healthyCountText;

    [SerializeField]
    private TextMeshProUGUI infectedCountText;

    [SerializeField]
    private GameObject resultsPanel;

    private static GameManager instance;

    private int healthyCount;

    private int infectedCount;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        //Set random cell to be original infected cell
        var cellObjs = GameObject.FindGameObjectsWithTag(Cell.CellTag);

        var randomIndex = Random.Range(0, cellObjs.Length);

        var infectedCell = cellObjs[randomIndex].GetComponent<Cell>();
        infectedCell.Infect();

        //Set healthy counts and infectedCounts
        healthyCount = cellObjs.Length - 1;
        infectedCount = 1;

        IsSimActive = true;

        StartCoroutine(WaitForEndSim());
    }

    public void UpdateHealthyCount(int amount)
    {
        healthyCount += amount;

        if (healthyCount == 0)
        {
            EndSim();
        }
    }

    public void UpdateInfectedCount(int amount)
    {
        infectedCount += amount;
    }

    private IEnumerator WaitForEndSim()
    {
        yield return new WaitForSeconds(50f);
        EndSim();
    }

    private void EndSim()
    {
        IsSimActive = false;
        resultsPanel.SetActive(true);
    }
}
