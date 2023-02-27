using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const string StartScene = "StartScene";

    private const float SimTime = 55f;

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
    private TextMeshProUGUI healthyCountResultText;

    [SerializeField]
    private TextMeshProUGUI infectedCountResultText;

    [SerializeField]
    private GameObject resultsPanel;

    private static GameManager instance;

    private int healthyCount;

    private int infectedCount;

    private void Awake()
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

        healthyCountText.text = $"Healthy cell count: {healthyCount}";
        infectedCountText.text = $"Infected cell count: {infectedCount}";
    }

    // Start is called before the first frame update
    void Start()
    {
        IsSimActive = true;

        StartCoroutine(WaitForEndSim());
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public void UpdateHealthyCount(int amount)
    {
        healthyCount += amount;
        healthyCountText.text = $"Healthy cell count: {healthyCount}";

        if (healthyCount == 0)
        {
            EndSim();
        }
    }

    public void UpdateInfectedCount(int amount)
    {
        infectedCount += amount;
        infectedCountText.text = $"Infected cell count: {infectedCount}";
    }

    private IEnumerator WaitForEndSim()
    {
        yield return new WaitForSeconds(SimTime);
        EndSim();
    }

    private void EndSim()
    {
        IsSimActive = false;
        healthyCountResultText.text = $"Healthy cell count: {healthyCount}";
        infectedCountResultText.text = $"Infected cell count: {infectedCount}";
        resultsPanel.SetActive(true);
    }

    public void RestartSim()
    {
        SceneManager.LoadScene(StartScene);
    }
}
