using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private float roomSpacing = 0f;
    
    private IRoomService _roomService;
    
    [Inject]
    private void ResolveDependencies(IRoomService roomService)
    {
        _roomService = roomService;
    }
    
    private void Start() => SetupRooms();

    private void SetupRooms()
    {
        var rooms = _roomService.GetRooms();
        if (rooms == null || rooms.Count == 0)
        {
            Debug.LogError($"Critical Error: No rooms in the list or list is null");
            return;
        }
        float roomWidth = _roomService.GetRoomWidth();
        for (int i = 0; i < rooms.Count; i++)
        {
            Vector3 newPosition = new Vector3(i * (roomWidth + roomSpacing), 0, 0);
            rooms[i].transform.position = newPosition;
            rooms[i].SetRoomIndex(i);
        }
        _roomService.SwitchRooms(0);
    }
    

}
