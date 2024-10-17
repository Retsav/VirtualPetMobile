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
    
    private IMinigameService _minigameService;
    private Sequence sequence;

    [Inject]
    private void ResolveDependecies(IMinigameService minigameService)
    {
        _minigameService = minigameService;
    }

    private void Start()
    {
        _minigameService.OnMinigameRequested += MinigameService_OnMinigameRequested;
        StartMinigame();
        sequence = DOTween.Sequence();
    }

    private void MinigameService_OnMinigameRequested(object sender, MinigameType e)
    {
        if (e != MinigameType.ThreeCups && !_minigameService.IsInMinigame)
            return;
        StartMinigame();
    }

    private void StartMinigame()
    {
        cupContainer.DOMoveY(cat.localPosition.y, .8f).OnComplete(() =>
        {
            cat.transform.SetParent(cupsList[0].transform);
            CalculateNextMove();
        });
    }

    private void CalculateNextMove()
    {
        
        int swapsCount = Random.Range(1, 5);
        sequence?.Kill();
        sequence = DOTween.Sequence();
        for (int i = 0; i < swapsCount; i++)
        {
            var cups = GetRandomCups();
            ShuffleType type = GetRandomShuffleType();
            switch (type)
            {
                case ShuffleType.Normal:
                    var firstCupPosition = cups[0].position;
                    sequence.Append(cups[0].DOMoveX(cups[1].position.x, .3f));
                    sequence.Join(cups[1].DOMoveX(firstCupPosition.x, .3f));
                    break;
            }
            sequence.AppendInterval(0.5f);
        }
    }

    private ShuffleType GetRandomShuffleType()
    {
        var maxSize = Enum.GetValues(typeof(ShuffleType)).Length;
        var randomEnum = Random.Range(0, maxSize);
        //return (ShuffleType)randomEnum;
        return ShuffleType.Normal;
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
        _minigameService.OnMinigameRequested -= MinigameService_OnMinigameRequested;
    }
    
    
    public enum ShuffleType
    {
        None,
        Normal,
        Upwards,
    }
}


