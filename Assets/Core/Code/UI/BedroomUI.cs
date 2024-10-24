using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BedroomUI : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private CanvasGroup _bedroomroomCanvasGroup;
    
    
    
    private IRoomService _roomService;
    private IPopupService _popupService;
    private IMoodService _moodService;


    [Inject]
    private void ResolveDependencies(IRoomService roomService, IPopupService popupService, IMoodService moodService)
    {
        _roomService = roomService;
        _popupService = popupService;
        _moodService = moodService;
    }

    private void Start()
    {
        _roomService.RoomChangedEvent += OnRoomChanged;
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnRoomChanged(object sender, OnRoomChangedEventArgs args)
    {
        if (args.RoomType is not BedRoom)
        {
            if (_bedroomroomCanvasGroup.alpha > 0)
            {
                _bedroomroomCanvasGroup.alpha = 0f;
                _bedroomroomCanvasGroup.interactable = false;
                _bedroomroomCanvasGroup.blocksRaycasts = false;
            }
        }
        else
        {
            _bedroomroomCanvasGroup.alpha = 1f;
            _bedroomroomCanvasGroup.interactable = true;
            _bedroomroomCanvasGroup.blocksRaycasts = true;
        }
    }

    private void OnButtonClicked() => _popupService.OnOpenMinigamePopupEvent();


    private void OnDestroy()
    {
        _roomService.RoomChangedEvent -= OnRoomChanged;
        _button.onClick.RemoveListener(OnButtonClicked);
    }
}
