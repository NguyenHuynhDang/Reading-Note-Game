using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource _audioSource;
    private int _baseTempo;
    
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _baseTempo = GameHandler.Instance.GetTempo();
        
        GameHandler.Instance.OnTempoIncrease += GameHandler_OnTempoIncrease;
    }

    private void GameHandler_OnTempoIncrease(object sender, OnTempoIncreaseEventArgs e)
    {
        float baseBps = _baseTempo / 60f;
        float bps = GameHandler.Instance.GetTempo() / 60f;
            
        float pitchAmount = bps / baseBps;
        
        _audioSource.pitch = pitchAmount;
    }
}
