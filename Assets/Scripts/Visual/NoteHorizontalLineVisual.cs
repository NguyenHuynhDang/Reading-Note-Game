using UnityEngine;

public class NoteHorizontalLineVisual : MonoBehaviour
{
    public GameObject fistLine;
    public GameObject secondLine;
    public GameObject thirdLine;
    public GameObject forthLine;

    public void ResetLines()
    {
        fistLine.SetActive(false);
        secondLine.SetActive(false);
        thirdLine.SetActive(false);
        forthLine.SetActive(false);
    }
}
