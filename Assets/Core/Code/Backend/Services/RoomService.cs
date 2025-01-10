using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomService : IRoomService
{
    private readonly List<Room> _rooms = new();
    private Room _currentActiveRoom;
    
    public void RegisterRoom(Room room)
    {
        _rooms.Add(room);
    }

    public float GetRoomWidth()
    {
        if (_rooms == null || _rooms.Count == 0)
        {
            Debug.LogError($"Critical Error: No rooms in the list or list is null");
            return 0;
        }
        return _rooms[0].GetRoomWidth();
    }

    public List<Room> GetRooms() => _rooms;
    public int GetCurrentRoomIndex() => _currentActiveRoom.GetRoomIndex();
    public Room GetCurrentRoom() => _currentActiveRoom;
    public void ClearRooms() => _rooms.Clear();

    public void SwitchRooms(int index)
    {
        int j = -1;
        for (int i = 0; i < _rooms.Count; i++)
        {
            if (_rooms[i].GetRoomIndex() == index)
            {
                j = i;
                break;
            }
        }
        if (j == -1)
        {
            Debug.LogError($"Critical Error: Room with index {index} not found");
            return;
        }
        _currentActiveRoom = _rooms[j];
        OnRoomChanged(_currentActiveRoom);
    }
    
    

    public event EventHandler<OnRoomChangedEventArgs> RoomChangedEvent;
    public void OnRoomChanged(Room roomType) => RoomChangedEvent?.Invoke(this, new OnRoomChangedEventArgs(roomType));
}

public class OnRoomChangedEventArgs : EventArgs
{
    public Room RoomType { get; }

    public OnRoomChangedEventArgs(Room roomType)
    {
        RoomType = roomType;
    }
}
