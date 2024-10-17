using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class CleaningSystem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer dirtSpriteRenderer;
    [SerializeField] private Texture2D brushTexture;


    private bool canClean = false;



    private Texture2D orginalTexture; 
    private Vector2 uv = Vector2.zero;
    
    private IInputService _inputService;
    private IMinigameService _minigameService;
    private IMoodService _moodService;

    [Inject]
    private void ResolveDependencies(IInputService inputService, IMinigameService minigameService, IMoodService moodService)
    {
        _inputService = inputService;
        _minigameService = minigameService;
        _minigameService.OnMinigameRequested += MinigameService_OnMinigameRequested;
        _moodService = moodService;
        HideDirty();
    }

    private void MinigameService_OnMinigameRequested(object sender, MinigameType e)
    {
        if (e != MinigameType.Cleaning)
            return;
        ActivateMinigame();
    }

    private void Start() => SetupTextureCopy();

    public void ShowDirty()
    {
        dirtSpriteRenderer.DOFade(1f, .3f);
        canClean = true;
    }

    public void HideDirty()
    {
        dirtSpriteRenderer.DOFade(0f, .3f);
        canClean = false;
        ResetTexture();
    }

    public void ActivateMinigame()
    {
        if (!canClean)
            return;
        _minigameService.SetInMinigame(true);
        _inputService.onSlideRaycastHit += OnSlideRaycastHit;
    }

    public void FinishMinigame()
    {
        HideDirty();
        _inputService.onSlideRaycastHit -= OnSlideRaycastHit;
        _minigameService.SetInMinigame(false);
        _moodService.AddMoodModifier(MoodTypeEnum.Klin, new MoodModifier(100, false));
    }

    private void SetupTextureCopy()
    {
        Texture2D texture = dirtSpriteRenderer.sprite.texture;
        orginalTexture = new Texture2D(texture.width, texture.height, texture.format, false);
        orginalTexture.SetPixels(texture.GetPixels());
        orginalTexture.Apply();
    }

    private void OnSlideRaycastHit(object sender, OnSlideRaycastHitEventArgs args)
    {
        RaycastHit2D hit = args.hit;
        Texture2D spriteTexture = dirtSpriteRenderer.sprite.texture;
        Vector2 localPoint = hit.point - (Vector2)hit.collider.transform.position;
        uv.x = localPoint.x / dirtSpriteRenderer.bounds.size.x + .5f;
        uv.y = localPoint.y / dirtSpriteRenderer.bounds.size.y + .5f;
        int pixelX = Mathf.FloorToInt(uv.x * spriteTexture.width);
        int pixelY = Mathf.FloorToInt(uv.y * spriteTexture.height);
        ApplyBrush(pixelX, pixelY);
    }

    private void ApplyBrush(int pixelX, int pixelY)
    {
        Texture2D texture = dirtSpriteRenderer.sprite.texture;
        int brushWidth = brushTexture.width;
        int brushHeight = brushTexture.height;
        for (int x = 0; x < brushWidth; x++)
        {
            for (int y = 0; y < brushHeight; y++)
            {
                int targetX = pixelX - (brushWidth / 2) + x;
                int targetY = pixelY - (brushHeight / 2) + y;
                if (targetX >= 0 && targetX < texture.width && targetY >= 0 && targetY < texture.height)
                {
                    if (brushTexture.GetPixel(x, y).a == 0)
                        continue;
                    texture.SetPixel(targetX, targetY, Color.clear);
                }
            }
        }
        texture.Apply();
        RecalculateCleanPercent();
    }

    private void RecalculateCleanPercent()
    {
        Texture2D texture = dirtSpriteRenderer.sprite.texture;
        var pixels = texture.GetPixels();
        var pixelsCountMax = pixels.Length;
        if (pixelsCountMax == 0)
            return;
        var alfaPixels = 0;
        for (int i = 0; i < pixelsCountMax; i++)
        {
            if (pixels[i].a == 0)
                alfaPixels++;
        }
        
        float result = (float)alfaPixels / pixelsCountMax * 100f;
        
        //DEBUG
        if (result > 70)
        {
            FinishMinigame();
        }
        
        Debug.Log($"Cleaning: {result}%");
    }

    public void ResetTexture()
    {
        Texture2D texture = dirtSpriteRenderer.sprite.texture;
        texture.SetPixels(orginalTexture.GetPixels());
        texture.Apply();
    }

    private void OnDestroy()
    {
        ResetTexture();
        _inputService.onSlideRaycastHit -= OnSlideRaycastHit;
        _minigameService.OnMinigameRequested -= MinigameService_OnMinigameRequested;
    }
}
