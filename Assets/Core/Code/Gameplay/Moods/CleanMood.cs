using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CleanMood : BaseMood
{
    private IMoodService _moodService;
    private IPetService _petService;

    [SerializeField] private float _dirtThreshold;
    private bool isDirty;

    [Inject]
    private void ResolveDependencies(IMoodService moodService, IPetService petService)
    {
        _moodService = moodService;
        _petService = petService;
    }

    private void OnMoodModifierAdded(object sender, OnMoodModifierAddedEventArgs args)
    {
        if (args.MoodType != MoodType)
            return;
        ModifiersBuffer.Add(args.Modifier);
        RecalculateStaticModifiers();
        if (!(Value > _dirtThreshold) || !isDirty) return;
        isDirty = false;
        _petService.SetDirty(false);
    }

    private void OnMoodModifierRemoved(object sender, OnMoodModifierRemovedEventArgs args)
    {
        if (!ModifiersBuffer.Contains(args.Modifier))
        {
            Debug.LogWarning("Modifier not registerd");
            return;
        } 
        ModifiersBuffer.Remove(args.Modifier);
        RecalculateStaticModifiers();
    }

    public override void RecalculatePersistentModifiers()
    {
        base.RecalculatePersistentModifiers();
        if (!(Value <= _dirtThreshold) || isDirty) return;
        isDirty = true;
        _petService.SetDirty(true);
    }

    protected override void InitializeMood()
    {
        ModifiersBuffer = new HashSet<MoodModifier>();
        _moodService.RegisterMood(this);
        MoodType = MoodTypeEnum.Klin;
        MaxValue = 100f;
        Value = MaxValue;
        _moodService.MoodModifierAddedEventHandler += OnMoodModifierAdded;
        _moodService.MoodModifierRemovedEventHandler += OnMoodModifierRemoved;
    }
    

    private void OnDestroy()
    {
        _moodService.MoodModifierAddedEventHandler -= OnMoodModifierAdded;
        _moodService.MoodModifierRemovedEventHandler -= OnMoodModifierRemoved;
    }
}
