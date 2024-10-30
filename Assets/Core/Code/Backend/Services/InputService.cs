using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputService : IInputService
{
    public event EventHandler<RaycastHit2D> onSlideRaycastHit;
    public event EventHandler<RaycastHit2D> onClickRaycastHit;
    public event EventHandler<RaycastHit2D> onEndTouchRaycastHit;
    public void OnSlideOnCollision(RaycastHit2D args) => onSlideRaycastHit?.Invoke(this, args);
    public void OnClickOnCollision(RaycastHit2D args) => onClickRaycastHit?.Invoke(this, args);
    public void OnEndTouchOnCollision(RaycastHit2D args) => onEndTouchRaycastHit?.Invoke(this, args);
}
