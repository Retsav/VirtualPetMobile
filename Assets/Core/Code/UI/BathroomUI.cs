using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BathroomUI : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private CanvasGroup _bathroomCanvasGroup;
    
    
    
    private IRoomService _roomService;


    [Inject]
    private void ResolveDependencies(IRoomService roomService)
    {
        _roomService = roomService;
    }

    private void Start()
    {
        _roomService.RoomChangedEvent += OnRoomChanged;
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnRoomChanged(object sender, OnRoomChangedEventArgs args)
    {
        if (args.RoomType is not BathRoom)
        {
            if (_bathroomCanvasGroup.alpha > 0) _bathroomCanvasGroup.alpha = 0f;
        }
        else
            _bathroomCanvasGroup.alpha = 1f;
    }

    private void OnButtonClicked()
    {
        Debug.Log("Clicked!");
    }


    private void OnDestroy()
    {
        _roomService.RoomChangedEvent -= OnRoomChanged;
        _button.onClick.RemoveListener(OnButtonClicked);
    }
}