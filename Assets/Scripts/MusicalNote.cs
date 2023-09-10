using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MusicalNote : MonoBehaviour
{
    private const float DeadZoneX = -4.5f;
    
    private bool _canBePress;

    public event EventHandler<OnNoteInstantiateEventArgs> OnInstantiate;
    
    public static event EventHandler<OnNoteHitEventArgs> OnNoteHit;
    public static event EventHandler<OnNoteMissedEventArgs> OnNoteMissed;
    
    public Note Note { get; private set; }

    private void Awake()
    {
        SetMusicalNoteRandomly();
    }

    private void Start()
    {
        OnInstantiate?.Invoke(this, new OnNoteInstantiateEventArgs() {Note = Note});
        GameInput.Instance.OnPlayPitch += GameInput_OnPlayPitch;
    }
    
    private void Update()
    {
        ScrollNoteByBeat();
    }

    private void GameInput_OnPlayPitch(object sender, OnPitchToggleEventArgs e)
    {
        if (_canBePress && GetNotePitch() == e.PitchToggle && this)
        {
            OnNoteHit?.Invoke(this, new OnNoteHitEventArgs { MusicalNote = this });
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        _canBePress = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (transform.position.x < DeadZoneX)
        {
            _canBePress = false;

            OnNoteMissed?.Invoke(this, new OnNoteMissedEventArgs { MusicalNote = this });
        }
    }

    private void ScrollNoteByBeat()
    {
        float offset = 1f;
        float distance = GameAssets.Instance.musicalNotePrefab.transform.position.x -
                    GameHandler.Instance.GetPlayPositionObject().position.x + offset;
        
        float speed = distance / GameHandler.Instance.TimeBetween8Beat;
        
        transform.position += Vector3.left * (speed * Time.deltaTime);
    }

    private void SetMusicalNoteRandomly()
    {
        int numberOfNotes = Enum.GetNames(typeof(Note)).Length;
        int noteNumber = Random.Range(0, numberOfNotes);
        Note = (Note)noteNumber;
    }
    
    private Pitch GetNotePitch()
    {
        switch ((int)Note % 7)
        {
            case 0:
                return Pitch.G;
            case 1:
                return Pitch.A;
            case 2:
                return Pitch.B;
            case 3:
                return Pitch.C;
            case 4:
                return Pitch.D;
            case 5:
                return Pitch.E;
            case 6:
                return Pitch.F;
        }
        
        // the function never reaches here
        return Pitch.A;
    }
    
}
