using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class MoodUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private MoodTypeEnum moodType;

    private float maxValue;
    
    private BaseMood mood;
    

    private IMoodService _moodService;

    [Inject]
    private void ResolveDependencies(IMoodService moodService)
    {
        _moodService = moodService;
    }

    private void Start()
    {
        //TODO: Change when repository will have mood name or somethin'
        mood = _moodService.GetMood(moodType);
    }

    private void Update()
    {
        if (mood == null)
            return;
        fillImage.fillAmount = mood.Value / mood.MaxValue;
    }
}
