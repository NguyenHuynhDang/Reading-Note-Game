using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    
    public int Score { get; private set; }
    public int MaxCombo { get; private set; }
    public int NumberOfNoteHit { get; private set; }
    public int NumberOfNoteMissed { get; private set; }
    
    [SerializeField] private int[] multiplierThreshold;
    
    private const int ScorePerNoteHit = 100;
    private const int DefaultScoreMultiplier = 1;
    
    private int _comboNoteHit;
    private int _currentScoreMultiplier = DefaultScoreMultiplier;
    private int _multiplyTracker;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        MusicalNote.OnNoteHit += MusicalNote_OnNoteHit;
        MusicalNote.OnNoteMissed += MusicalNote_OnNoteMissed;
    }

    private void MusicalNote_OnNoteHit(object sender, OnNoteHitEventArgs e)
    {
        Score += ScorePerNoteHit * _currentScoreMultiplier;
        
        NumberOfNoteHit++;
        _comboNoteHit++;
        
        // Update Multiplier
        if (_currentScoreMultiplier - 1 < multiplierThreshold.Length)
        {
            _multiplyTracker++;
            if (multiplierThreshold[_currentScoreMultiplier - 1] <= _multiplyTracker)
            {
                _multiplyTracker = 0;
                _currentScoreMultiplier++;
            }
        }
    }

    private void MusicalNote_OnNoteMissed(object sender, OnNoteMissedEventArgs e)
    {
        _currentScoreMultiplier = DefaultScoreMultiplier;
        
        NumberOfNoteMissed++;
        
        MaxCombo = Math.Max(MaxCombo, _comboNoteHit);
        _comboNoteHit = 0;
    }
}
