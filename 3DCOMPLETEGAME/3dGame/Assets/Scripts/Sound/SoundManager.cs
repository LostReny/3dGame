using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<MusicSetup> musicSetups;
    public List<SFXSetup> sFXSetups;
}

public enum MusicType
{
    TYPE_01,
    TYPE_02,
    TYPE_03
}

[System.Serializable]
public class MusicSetup
{
    public MusicType musicType;
    public AudioClip audioClip;
}

public enum SFXType
{
    AUDIO_TYPE_01,
    AUDIO_TYPE_02,
    AUDIO_TYPE_03
}

[System.Serializable]
public class SFXSetup
{
    public SFXType sfxType;
    public AudioClip sfxAudioClip;
}
