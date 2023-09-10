using UnityEngine;

public class ButtonPressedVisual : MonoBehaviour
{
    [SerializeField] private Pitch pitch;
    private void Start()
    {
        GameInput.Instance.OnPlayPitch += GameInput_OnPlayPitch;
        GameInput.Instance.OnReleasePitch += GameInput_OnReleasePitch;
        Hide();
    }

    private void GameInput_OnReleasePitch(object sender, OnPitchToggleEventArgs e)
    {
        if (pitch == e.PitchToggle)
        {
            Hide();
        }
    }

    private void GameInput_OnPlayPitch(object sender, OnPitchToggleEventArgs e)
    {
        if (pitch == e.PitchToggle)
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
