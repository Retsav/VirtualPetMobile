using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ThreeCupsUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup gameOverCanvasGroup;
    [SerializeField] private TextMeshProUGUI pointsLabel;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button exitButton;
    
    
    
    private IMinigameService _minigameService;
    private ISceneManagmentService _sceneManagmentService;

    [Inject]
    private void ResolveDependencies(IMinigameService minigameService, ISceneManagmentService sceneManagmentService)
    {
        _minigameService = minigameService;
        _sceneManagmentService = sceneManagmentService;
    }

    private void Start()
    {
        _minigameService.OnMinigamePointsUpdated += MinigameService_OnPointsUpdated;
        _minigameService.OnMinigameOver += MinigameService_OnGameOver;
        gameOverCanvasGroup.alpha = 0f;
        retryButton.onClick.RemoveAllListeners();
        retryButton.onClick.AddListener(RetryMinigame);
        exitButton.onClick.RemoveAllListeners();
        exitButton.onClick.AddListener(ExitMinigame);
    }

    private void ExitMinigame() => _sceneManagmentService.OnLoadScene(SceneType.MainGame);
    private void RetryMinigame() => _sceneManagmentService.OnLoadScene(SceneType.ThreeCups);

    private void MinigameService_OnGameOver(object sender, MinigameType minigameType)
    {
        if (minigameType != MinigameType.ThreeCups)
            return;
        gameOverCanvasGroup.alpha = 1f;
    }

    private void MinigameService_OnPointsUpdated(object sender, MinigamePointsUpdatedEventArgs e)
    {
        if (e.MinigameType != MinigameType.ThreeCups)
            return;
        pointsLabel.text = $"{e.Points}";
    }

    private void OnDestroy()
    {
        _minigameService.OnMinigamePointsUpdated -= MinigameService_OnPointsUpdated;
        _minigameService.OnMinigameOver -= MinigameService_OnGameOver;
        retryButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();
    }
}
