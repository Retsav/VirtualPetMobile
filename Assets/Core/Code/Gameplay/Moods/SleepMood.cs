using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SleepMood : BaseMood
{
    private IMoodService _moodService;

    [Inject]
    private void ResolveDependencies(IMoodService moodService)
    {
        _moodService = moodService;
    }

    private void OnMoodModifierAdded(object sender, OnMoodModifierAddedEventArgs args)
    {
        if (args.MoodType != MoodType)
            return;
        ModifiersBuffer.Add(args.Modifier);
        RecalculateStaticModifiers();
    }

    private void OnMoodModifierRemoved(object sender, OnMoodModifierAddedEventArgs args)
    {
        if (!ModifiersBuffer.Contains(args.Modifier))
        {
            Debug.LogWarning("Modifier not registerd");
            return;
        } 
        ModifiersBuffer.Remove(args.Modifier);
        RecalculateStaticModifiers();
    }

    protected override void InitializeMood()
    {
        ModifiersBuffer = new HashSet<MoodModifier>();
        _moodService.RegisterMood(this);
        MoodType = MoodTypeEnum.Sleep;
        MaxValue = 100f;
        Value = MaxValue;
        _moodService.MoodModifierAddedEventHandler += OnMoodModifierAdded;
    }
    

    private void OnDestroy()
    {
        _moodService.MoodModifierAddedEventHandler -= OnMoodModifierAdded;
    }
}