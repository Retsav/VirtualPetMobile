using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputService
{
    public event EventHandler<RaycastHit2D> onSlideRaycastHit;
    public event EventHandler<RaycastHit2D> onClickRaycastHit;
    public void OnSlideOnCollision(RaycastHit2D args);
    public void OnClickOnCollision(RaycastHit2D args);
    
}


