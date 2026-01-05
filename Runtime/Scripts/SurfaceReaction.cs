using UnityEngine;

namespace Dave6.SurfaceReactionSystem
{
    [CreateAssetMenu(fileName = "SurfaceReaction", menuName = "DaveAssets/SurfaceReactionSystem/Surface Reaction")]
    public class SurfaceReaction : ScriptableObject
    {
        public Surface surface;
        public ImpactType impactType;
        public ImpactEffectSet effectSet;
    }
}