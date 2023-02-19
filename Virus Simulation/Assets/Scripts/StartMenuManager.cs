using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    private const string SimulationScene = "SimulationScene";

    [SerializeField]
    private Button startButton;

    [SerializeField]
    private TMP_InputField infectionChanceInput;

    [SerializeField]
    private TMP_InputField recoveryChanceInput;

    public void StartGame()
    {
        SceneManager.LoadScene(SimulationScene);

        var infectionChance = Mathf.Min(int.Parse(infectionChanceInput.text), 100);
        var recoveryChance = Mathf.Min(int.Parse(recoveryChanceInput.text), 100);

        StateMachine.InfectionChance = infectionChance;
        StateMachine.RecoveryChance = recoveryChance;
    }
}
