using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class BaseMood : MonoBehaviour
{
    public float Value { get; protected set; }
    public float MaxValue { get; protected set; }
    public MoodTypeEnum MoodType { get; protected set; } 
    protected HashSet<MoodModifier> ModifiersBuffer {get; set;}

    public virtual void Awake()
    {
        InitializeMood();
    }

    protected abstract void InitializeMood();

    public virtual void Update()
    {
        RecalculatePersistentModifiers();
    }

    public virtual void RecalculatePersistentModifiers()
    {
        foreach (var moodModifier in ModifiersBuffer)
            if(moodModifier.isPersistent) Value += moodModifier.value * Time.deltaTime;
    }

    public virtual void RecalculateStaticModifiers()
    {
        HashSet<MoodModifier> modifiersToRemove = new();
        foreach (var moodModifier in ModifiersBuffer)
        {
            if (moodModifier.resolved) continue;
            if (!moodModifier.isPersistent)
            {
                Value += moodModifier.value;
                moodModifier.resolved = true;
            }
            if(!moodModifier.stayBuffered) modifiersToRemove.Add(moodModifier);
        }
        foreach (var removableModifier in modifiersToRemove) ModifiersBuffer.Remove(removableModifier);
    }
}

public enum MoodTypeEnum
{
    None,
    Hunger,
    Sleep,
    Happiness,
    Klin
}
