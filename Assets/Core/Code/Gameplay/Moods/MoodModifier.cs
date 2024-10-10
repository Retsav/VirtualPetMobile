using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodModifier
{
    public float value;
    public bool isPersistent;
    public bool stayBuffered;
    public bool resolved;

    public MoodModifier(float value, bool isPersistent, bool stayBuffered = false)
    {
        this.value = value;
        this.isPersistent = isPersistent;
        this.stayBuffered = stayBuffered;
    }
}
