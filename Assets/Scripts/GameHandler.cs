using System;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance { get; private set; }
    
    public int Score { get; private set; }
    public int ComboNoteHit { get; private set; }
    public int NumberOfNoteHit { get; private set; }
    public int NumberOfNoteMissed { get; private set; }
    public float TimeBetween8Beat { get; private set; }

    public event EventHandler<OnTempoIncreaseEventArgs> OnTempoIncrease;

    [SerializeField] private int tempo;
    [SerializeField] private int[] multiplierThreshold;
    [SerializeField] private Transform playPositionObject;
    
    private const int TempoIncreaseAmount = 20;
    private const int TempoIncreaseThreshold = 4;
    private const int ScorePerNoteHit = 100;
    private const int DefaultScoreMultiplier = 1;
    private const int GameOverThreshold = 5;

    private List<MusicalNote> MusicalNoteList;

    private int _currentScoreMultiplier = DefaultScoreMultiplier;
    private int _numberOfNoteHitContinuously;
    private int _numberOfNoteMissedContinuously;
    private int _multiplyTracker;
    private float _spawnNoteTimer;
    
    protected GameHandler() {} // protected constructor
    
    private void Awake()
    {
        Instance = this;
        MusicalNoteList = new List<MusicalNote>();

        SetTimeBetween8Beat();
    }

    private void Start()
    {
        MusicalNote.OnNoteHit += MusicalNote_OnNoteHit;
        MusicalNote.OnNoteMissed += MusicalNote_OnNoteMissed;
        
        // InvokeRepeating(nameof(CreateNote), 0, TimeBetween8Beat);
    }

    private void Update()
    {
        _spawnNoteTimer -= Time.deltaTime;

        if (_spawnNoteTimer <= 0f)
        {
            _spawnNoteTimer = TimeBetween8Beat;
            CreateNote();
        }
    }

    private void MusicalNote_OnNoteHit(object sender, OnNoteHitEventArgs e)
    {
        Score += ScorePerNoteHit * _currentScoreMultiplier;
        
        NumberOfNoteHit++;
        _numberOfNoteHitContinuously++;
        ComboNoteHit++;
        
        _numberOfNoteMissedContinuously = 0;

        // Update Tempo
        if (_numberOfNoteHitContinuously == TempoIncreaseThreshold)
        {
            tempo += TempoIncreaseAmount;
            _numberOfNoteHitContinuously = 0;

            SetTimeBetween8Beat();
            OnTempoIncrease?.Invoke(this, new OnTempoIncreaseEventArgs {IncreaseAmount = TempoIncreaseAmount});
        }
        
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
        
        for (int i = 0; i < MusicalNoteList.Count; i++)
        {
            if (MusicalNoteList[i] == e.MusicalNote)
            {
                MusicalNoteList.Remove(e.MusicalNote);
                DestroyNote(e.MusicalNote);
                i--;
            }
        }
    }
    
    private void MusicalNote_OnNoteMissed(object sender, OnNoteMissedEventArgs e)
    {
        _currentScoreMultiplier = DefaultScoreMultiplier;
        
        NumberOfNoteMissed++;
        _numberOfNoteMissedContinuously++;
        
        _numberOfNoteHitContinuously = 0;
        ComboNoteHit = 0;

        if (_numberOfNoteMissedContinuously >= GameOverThreshold)
        {
            // game over
            Time.timeScale = 0;
        }
        
        for (int i = 0; i < MusicalNoteList.Count; i++)
        {
            if (MusicalNoteList[i] == e.MusicalNote)
            {
                MusicalNoteList.Remove(e.MusicalNote);
                DestroyNote(e.MusicalNote);
                i--;
            }
        }
    }
    
    private void CreateNote()
    {
        MusicalNote musicalNote = Instantiate(GameAssets.Instance.musicalNotePrefab);
        MusicalNoteList.Add(musicalNote);
    }

    private void DestroyNote(MusicalNote musicalNote)
    {
        MusicalNoteList.Remove(musicalNote);
        Destroy(musicalNote.gameObject);
    }

    private void SetTimeBetween8Beat()
    {
        TimeBetween8Beat = 480f / tempo;
    }

    public int GetTempo()
    {
        return tempo;
    }
    
    public Transform GetPlayPositionObject()
    {
        return playPositionObject;
    }
}
