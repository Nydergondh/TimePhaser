using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAudios : MonoBehaviour
{
    public static SoundAudios soundAudios;

    public  SoundAudioClip[] soundAudioClipArray;

    void Awake() {
        if (soundAudios != null) {
            Destroy(this);
            return;
        }
        soundAudios = this;
    }

    [System.Serializable]
    public class SoundAudioClip {
        public Sound sound;
        public AudioClip audioClip;
    }

    public enum Sound {
        Punch,
        EnemyHurt,
        EnemyAttack,
        EnemyGun,
        EnemyFire,
        ProjectileCollide,
        Hurt,
        Death,
        Smash,
        Door,
        BossRoar,
        BossScream,
        BossExplode,
    }
}
