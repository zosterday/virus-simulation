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

    private const int DefaultInfectionChance = 100;

    private const int DefaultRecoveryChance = 30;

    [SerializeField]
    private Button startButton;

    [SerializeField]
    private TMP_InputField infectionChanceInput;

    [SerializeField]
    private TMP_InputField recoveryChanceInput;

    public void StartGame()
    {
        SceneManager.LoadScene(SimulationScene);

        if (!int.TryParse(infectionChanceInput.text, out var infectionChance))
        {
            infectionChance = DefaultInfectionChance;
        }

        if (!int.TryParse(recoveryChanceInput.text, out var recoveryChance))
        {
            recoveryChance = DefaultRecoveryChance;
        }

        infectionChance = Mathf.Min(infectionChance, 100);
        recoveryChance = Mathf.Min(recoveryChance, 100);

        StateMachine.InfectionChance = infectionChance;
        StateMachine.RecoveryChance = recoveryChance;
    }
}
