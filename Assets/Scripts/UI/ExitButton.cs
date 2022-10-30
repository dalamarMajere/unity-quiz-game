using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class ExitButton : MonoBehaviour
    {
        [SerializeField] private int mainMenuSceneIndex;
    
        private Button _exitButton;

        private void Start()
        {
            _exitButton = GetComponent<Button>();
            _exitButton.onClick.AddListener(ExitToMainMenu);
        }

        private void OnDestroy()
        {
            _exitButton.onClick.RemoveListener(ExitToMainMenu);
        }

        private void ExitToMainMenu()
        {
            SceneManager.LoadScene(mainMenuSceneIndex);
        }
    }
}