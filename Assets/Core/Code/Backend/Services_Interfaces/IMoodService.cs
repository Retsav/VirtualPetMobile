using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoodService
{
    public event EventHandler<OnMoodModifierAddedEventArgs> MoodModifierAddedEventHandler;
    public event EventHandler<OnMoodModifierRemovedEventArgs> MoodModifierRemovedEventHandler;
    
    public void RegisterMood(BaseMood baseMood);
    public void UnregisterMood(BaseMood baseMood);
    public void AddMoodModifier(MoodTypeEnum moodType, MoodModifier modifier);
    public void RemoveMoodModifier(MoodTypeEnum moodType, MoodModifier modifier);
    public BaseMood GetMood(MoodTypeEnum moodType);
}


