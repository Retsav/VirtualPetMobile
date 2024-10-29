using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class BedroomUI : MonoBehaviour
{
    [SerializeField] private Button lampButton;
    [SerializeField] private Image _lampImage;
    
    [SerializeField] private CanvasGroup _bedroomroomCanvasGroup;
    [SerializeField] private FadeUI fadeUI;

    [SerializeField] private Sprite _lampOnSprite;
    [SerializeField] private Sprite _lampOffSprite;
    
    
    private bool _isLampOff = false;


    private MoodModifier _moodModifier;
    
    private IRoomService _roomService;
    private IMoodService _moodService;


    [Inject]
    private void ResolveDependencies(IRoomService roomService, IMoodService moodService)
    {
        _roomService = roomService;
        _moodService = moodService;
    }

    private void Start()
    {
        _roomService.RoomChangedEvent += OnRoomChanged;
        lampButton.onClick.AddListener(OnButtonClicked);
        _moodModifier = new MoodModifier(2f, true, true);
    }

    private void OnRoomChanged(object sender, OnRoomChangedEventArgs args)
    {
        if (args.RoomType is not BedRoom)
        {
            if (_isLampOff)
            {
                fadeUI.Hide();
                _isLampOff = false;
                _lampImage.sprite = _lampOnSprite;
                _moodService.RemoveMoodModifier(MoodTypeEnum.Sleep, _moodModifier);
            }
            
            if (!(_bedroomroomCanvasGroup.alpha > 0)) return;
            _bedroomroomCanvasGroup.alpha = 0f;
            _bedroomroomCanvasGroup.interactable = false;
            _bedroomroomCanvasGroup.blocksRaycasts = false;
        }
        else
        {
            _bedroomroomCanvasGroup.alpha = 1f;
            _bedroomroomCanvasGroup.interactable = true;
            _bedroomroomCanvasGroup.blocksRaycasts = true;
        }
    }

    private void OnButtonClicked()
    {
        if (!_isLampOff)
        {
            fadeUI.Show();
            _isLampOff = true;
            _lampImage.sprite = _lampOffSprite;
            _moodService.AddMoodModifier(MoodTypeEnum.Sleep, _moodModifier);
        }
        else
        {
            fadeUI.Hide();
            _isLampOff = false;
            _lampImage.sprite = _lampOnSprite;
            _moodService.RemoveMoodModifier(MoodTypeEnum.Sleep, _moodModifier);
        }
        
    }


    private void OnDestroy()
    {
        _roomService.RoomChangedEvent -= OnRoomChanged;
        lampButton.onClick.RemoveListener(OnButtonClicked);
    }
}
