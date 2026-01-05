using UnityEngine;

namespace Dave6.SurfaceReactionSystem
{
    /// <summary>
    /// 물리적 사실, 충돌 정보만 가지고 있어야함
    /// </summary>
    public struct ImpactContext
    {
        public readonly GameObject hitObject;
        public readonly Vector3 position;
        public readonly Vector3 normal;

        public readonly ImpactType impactType;
        
        public ImpactContext(GameObject hitObject, Vector3 position, Vector3 normal, ImpactType impactType)
        {
            this.hitObject = hitObject;
            this.position = position;
            this.normal = normal;
            this.impactType = impactType;
        }
    }
}