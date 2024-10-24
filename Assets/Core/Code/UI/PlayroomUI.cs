using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayroomUI : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private CanvasGroup _playroomCanvasGroup;
    
    
    
    private IRoomService _roomService;
    private IPopupService _popupService;


    [Inject]
    private void ResolveDependencies(IRoomService roomService, IPopupService popupService)
    {
        _roomService = roomService;
        _popupService = popupService;
    }

    private void Start()
    {
        _roomService.RoomChangedEvent += OnRoomChanged;
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnRoomChanged(object sender, OnRoomChangedEventArgs args)
    {
        if (args.RoomType is not PlayRoom)
        {
            if (_playroomCanvasGroup.alpha > 0)
            {
                _playroomCanvasGroup.alpha = 0f;
                _playroomCanvasGroup.interactable = false;
                _playroomCanvasGroup.blocksRaycasts = false;
            }
        }
        else
        {
            _playroomCanvasGroup.alpha = 1f;
            _playroomCanvasGroup.interactable = true;
            _playroomCanvasGroup.blocksRaycasts = true;
        }
    }

    private void OnButtonClicked() => _popupService.OnOpenMinigamePopupEvent();


    private void OnDestroy()
    {
        _roomService.RoomChangedEvent -= OnRoomChanged;
        _button.onClick.RemoveListener(OnButtonClicked);
    }
}
