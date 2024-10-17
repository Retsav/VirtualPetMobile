using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputService : IInputService
{
    public event EventHandler<OnSlideRaycastHitEventArgs> onSlideRaycastHit;
    public void OnSlideOnCollision(OnSlideRaycastHitEventArgs args) => onSlideRaycastHit?.Invoke(this, args);
}
