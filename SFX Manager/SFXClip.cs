using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "New SFX Clip", fileName = "NewSFXClip")]
public class SFXClip : ScriptableObject
{
    [Space]
    [Title("Audio Clip")]
    [Required]
    public AudioClip clip;

    [Title("Clip Settings")]
    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(0f, 0.2f)]
    public float volumeVariation = 0.05f;
    [Range(0f, 2f)]
    public float pitch = 1f;
    [Range(0f, 0.2f)]
    public float pitchVariation = 0.05f;
}
