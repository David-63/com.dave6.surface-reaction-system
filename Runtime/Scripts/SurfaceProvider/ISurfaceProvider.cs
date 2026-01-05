using System.Collections.Generic;

namespace Dave6.SurfaceReactionSystem
{
    public interface ISurfaceProvider
    {
        bool TryGetSurfaces(in ImpactContext context, List<SurfaceHit> results);
    }
}