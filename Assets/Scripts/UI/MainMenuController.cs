using Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button exitButton;

        private void Start()
        {
            startButton.onClick.AddListener(StartGame);
            exitButton.onClick.AddListener(ExitGame);
        }

        private void OnDestroy()
        {
            startButton.onClick.RemoveListener(StartGame);
            exitButton.onClick.RemoveListener(ExitGame);
        }

        private void StartGame()
        {
            SceneLoader.LoadGameScene();
        }

        private void ExitGame()
        {
            Application.Quit();
        }
    }
}