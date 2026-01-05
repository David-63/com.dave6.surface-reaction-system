using System.Collections.Generic;

namespace Dave6.SurfaceReactionSystem
{
    public class SurfaceReactionResolver
    {
        Dictionary<(Surface, ImpactType), SurfaceReaction> lookup;

        readonly List<SurfaceReaction> reactions;

        public SurfaceReactionResolver(IEnumerable<SurfaceReaction> reactions)
        {
            lookup = new();

            foreach (var reaction in reactions)
            {
                if (reaction == null) continue;

                lookup[(reaction.surface, reaction.impactType)] = reaction;
            }
        }
        public void Resolve(in ImpactContext context, IReadOnlyList<SurfaceHit> surfaceHits)
        {
            foreach (var hit in surfaceHits)
            {
                if (!lookup.TryGetValue((hit.surface, context.impactType), out var reaction)) continue;
                var effectContext = new ImpactEffectContext(context.position, context.normal);
                reaction.effectSet.PlayAll(effectContext);
            }
            for (int i = 0; i < surfaceHits.Count; i++)
            {
                var hit = surfaceHits[i];
                if (!lookup.TryGetValue((hit.surface, context.impactType), out var reaction)) continue;

                var effectContext = new ImpactEffectContext(context.position, context.normal);

                reaction.effectSet.PlayAll(effectContext);
                
            }
        }
    }
}