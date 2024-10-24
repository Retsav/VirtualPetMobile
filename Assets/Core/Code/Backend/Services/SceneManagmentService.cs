using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagmentService : ISceneManagmentService
{
    public void OnLoadScene(SceneType scene) => ChangeScene?.Invoke(this, scene);
    public event EventHandler<SceneType> ChangeScene;
}
