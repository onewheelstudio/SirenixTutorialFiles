using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlaySFXExample : MonoBehaviour
{
    public SFX sfxToPlay;

    private void Start()
    {
        sfxToPlay.PlaySFX();
    }
}
