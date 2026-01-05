using System.Collections.Generic;
using UnityEngine;
using UnityUtils;

namespace Dave6.SurfaceReactionSystem
{
    public class SurfaceReactionService : SingletonTemplate<SurfaceReactionService>
    {
        [SerializeField] SurfaceReactionDatabase database;
        SurfaceReactionResolver m_Resolver;

        List<ISurfaceProvider> m_Providers = new();
        List<SurfaceHit> m_SurfaceHits = new();

        protected override void Awake()
        {
            base.Awake();
            m_Resolver = new SurfaceReactionResolver(database.reactions);

            m_Providers.Add(new RendererSurfaceProvider(database));
            m_Providers.Add(new TerrainSurfaceProvider(database));
        }

        public void ProcessImpact(in ImpactContext context)
        {
            m_SurfaceHits.Clear();

            foreach (var provider in m_Providers)
            {
                if (provider.TryGetSurfaces(context, m_SurfaceHits)) break;
            }

            if (m_SurfaceHits.Count == 0) m_SurfaceHits.Add(new SurfaceHit(database.defaultSurface, 1f));

            m_Resolver.Resolve(context, m_SurfaceHits);
        }
    }
}