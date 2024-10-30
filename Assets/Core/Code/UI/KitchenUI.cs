using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class KitchenUI : MonoBehaviour
{
    [SerializeField] private Button minigameButton;
    [SerializeField] private CanvasGroup kitchenCanvasGroup;
    

    private IRoomService _roomService;
    private IMinigameService _minigameService;
    
    
    [Inject]
    private void ResolveDependencies(IMinigameService minigameService, IRoomService roomService)
    {
        _minigameService = minigameService;
        _roomService = roomService;
    }

    private void Start()
    {
        minigameButton.onClick.RemoveAllListeners();
        minigameButton.onClick.AddListener(OnMinigameButtonClicked);
        _roomService.RoomChangedEvent += OnRoomChanged;
    }

    private void OnRoomChanged(object sender, OnRoomChangedEventArgs e)
    {
        if (e.RoomType is not KitchenRoom)
        {
            if (kitchenCanvasGroup.alpha > 0)
            {
                kitchenCanvasGroup.alpha = 0f;
                kitchenCanvasGroup.interactable = false;
                kitchenCanvasGroup.blocksRaycasts = false;
            }
        }
        else
        {
            kitchenCanvasGroup.alpha = 1f;
            kitchenCanvasGroup.interactable = true;
            kitchenCanvasGroup.blocksRaycasts = true;
        }
    }

    private void OnMinigameButtonClicked()
    {
        _minigameService.RequestMinigame(MinigameType.Cauldron);
    }
}
