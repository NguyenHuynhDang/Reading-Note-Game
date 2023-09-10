using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets Instance { get; private set; }
    
    public MusicalNote musicalNotePrefab;

    protected GameAssets() {} // protected constructor
    
    private void Awake()
    {
        Instance = this;
    }
}
