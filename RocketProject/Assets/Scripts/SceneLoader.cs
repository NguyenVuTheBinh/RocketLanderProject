using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public enum Scene
    {
        MenuScene,
        InGameScene,
        GameOverScene
    }
    public static void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());  
    }
}
