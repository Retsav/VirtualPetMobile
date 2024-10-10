using System;
using System.Collections.Generic;
using UnityEngine;

public class MoodService : IMoodService
{
    private HashSet<BaseMood> _moods = new HashSet<BaseMood>();
    public event EventHandler<OnMoodModifierAddedEventArgs> MoodModifierAddedEventHandler;
    public event EventHandler<OnMoodModifierRemovedEventArgs> MoodModifierRemovedEventHandler;
    
    
    public void RegisterMood(BaseMood baseMood)
    {
        if (_moods.Contains(baseMood))
            Debug.LogWarning($"{baseMood} is already registered");
        _moods.Add(baseMood);
        Debug.Log($"{baseMood} registered.");
    }

    public void UnregisterMood(BaseMood baseMood)
    {
        if (!_moods.Contains(baseMood))
            Debug.LogWarning($"{baseMood} not in mood hashset!");
        _moods.Remove(baseMood);
        Debug.Log($"{baseMood} unregistered.");
    }

    public void AddMoodModifier(MoodTypeEnum moodType, MoodModifier modifier) => MoodModifierAddedEventHandler?.Invoke(this, new OnMoodModifierAddedEventArgs(moodType, modifier));
    public void RemoveMoodModifier(MoodTypeEnum moodType, MoodModifier modifier) => MoodModifierRemovedEventHandler?.Invoke(this, new OnMoodModifierRemovedEventArgs(moodType, modifier));
    
    public BaseMood GetMood(MoodTypeEnum moodType)
    {
        foreach (var mood in _moods)
        {
            if(mood.MoodType != moodType) continue;
            return mood;
        }
        return null;
    }
}

public class OnMoodModifierAddedEventArgs : EventArgs
{
    public MoodTypeEnum MoodType { get; private set; }
    public MoodModifier Modifier { get; private set; }

    public OnMoodModifierAddedEventArgs(MoodTypeEnum moodType, MoodModifier modifier)
    {
        MoodType = moodType;
        Modifier = modifier;
    }
}

public class OnMoodModifierRemovedEventArgs : EventArgs
{
    public MoodTypeEnum MoodType { get; private set; }
    public MoodModifier Modifier { get; private set; }

    public OnMoodModifierRemovedEventArgs(MoodTypeEnum moodType, MoodModifier modifier)
    {
        MoodType = moodType;
        Modifier = modifier;
    }
}
