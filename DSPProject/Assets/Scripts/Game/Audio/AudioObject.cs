using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Audio Object", menuName = "Assets/Audio/DSP Voice Recordings")]
public class AudioObject : ScriptableObject
{
    public AudioClip clip;
    public string subtitle;
}
