using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MinigamesPopup : MonoBehaviour
{
    [SerializeField] private CanvasGroup minigamePopupCanvasGroup;
    [SerializeField] private Button _threeCupsButton;
    
    private IMinigameService _minigameService;
    private IPopupService _popupService;

    [Inject]
    private void ResolveDependencies(IMinigameService minigameService, IPopupService popupService)
    {
        _minigameService = minigameService;
        _popupService = popupService;
    }

    private void Start()
    {
        _popupService.OpenMinigamePopupEvent += OnOpenMinigamePopupEvent;
        HidePopup();
        _threeCupsButton.onClick.AddListener(RequestThreeCupsMinigame);
    }

    private void RequestThreeCupsMinigame() => _minigameService.RequestMinigame(MinigameType.ThreeCups);

    private void ShowPopup()
    {
        minigamePopupCanvasGroup.alpha = 1;
        minigamePopupCanvasGroup.blocksRaycasts = true;
        minigamePopupCanvasGroup.interactable = true;
    }

    private void HidePopup()
    {
        minigamePopupCanvasGroup.alpha = 0;
        minigamePopupCanvasGroup.blocksRaycasts = false;
        minigamePopupCanvasGroup.interactable = false;
    }
    
    private void OnOpenMinigamePopupEvent(object sender, EventArgs e)
    {
        ShowPopup();
    }

    private void OnDestroy()
    {
        _threeCupsButton.onClick.RemoveAllListeners();
        _popupService.OpenMinigamePopupEvent -= OnOpenMinigamePopupEvent;
    }
}
