using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameService : IMinigameService
{
    public bool IsInMinigame { get; set; }
    public event EventHandler<MinigameType> OnMinigameRequested;
    public void RequestMinigame(MinigameType minigameType) => OnMinigameRequested?.Invoke(this, minigameType);
    public void SetInMinigame(bool isInMinigame) => IsInMinigame = isInMinigame;
}
