using System.Collections.Generic;
using UnityEngine;

namespace Dave6.SurfaceReactionSystem
{
    public class TerrainSurfaceProvider : ISurfaceProvider
    {
        SurfaceReactionDatabase m_Database;

        public TerrainSurfaceProvider(SurfaceReactionDatabase database)
        {
            m_Database = database;
        }

        public bool TryGetSurfaces(in ImpactContext context, List<SurfaceHit> results)
        {
            if (!context.hitObject.TryGetComponent(out Terrain terrain)) return false;

            foreach (var hit in m_Database.GetSurfaceHitsFromTerrain(terrain, context.position))
            {
                results.Add(new SurfaceHit(hit.surface, hit.weight));
            }

            return true;
        }
    }
}