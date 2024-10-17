using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputService
{
    public event EventHandler<OnSlideRaycastHitEventArgs> onSlideRaycastHit;
    public void OnSlideOnCollision(OnSlideRaycastHitEventArgs args);
}

public class OnSlideRaycastHitEventArgs : EventArgs
{
    public RaycastHit2D hit;

    public OnSlideRaycastHitEventArgs(RaycastHit2D hit)
    {
        this.hit = hit;
    }
}
