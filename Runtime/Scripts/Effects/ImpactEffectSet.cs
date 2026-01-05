using System.Collections.Generic;
using UnityEngine;

namespace Dave6.SurfaceReactionSystem
{
    [CreateAssetMenu(fileName = "ImpactEffectSet", menuName = "DaveAssets/SurfaceReactionSystem/Effects/Impact EffectSet")]
    public class ImpactEffectSet : ScriptableObject
    {
        // public List<SpawnVfx> spawnVfxs = new();
        // public List<PlaySfx> playSfxs = new();

        [SerializeField] List<ScriptableObject> effects = new();

        public void PlayAll(in ImpactEffectContext context)
        {
            foreach (var effect in effects)
            {
                (effect as IImpactEffect)?.Play(context);                
            }
        }
    }
}