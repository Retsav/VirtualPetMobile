using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRoomService
{
    public void RegisterRoom(Room room);
    public float GetRoomWidth();
    public List<Room> GetRooms();
    public int GetCurrentRoomIndex();
    public Room GetCurrentRoom();
    public void ClearRooms();
    public void SwitchRooms(int index);
    
    public event EventHandler<OnRoomChangedEventArgs> RoomChangedEvent;
    public void OnRoomChanged(Room roomType);
}
