using UnityEngine.SceneManagement;

namespace Data
{
    public static class SceneLoader
    {
        private static int MainMenuSceneIndex => 1;
        private static int GameSceneIndex => 0;
        private static int GameEndSceneIndex => 2;

        public static void LoadGameScene()
        {
            LoadScene(GameSceneIndex);
        }

        public static void LoadMainMenuScene()
        {
            LoadScene(MainMenuSceneIndex);
        }

        public static void LoadGameEndScene()
        {
            LoadScene(GameEndSceneIndex);
        }

        private static void LoadScene(int gameSceneIndex)
        {
            SceneManager.LoadScene(gameSceneIndex);
        }
    }
}