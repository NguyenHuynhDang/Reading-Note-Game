using UnityEngine;

public class MusicalNoteVisual : MonoBehaviour
{
    private const float NoteDistance = 0.375f;
    private const float G4Position = 1.625f;
    private const float B5PositionFlip = 0.625f;

    [SerializeField] private MusicalNote musicalNote;
    [SerializeField] private NoteHorizontalLineVisual horizontalLines;
    
    private void Awake()
    {
        musicalNote.OnInstantiate += MusicalNote_OnInstantiate;
    }

    private void MusicalNote_OnInstantiate(object sender, OnNoteInstantiateEventArgs e)
    {
        SetNotePosition(e.Note);
        SetNoteHorizontalLines(e.Note);
    }

    private void SetNotePosition(Note currentNote)
    {
        float basePosition = currentNote >= Note.B5 ? B5PositionFlip : G4Position;
        Note baseNote = currentNote >= Note.B5 ? Note.B5 : Note.G4;
        
        float yPosition = basePosition + NoteDistance * ((int)currentNote - (int)baseNote);
        musicalNote.transform.position = new Vector3(transform.position.x, yPosition, 0);
    }

    private void SetNoteHorizontalLines(Note currentNote)
    {
        if (currentNote >= Note.B5)
        {
            musicalNote.transform.localScale = new Vector3(-1, -1, 0); // flip note
        }
        else
        {
            musicalNote.transform.localScale = new Vector3(1, 1, 0); // flip note
        }
        
        // Activate horizontal lines
        switch (currentNote)
        {
            case Note.A6:
            case Note.C4:
                horizontalLines.fistLine.SetActive(true);
                break;
            case Note.B6:
            case Note.B4:
                horizontalLines.secondLine.SetActive(true);
                break;
            case Note.A4:
                horizontalLines.fistLine.SetActive(true);
                horizontalLines.thirdLine.SetActive(true);
                break;
            case Note.G3:
                horizontalLines.secondLine.SetActive(true);
                horizontalLines.forthLine.SetActive(true);
                break;
            default:
                horizontalLines.ResetLines();
                break;
        }
    }
}
