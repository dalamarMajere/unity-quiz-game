using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button aboutButton;

        [SerializeField] private int gameSceneIndex;
        [SerializeField] private int aboutSceneIndex;

        private void Start()
        {
            startButton.onClick.AddListener(StartGame);
            aboutButton.onClick.AddListener(ShowAboutGame);
            exitButton.onClick.AddListener(ExitGame);
        }

        private void OnDestroy()
        {
            startButton.onClick.RemoveListener(StartGame);
            aboutButton.onClick.RemoveListener(ShowAboutGame);
            exitButton.onClick.RemoveListener(ExitGame);
        }

        private void StartGame()
        {
            LoadScene(gameSceneIndex);
        }

        private void ShowAboutGame()
        {
            LoadScene(aboutSceneIndex);
        }

        private void ExitGame()
        {
            Application.Quit();
        }

        private void LoadScene(int index)
        {
            SceneManager.LoadScene(index);
        }
    }
}