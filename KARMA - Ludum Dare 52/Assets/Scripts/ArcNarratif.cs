using UnityEngine;

[CreateAssetMenu(fileName = "Nouvel ArcNarratif", menuName = "ArcNarratif")]
public class ArcNarratif : ScriptableObject
{
    public Dialogue[] dialogues;
    public string titre;
}
