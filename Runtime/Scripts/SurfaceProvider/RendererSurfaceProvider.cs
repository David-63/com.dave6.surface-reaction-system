using System.Collections.Generic;
using UnityEngine;

namespace Dave6.SurfaceReactionSystem
{
    public class RendererSurfaceProvider : ISurfaceProvider
    {
        SurfaceReactionDatabase m_Database;

        public RendererSurfaceProvider(SurfaceReactionDatabase database)
        {
            m_Database = database;
        }

        public bool TryGetSurfaces(in ImpactContext context, List<SurfaceHit> results)
        {
            if (!context.hitObject.TryGetComponent(out Renderer renderer)) return false;

            Surface surface = m_Database.GetSurfaceFromRenderer(renderer);

            if (surface == null) return false;

            results.Add(new SurfaceHit(surface, 1f));

            return true;
        }
    }
}