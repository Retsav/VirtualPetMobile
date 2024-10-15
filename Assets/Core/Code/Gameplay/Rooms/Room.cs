using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Room : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _backgroundSprite;
    private int _roomIndex = -1;

    protected IRoomService _roomService;
    
    [Inject]
    private void Construct(IRoomService roomService)
    {
        _roomService = roomService;
        _roomService.RegisterRoom(this);
    }

    public float GetRoomWidth() => _backgroundSprite.bounds.size.x;
    public void SetRoomIndex(int index) => _roomIndex = index;
    public int GetRoomIndex() => _roomIndex;
}