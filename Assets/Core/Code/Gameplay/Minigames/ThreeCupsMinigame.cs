using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class ThreeCupsMinigame : MonoBehaviour
{
    [SerializeField] private Transform cupContainer;
    [SerializeField] private List<Transform> cupsList;
    [SerializeField] private Transform cat;

    [SerializeField] private float shuffleSpeed = .3f;
    [SerializeField] private float shuffleInterval = .5f;

    [SerializeField] private float yOffset = 5f;
    private int points;


    private bool _isShuffling = false; 
    private Sequence sequence;
    private int shufflesCount;
    
    private IMinigameService _minigameService;
    private IInputService _inputService;

    [Inject]
    private void ResolveDependecies(IMinigameService minigameService, IInputService inputService)
    {
        _minigameService = minigameService;
        _inputService = inputService;
    }

    private void Start()
    {
        StartMinigame();
    }
    

    private void StartMinigame()
    {
        _inputService.onClickRaycastHit += InputService_OnClickRaycastHit;
        StartRound();
    }

    private void InputService_OnClickRaycastHit(object sender, RaycastHit2D hit)
    {

        if (_isShuffling)
        {
            return;
        }
        var isWin = false;
        if (hit.transform.childCount > 1)
        {
            points++;
            _minigameService.OnPointsUpdated(new MinigamePointsUpdatedEventArgs(MinigameType.ThreeCups, points));
            isWin = true;
        }
        cat.parent = null;
        sequence?.Kill();
        sequence = DOTween.Sequence();
        sequence.Append(cupContainer.DOMoveY(yOffset, .8f));
        sequence.AppendInterval(0.5f);
        sequence.AppendCallback(isWin ? StartRound : GameOver);
    }

    private void StartRound()
    {
        cupContainer.DOMoveY(cat.localPosition.y, .8f).OnComplete(() =>
        {
            cat.transform.SetParent(cupsList[0].transform);
            CalculateShuffles();
        });
    }

    private void GameOver()
    {
        _inputService.onClickRaycastHit -= InputService_OnClickRaycastHit;
        _minigameService.OnGameOver(MinigameType.ThreeCups);
        Debug.Log("YOU DIED.");
    }

    private void CalculateShuffles()
    {
        shufflesCount = Random.Range(1, 15);
        _isShuffling = true;
        CalculateNextMove();
    }

    private void CalculateNextMove()
    {
        if (shufflesCount <= 0)
        {
            _isShuffling = false;
            return;
        }
        sequence?.Kill();
        sequence = DOTween.Sequence();
        var cups = GetRandomCups();
        ShuffleType type = GetRandomShuffleType();
        var firstCupPosition = cups[0].position;
        var secondCupPosition = cups[1].position; 
        switch (type)
        { 
            case ShuffleType.Normal:
                sequence.Append(cups[0].DOMoveX(secondCupPosition.x, shuffleSpeed)); 
                sequence.Join(cups[1].DOMoveX(firstCupPosition.x, shuffleSpeed)); 
                break;
            case ShuffleType.Upwards:
                sequence.Append(cups[0].DOMoveY(yOffset, shuffleSpeed));
                sequence.Join(cups[1].DOMoveY(-yOffset, shuffleSpeed));
                sequence.Append(cups[0].DOMoveX(secondCupPosition.x, shuffleSpeed)); 
                sequence.Join(cups[1].DOMoveX(firstCupPosition.x, shuffleSpeed)); 
                sequence.Append(cups[0].DOMoveY(secondCupPosition.y, shuffleSpeed));
                sequence.Join(cups[1].DOMoveY(firstCupPosition.y, shuffleSpeed));
                break;
        }

        shufflesCount--;
        sequence.AppendInterval(shuffleInterval);
        sequence.AppendCallback(CalculateNextMove);
    }



    private ShuffleType GetRandomShuffleType()
    {
        var maxSize = Enum.GetValues(typeof(ShuffleType)).Length;
        var randomEnum = Random.Range(0, maxSize);
        return (ShuffleType)randomEnum;
    }

    private Transform[] GetRandomCups()
    {
        var selectedCups = new Transform[2];
        int firstIndex = Random.Range(0, cupsList.Count);
        int secondIndex = 0;
        do
        {
            secondIndex = Random.Range(0, cupsList.Count);
        } while (firstIndex == secondIndex);
        selectedCups[0] = cupsList[firstIndex];
        selectedCups[1] = cupsList[secondIndex];
        return selectedCups;
    }

    private void OnDestroy()
    {
        _inputService.onClickRaycastHit -= InputService_OnClickRaycastHit;
    }
    
    
    public enum ShuffleType
    {
        Normal,
        Upwards,
    }
}


