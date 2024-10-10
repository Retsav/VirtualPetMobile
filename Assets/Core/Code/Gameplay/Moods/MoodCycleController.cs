using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MoodCycleController : MonoBehaviour
{
    private IMoodService _moodService;

    [Inject]
    private void ResolveDependencies(IMoodService moodService)
    {
        _moodService = moodService;
    }

    private void Start()
    {
        _moodService.AddMoodModifier(MoodTypeEnum.Hunger, new MoodModifier(-1, true, true));
        _moodService.AddMoodModifier(MoodTypeEnum.Klin, new MoodModifier(-1, true, true));
        _moodService.AddMoodModifier(MoodTypeEnum.Happiness, new MoodModifier(-1, true, true));
        _moodService.AddMoodModifier(MoodTypeEnum.Sleep, new MoodModifier(-1, true, true));
    }
}
