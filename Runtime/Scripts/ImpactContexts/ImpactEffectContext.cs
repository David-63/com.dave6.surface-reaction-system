using UnityEngine;

namespace Dave6.SurfaceReactionSystem
{
    /// <summary>
    /// 이펙트 처리용 구조체
    /// </summary>
    public readonly struct ImpactEffectContext
    {
        public readonly Vector3 position;
        public readonly Vector3 normal;

        public ImpactEffectContext(Vector3 position, Vector3 normal)
        {
            this.position = position;
            this.normal = normal;
        }
    }
}