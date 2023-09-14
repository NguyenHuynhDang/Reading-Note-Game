using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }
   public GameState CurrentGameState { get; private set; }

   public float CountDownToStartTimer { get; private set; } = 3f;
   public event EventHandler OnStateChange;
   
   private float _waitingToStartTimer = 1f;

   private void Awake()
   {
      Instance = this;
      CurrentGameState = GameState.WaitingToStart;
   }

   private void Start()
   {
      NoteManager.Instance.OnGameOver += NoteManager_OnGameOver;
   }
   
   private void Update()
   {
      switch (CurrentGameState)
      {
         case GameState.WaitingToStart:
            _waitingToStartTimer -= Time.deltaTime;

            if (_waitingToStartTimer < 0f)
            {
               CurrentGameState = GameState.CountDownToStart;
               OnStateChange?.Invoke(this, EventArgs.Empty);
            }
            
            break;
         case GameState.CountDownToStart:
            CountDownToStartTimer -= Time.deltaTime;
            
            if (CountDownToStartTimer < 0f)
            {
               CurrentGameState = GameState.GamePlaying;
               OnStateChange?.Invoke(this, EventArgs.Empty);
            }
            
            break;
      }
   }
   
   private void NoteManager_OnGameOver(object sender, EventArgs e)
   {
      CurrentGameState = GameState.GameOver;
      OnStateChange?.Invoke(this, EventArgs.Empty);
   }
}
