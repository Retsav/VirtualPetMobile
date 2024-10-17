using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class RoomSwipeManager : MonoBehaviour
{
    private bool isDragging = false;
    
    [SerializeField] private float snapBackThreshold = 0.3f; 
    [SerializeField] private float slideDuration = 0.25f;
    
    private float _startTouchPositionX;
    private float _currentTouchPositionX;
    private Vector3 _currentCameraPosition;

    private IRoomService _roomService;
    private IMinigameService _minigameService;
    
    private Camera _mainCamera;


    [Inject]
    private void ResolveDependencies(IRoomService roomService, IMinigameService minigameService)
    {
        _minigameService = minigameService;
        _roomService = roomService;
    }
    
    private void Start()
    {
        _mainCamera = Camera.main;
        _currentCameraPosition = _mainCamera.transform.position;
    }


    void Update()
    {
        if (_minigameService == null)
            return;
        if (_minigameService.IsInMinigame)
            return;
        if (Input.touchCount == 1)
        {
            var touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    StartSwipe(touch);
                    break;
                case TouchPhase.Moved:
                    ContinueSwipe(touch);
                    break;
                case TouchPhase.Ended:
                    EndSwipe(touch);
                    break;
                default:
                    break;
            }
        }
    }

    private void StartSwipe(Touch touch)
    {
        isDragging = true;
        _startTouchPositionX = touch.position.x;
        _currentCameraPosition = _mainCamera.transform.position;
    }
    
    private void EndSwipe(Touch touch)
    {
        isDragging = false;
        float swipeDelta = _currentTouchPositionX - _startTouchPositionX;
        float normalizedSwipe = swipeDelta / Screen.width;
        
        if (Mathf.Abs(normalizedSwipe) > snapBackThreshold)
        {
            int currentRoomIndex = _roomService.GetCurrentRoomIndex();
            if (normalizedSwipe > 0 && currentRoomIndex > 0) // Swipe left
            {
                _roomService.SwitchRooms(currentRoomIndex - 1);
            }
            else if (normalizedSwipe < 0 && currentRoomIndex < _roomService.GetRooms().Count - 1) // Swipe right
            {
                _roomService.SwitchRooms(currentRoomIndex + 1);
            }
        }
        
        AnimateToCurrentRoom();
    }

    private void AnimateToCurrentRoom()
    {
        int currentRoomIndex = _roomService.GetCurrentRoomIndex();
        Vector3 targetPosition = _roomService.GetRooms()[currentRoomIndex].transform.position;
        targetPosition.z = _currentCameraPosition.z;  
        _mainCamera.transform.DOMove(targetPosition, slideDuration).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            _currentCameraPosition = targetPosition;
        });
    }

    private void ContinueSwipe(Touch touch)
    {
        if (!isDragging) return;
        _currentTouchPositionX = touch.position.x;
        float swipeDelta = _currentTouchPositionX - _startTouchPositionX;
        _mainCamera.transform.position = _currentCameraPosition + new Vector3(-swipeDelta / Screen.width * _roomService.GetRoomWidth(), 0, 0);
    }


}
