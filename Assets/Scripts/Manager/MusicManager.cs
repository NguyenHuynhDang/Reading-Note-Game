using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource _audioSource;
    private int _baseTempo;
    
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _baseTempo = NoteManager.Instance.GetTempo();
        
        NoteManager.Instance.OnTempoIncrease += GameHandler_OnTempoIncrease;
        GameManager.Instance.OnStateChange += GameManager_OnStateChange;
    }

    private void GameManager_OnStateChange(object sender, EventArgs e)
    {
        if (GameManager.Instance.CurrentGameState == GameState.GamePlaying)
        {
            _audioSource.Play();
        }
    }

    private void GameHandler_OnTempoIncrease(object sender, OnTempoIncreaseEventArgs e)
    {
        float baseBps = _baseTempo / 60f;
        float bps = NoteManager.Instance.GetTempo() / 60f;
            
        float pitchAmount = bps / baseBps;
        
        _audioSource.pitch = pitchAmount;
    }
}
