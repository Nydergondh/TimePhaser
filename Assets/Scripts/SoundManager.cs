using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    //returns a clip corresponding to the kind of sound (enum) you want to play
    public static AudioClip GetSound(SoundAudios.Sound sound) {
        foreach (SoundAudios.SoundAudioClip audio in SoundAudios.soundAudios.soundAudioClipArray) {
            if (audio.sound == sound) {
                return audio.audioClip;
            }
        }
        Debug.Log("ERROR");
        return null;
    }

}
