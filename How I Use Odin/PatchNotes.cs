using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Patch Notes", menuName = "Create New Patch Notes")]
public class PatchNotes : SerializedScriptableObject
{
    [SerializeField]
    public Dictionary<float, NoteContainer> patchNotes = new Dictionary<float, NoteContainer>();

    [Title("Add New Notes")]
    [SerializeField]
    [OnValueChanged("LoadNotesToEdit")]
    private float version;
    [TextArea(10, 20)]
    [SerializeField]
    [OnValueChanged("AddNotes")]
    private string notes;
    [SerializeField]
    [TextArea(5, 10)]
    public string knownIssues;

    public NoteContainer GetNotes(float version)
    {
        patchNotes.TryGetValue(version, out NoteContainer notes);
        return notes;
    }
    public void SaveNotes(float version, string note)
    {
        patchNotes.TryGetValue(version, out NoteContainer notes);
        if (notes == null)
            patchNotes.Add(version, new NoteContainer(version, note));
        else
            notes.notes = note;
    }

    [GUIColor(0.6f, 1f, 0.6f)]
    [ButtonGroup("Buttons")]
    private void AddNotes()
    {
        SaveNotes(version, notes);
    }

    [GUIColor(1f, 0.6f, 0.6f)]
    [ButtonGroup("Buttons")]
    private void ResetRead()
    {
        foreach (KeyValuePair<float, NoteContainer> note in patchNotes)
            ES3.Save<bool>(note.Key.ToString() + " has been read", false, "patchnotes.es3");
    }
    public void SetLatestAsRead()
    {
        ES3.Save<bool>(GetLatestVersion().ToString() + " has been read", true, "patchnotes.es3");
    }
    public bool IsLatestRead()
    {
        if (!ES3.FileExists("patchnotes.es3"))
            return false;

        return ES3.Load<bool>(GetLatestVersion().ToString() + " has been read", "patchnotes.es3");
    }
    private void LoadNotesToEdit()
    {
        if (patchNotes.TryGetValue(version, out NoteContainer notes))
            this.notes = notes.notes;
    }
    public float GetLatestVersion()
    {
        float latestVersion = 0f;

        foreach (KeyValuePair<float, NoteContainer> note in patchNotes)
        {
            if (note.Key > latestVersion)
                latestVersion = note.Key;
        }

        return latestVersion;
    }
    public ReadOnlyCollection<NoteContainer> GetAllNotes(bool newestFirst = true)
    {
        List<NoteContainer> notes = new List<NoteContainer>();
        foreach (var version in patchNotes.Keys)
        {
            notes.Add(GetNotes(version));
        }

        if (newestFirst)
            notes = notes.OrderByDescending(x => x.version).ToList();

        return notes.AsReadOnly();
    }
    [SerializeField]
    public class NoteContainer
    {
        public NoteContainer(float version, string notes)
        {
            this.version = version;
            this.notes = notes;
        }

        public float version;
        [TextArea]
        public string notes;
    }
}
