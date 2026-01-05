using System;
using Dave6.ObjectPoolingSystem;
using UnityEngine;

namespace Dave6.SurfaceReactionSystem
{
    [CreateAssetMenu(fileName = "SpawnVfx", menuName = "DaveAssets/SurfaceReactionSystem/Effects/Spawn Vfx")]
    public class SpawnVfx : ScriptableObject, IImpactEffect
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float probability = 1f;

        public bool randomizeRotation;
        public Vector3 randomizedRotationMultiplier = Vector3.zero;

        public void Play(ImpactEffectContext context)
        {
            // context.position / normal 기반 스폰

            if (UnityEngine.Random.value > probability) return;

            Vector3 offset = Vector3.zero;

            if (randomizeRotation)
            {
                offset = new Vector3
                (
                    UnityEngine.Random.Range(0, 180 * randomizedRotationMultiplier.x),
                    UnityEngine.Random.Range(0, 180 * randomizedRotationMultiplier.y),
                    UnityEngine.Random.Range(0, 180 * randomizedRotationMultiplier.z)
                );
            }

            GameObject instance = ObjectPoolService.instance.Get(prefab);

            instance.transform.SetPositionAndRotation
            (
                context.position + context.normal * 0.001f,
                Quaternion.LookRotation(context.normal)
            );
        }
    }
}