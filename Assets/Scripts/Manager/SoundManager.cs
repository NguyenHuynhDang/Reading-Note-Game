using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] noteHitAudioClips;
    [SerializeField] private AudioClip noteMissedAudioClip;
    private void Start()
    {
        MusicalNote.OnNoteHit += MusicalNote_OnNoteHit;
        MusicalNote.OnNoteMissed += MusicalNoteOn_NoteMissed;
    }
    
    private void MusicalNote_OnNoteHit(object sender, OnNoteHitEventArgs e)
    {
        if (Camera.main != null) 
            PlaySound(noteHitAudioClips[(int)e.MusicalNote.Note], Camera.main.transform.position);
    }

    private void MusicalNoteOn_NoteMissed(object sender, OnNoteMissedEventArgs e)
    {
        if (Camera.main != null) 
            PlaySound(noteMissedAudioClip, Camera.main.transform.position, 0.2f);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
}
