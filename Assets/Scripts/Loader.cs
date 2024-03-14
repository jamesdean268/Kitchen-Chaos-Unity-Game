using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader {

    public enum Scene {
        MainMenuScene,
        GameScene,
        LoadingScene,
    }

    private static Scene targetScene;

    public static void Load(Scene targetScene) {
        // Set the target scene
        Loader.targetScene = targetScene;
        // Load the loading scene
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoaderCallback() {
        // Load the actual target scene from the loading scene once it has rendered
        SceneManager.LoadScene(targetScene.ToString());
    }

}
