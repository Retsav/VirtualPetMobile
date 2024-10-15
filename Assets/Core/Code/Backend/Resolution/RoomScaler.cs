using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScaler : MonoBehaviour
{
    private void Awake()
    {
        float cameraHeight = Camera.main.orthographicSize * 2;
        float cameraWidth = cameraHeight * Screen.width / Screen.height;


        transform.localScale = new Vector3(cameraWidth, cameraHeight, 1);
    }
}
