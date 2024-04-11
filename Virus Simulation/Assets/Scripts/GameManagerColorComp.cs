using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerColorComp : MonoBehaviour
{
    private const float SimTime = 45f;

    public static GameManagerColorComp Instance
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
    private TextMeshProUGUI redCountText;

    [SerializeField]
    private TextMeshProUGUI greenCountText;

    [SerializeField]
    private TextMeshProUGUI blueCountText;

    [SerializeField]
    private TextMeshProUGUI redCountResultText;

    [SerializeField]
    private TextMeshProUGUI greenCountResultText;

    [SerializeField]
    private TextMeshProUGUI blueCountResultText;

    [SerializeField]
    private TextMeshProUGUI winnerResultText;

    [SerializeField]
    private GameObject resultsPanel;

    private static GameManagerColorComp instance;

    private int redCount;

    private int greenCount;

    private int blueCount;

    private int totalCells;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        IsSimActive = true;

        //Set random cell to be original infected cell
        var cellObjs = GameObject.FindGameObjectsWithTag(ColorCompCell.CellTag);

        var randomIndex1 = Random.Range(0, cellObjs.Length);
        var randomIndex2 = Random.Range(0, cellObjs.Length);
        var randomIndex3 = Random.Range(0, cellObjs.Length);

        while (randomIndex2 == randomIndex1)
        {
            randomIndex2 = Random.Range(0, cellObjs.Length);
        }
        while (randomIndex3 == randomIndex1 || randomIndex3 == randomIndex2)
        {
            randomIndex3 = Random.Range(0, cellObjs.Length);
        }

        var redCell = cellObjs[randomIndex1].GetComponent<ColorCompCell>();
        redCell.UpdateColor(CellColor.red);
        var greenCell = cellObjs[randomIndex2].GetComponent<ColorCompCell>();
        greenCell.UpdateColor(CellColor.green);
        var blueCell = cellObjs[randomIndex3].GetComponent<ColorCompCell>();
        blueCell.UpdateColor(CellColor.blue);

        totalCells = cellObjs.Length;
        //Set healthy counts and infectedCounts
        redCount = 1;
        greenCount = 1;
        blueCount = 1;

        redCountText.text = $"Red count: {redCount}";
        greenCountText.text = $"Green count: {greenCount}";
        blueCountText.text = $"Blue count: {blueCount}";

        StartCoroutine(WaitForEndSim());
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public void UpdateRedCount(int amount)
    {
        redCount += amount;
        redCountText.text = $"Red count: {redCount}";

        if (redCount == totalCells)
        {
            EndSim("Red");
        }
    }

    public void UpdateGreenCount(int amount)
    {
        greenCount += amount;
        greenCountText.text = $"Green count: {greenCount}";

        if (greenCount == totalCells)
        {
            EndSim("Green");
        }
    }

    public void UpdateBlueCount(int amount)
    {
        blueCount += amount;
        blueCountText.text = $"Blue count: {blueCount}";

        if (blueCount == totalCells)
        {
            EndSim("Blue");
        }
    }

    private IEnumerator WaitForEndSim()
    {
        yield return new WaitForSeconds(SimTime);

        var winner = "Green";
        if (redCount > blueCount && redCount > greenCount)
        {
            winner = "Red";
        }
        else if (blueCount > redCount && blueCount > greenCount)
        {
            winner = "Blue";
        }
        EndSim(winner);
    }

    private void EndSim(string winner)
    {
        IsSimActive = false;
        redCountResultText.text = $"Red count: {redCount}";
        greenCountResultText.text = $"Green count: {greenCount}";
        blueCountResultText.text = $"Blue count: {blueCount}";
        winnerResultText.text = $"Winner: {winner}!";
        resultsPanel.SetActive(true);
    }
}
