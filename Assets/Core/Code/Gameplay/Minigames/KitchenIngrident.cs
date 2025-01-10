using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class KitchenIngrident : MonoBehaviour
{
    [SerializeField] private IngredientType ingredientType;
    [SerializeField] private GameObject cauldronObject;
    [SerializeField] private float distanceBuffer;
    

    private IInputService _inputService;
    private IMinigameService _minigameService;
    
    private Vector2 _startPosition;
    private bool _isMinigame;
    private bool _isHeld;
    private Camera _camera;
    
    [Inject]
    private void ResolveDependencies(IInputService inputService, IMinigameService minigameService)
    {
        _inputService = inputService;
        _minigameService = minigameService;
    }

    private void Start()
    {
        _camera = Camera.main;
        _minigameService.OnMinigameRequested += MinigameService_OnMinigameRequested;
        _minigameService.OnMinigameOver += MinigameService_OnMinigameOver;
        _inputService.onSlideRaycastHit += InputService_OnSlideRaycastHit;
        _inputService.onClickRaycastHit += InputService_OnClickRaycastHit;
        _inputService.onEndTouchRaycastHit += InputService_OnEndTouchRaycastHit;
    }

    private void InputService_OnEndTouchRaycastHit(object sender, RaycastHit2D e)
    {
        if (!_isMinigame) return;
        if (e.transform != transform || !_isHeld) return;
        _isHeld = false;
        Debug.Log(Vector2.Distance(transform.position, cauldronObject.transform.position));
        if(Vector2.Distance(transform.position, cauldronObject.transform.position) < distanceBuffer && _minigameService.CurrentRecipe.Contains(ingredientType))
        {
            transform.position = _startPosition;
            gameObject.SetActive(false);
            _minigameService.OnIngredientAdded(ingredientType);
        }
        else
        {
            transform.position = _startPosition;
        }
    }

    private void InputService_OnClickRaycastHit(object sender, RaycastHit2D e)
    {
        if (!_isMinigame) return;
        if (e.transform != transform || _isHeld) return;
        _isHeld = true;
    }

    private void InputService_OnSlideRaycastHit(object sender, RaycastHit2D e)
    {
        if (!_isMinigame) return;
        if (e.transform != transform || !_isHeld) return;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = touch.position;
            Vector2 worldPoint = _camera.ScreenToWorldPoint(touchPosition);
            transform.position = worldPoint;
        }
    }

    private void MinigameService_OnMinigameOver(object sender, MinigameType e)
    {
        if (e != MinigameType.Cauldron)
            return;
        _isMinigame = false;
        transform.position = _startPosition;
        gameObject.SetActive(true);
    }


    
    
    private void MinigameService_OnMinigameRequested(object sender, MinigameType e)
    {
        if (e != MinigameType.Cauldron)
            return;
        _startPosition = (Vector2)transform.position;
        _isMinigame = true;
    }


    private void OnDestroy()
    {
        _minigameService.OnMinigameRequested -= MinigameService_OnMinigameRequested;
        _minigameService.OnMinigameOver -= MinigameService_OnMinigameOver;
        _inputService.onSlideRaycastHit -= InputService_OnSlideRaycastHit;
        _inputService.onClickRaycastHit -= InputService_OnClickRaycastHit;
        _inputService.onEndTouchRaycastHit -= InputService_OnEndTouchRaycastHit;
    }
}
