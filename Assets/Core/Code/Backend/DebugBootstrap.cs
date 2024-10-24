using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class DebugBootstrap : MonoBehaviour
{
    private ISceneManagmentService _sceneManager;
    
    
    [Inject]
    private void ResolveDependencies(ISceneManagmentService sceneManager) => _sceneManager = sceneManager;

    private void Start() => _sceneManager.ChangeScene += SceneManager_OnChangeScene;

    private void SceneManager_OnChangeScene(object sender, SceneType e) => StartCoroutine(UnloadScenesAndLoadNewScene(e));

    private IEnumerator UnloadScenesAndLoadNewScene(SceneType scene)
    {
        HashSet<AsyncOperation> asyncOperations = new HashSet<AsyncOperation>();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene loadedScene = SceneManager.GetSceneAt(i);
            if (loadedScene.buildIndex == 4) continue;
            if (loadedScene.isLoaded) asyncOperations.Add(SceneManager.UnloadSceneAsync(loadedScene));
        }

        foreach (var asyncOperation in asyncOperations)
        {
            while (!asyncOperation.isDone)
            {
                yield return null;
            }
        }

        asyncOperations.Clear();
        switch (scene)
        {
            case SceneType.MainGame:
                asyncOperations.Add(SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive));
                asyncOperations.Add(SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive));
                break;
            case SceneType.ThreeCups:
                asyncOperations.Add(SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive));
                asyncOperations.Add(SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive));
                break;
            default:
                Debug.LogError("No logic for scene implemented");
                break;
        }

        foreach (var asyncOperation in asyncOperations)
        {
            while (!asyncOperation.isDone)
            {
                yield return null;
            }
        }

        switch (scene)
        {
            case SceneType.MainGame:
                SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
                break;
            case SceneType.ThreeCups:
                SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2));
                break;
        }
    }
}
