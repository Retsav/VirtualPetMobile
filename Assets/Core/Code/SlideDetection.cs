using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SlideDetection : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 endPos;
    private Camera _camera;
    
    [SerializeField] private LayerMask mask;

    private IInputService _inputService;
    
    
    
    [Inject]
    private void ResolveDependencies(IInputService inputService)
    {
        _inputService = inputService;
    }
    
    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.touchCount <= 0) return;
        Touch touch = Input.GetTouch(0);
        switch (touch.phase)
        {
            case TouchPhase.Began:
                startPos = touch.position;
                break;
            case TouchPhase.Moved:
                DetectSwipeOverCollider(touch.position);
                break;
            case TouchPhase.Ended:
                endPos = touch.position;
                DetectSwipeOverCollider(endPos);
                break;
        }
    }

    private void DetectSwipeOverCollider(Vector2 touchPosition)
    {
        Vector2 worldPoint = _camera.ScreenToWorldPoint(touchPosition);
        RaycastHit2D tempHit = Physics2D.Raycast(worldPoint, Vector2.zero, mask);
        if (tempHit.collider is not null)
        {
            _inputService.OnSlideOnCollision(tempHit);
        }
    }
}
