using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

public class SFXManager : MonoBehaviour
{
    private static SFXManager _instance;
    public static SFXManager instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<SFXManager>();

            return _instance;
        }
    }

    [HorizontalGroup("AudioSource")]
    [SerializeField]
    private AudioSource defaultAudioSource;

    [TabGroup("UI")]
    [AssetList(Path = "/Audio/UI SFX", AutoPopulate = true)]
    public List<SFXClip> uiSFX;
    [TabGroup("Ambient")]
    [AssetList(Path = "/Audio/Ambient SFX", AutoPopulate = true)]
    public List<SFXClip> ambientSFX;
    [TabGroup("Weapons")]
    [AssetList(Path = "/Audio/Weapon SFX", AutoPopulate = true)]
    public List<SFXClip> weaponSFX;

    public static void PlaySFX(SFXClip sfx, bool waitToFinish = true, AudioSource audioSource = null)
    {
        if (audioSource == null)
            audioSource = SFXManager.instance.defaultAudioSource;

        if (audioSource == null)
        {
            Debug.LogError("You forgot to add a default audio source!");
            return;
        }

        if (!audioSource.isPlaying || !waitToFinish)
        {
            audioSource.clip = sfx.clip;
            audioSource.volume = sfx.volume + Random.Range(-sfx.volumeVariation, sfx.volumeVariation);
            audioSource.pitch = sfx.pitch + Random.Range(-sfx.pitchVariation, sfx.pitchVariation);
            audioSource.Play();
        }
    }

    [HorizontalGroup("AudioSource")]
    [ShowIf("@defaultAudioSource == null")]
    [GUIColor(1f,0.5f,0.5f,1f)]
    [Button]
    private void AddAudioSource()
    {
        defaultAudioSource = this.gameObject.GetComponent<AudioSource>();

        if (defaultAudioSource == null)
            defaultAudioSource = this.gameObject.AddComponent<AudioSource>();
    }

    public enum SFXType
    {
        UI,
        Ambient,
        Weapons
    }
}





