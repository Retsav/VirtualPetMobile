using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneManagmentService
{
    public event EventHandler<SceneType> ChangeScene;
    public void OnLoadScene(SceneType scene);
    
}



public enum SceneType
{
    MainGame,
    ThreeCups
}
