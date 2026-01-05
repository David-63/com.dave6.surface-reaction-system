using System;
using System.Collections.Generic;
using Dave6.ObjectPoolingSystem;
using UnityEngine;

namespace Dave6.SurfaceReactionSystem
{
    [CreateAssetMenu(fileName = "PlaySfxs", menuName = "DaveAssets/SurfaceReactionSystem/Effects/Play Sfxs")]
    public class PlaySfx : ScriptableObject, IImpactEffect
    {
        [Header("Audio")]
        public AudioSource audioSourcePrefab;
        public List<AudioClip> audioClips;

        [Header("Randomization")]
        public Vector2 volumeRange = new Vector2(0.8f, 1f);
        public Vector2 pitchRange = new Vector2(0.8f, 1.2f);

        public void Play(ImpactEffectContext context)
        {
            if (audioClips.Count == 0) return;
            AudioClip clip = audioClips[UnityEngine.Random.Range(0, audioClips.Count)];

            GameObject instance = ObjectPoolService.instance.Get(audioSourcePrefab.gameObject);
            instance.transform.SetPositionAndRotation(context.position, Quaternion.identity);            

            var source = instance.GetComponent<AudioSource>();
            source.clip = clip;
            source.volume = UnityEngine.Random.Range(volumeRange.x, volumeRange.y);
            source.pitch = UnityEngine.Random.Range(pitchRange.x, pitchRange.y);
            source.spatialBlend = 1f; // 3D 고정
            source.Play();

            var autoRelease = instance.GetComponent<AutoReleaseAfterTime>();
            autoRelease.timer.Reset(clip.length / source.pitch);            
        }
    }
}