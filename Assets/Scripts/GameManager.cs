using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject EndGameScreen;
    [SerializeField] private TMP_Text GameResultText;

    [SerializeField] public GameObject ForwardBackControls;
    [SerializeField] public GameObject RightLeftControls;

    private HealthSystem PlayerHealthSystem;
    private readonly List<HealthSystem> AIHealthSystems = new();

    public static GameManager Instance;

    private void OnEnable()
    {
        Instance = this;
    }

    private void Start()
    {
        EndGameScreen.SetActive(false);

        PlayerHealthSystem = FindObjectOfType<PlayerLogic>().HealthSystem;
        foreach (AILogic ai in FindObjectsOfType<AILogic>()) AIHealthSystems.Add(ai.HealthSystem);

        InvokeRepeating(nameof(EndGameChecker), 1f, 1f);
    }

    private void EndGameChecker()
    {
        bool allAiDied = false;
        bool playerDied = false;

        allAiDied = !AIHealthSystems.Any(e => e.Health > 0); //Ни у одного нет более 0 здоровья
        playerDied = PlayerHealthSystem.Health <= 0f;

        if (allAiDied && playerDied)
        {
            EndGame(winnerIsPlayer: null);
            return;
        }
        
        if (!allAiDied && playerDied)
        {
            EndGame(winnerIsPlayer: false);
            return;
        }

        if (allAiDied && !playerDied)
        {
            EndGame(winnerIsPlayer: transform);
            return;
        }
    }

    private void EndGame(bool? winnerIsPlayer)
    {
        EndGameScreen.SetActive(true);
        foreach (MovementSystem movement in FindObjectsOfType<MovementSystem>()) movement.BlockMovement = true;

        if (winnerIsPlayer.HasValue)
        {
            if (winnerIsPlayer.Value)
            {
                GameResultText.text = "Вы победили!";
            }
            else
            {
                GameResultText.text = "Вы проиграли!";
            }
        }
        else
        {
            GameResultText.text = "Ничья!";
        }
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}