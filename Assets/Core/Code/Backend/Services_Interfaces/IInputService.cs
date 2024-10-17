using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputService
{
    public event EventHandler<RaycastHit2D> onSlideRaycastHit;
    public void OnSlideOnCollision(RaycastHit2D args);
}


