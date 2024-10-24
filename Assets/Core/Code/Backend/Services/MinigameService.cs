using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameService : IMinigameService
{
    public bool IsInMinigame { get; set; }
    public event EventHandler<MinigameType> OnMinigameRequested;
    public event EventHandler<MinigamePointsUpdatedEventArgs> OnMinigamePointsUpdated;
    public event EventHandler<MinigameType> OnMinigameOver;
    public void RequestMinigame(MinigameType minigameType) => OnMinigameRequested?.Invoke(this, minigameType);
    public void SetInMinigame(bool isInMinigame) => IsInMinigame = isInMinigame;
    public void OnPointsUpdated(MinigamePointsUpdatedEventArgs args) => OnMinigamePointsUpdated?.Invoke(this, args);
    public void OnGameOver(MinigameType minigameType) => OnMinigameOver?.Invoke(this, minigameType);
}
