using System;

public class OnPitchToggleEventArgs : EventArgs
{
    public Pitch PitchToggle;
}

public class OnNoteInstantiateEventArgs : EventArgs
{
    public Note Note;
}

public class OnNoteHitEventArgs : EventArgs
{
    public MusicalNote MusicalNote;
}

public class OnNoteMissedEventArgs : EventArgs
{
    public MusicalNote MusicalNote;
}

public class OnTempoIncreaseEventArgs : EventArgs
{
    public int IncreaseAmount;
}