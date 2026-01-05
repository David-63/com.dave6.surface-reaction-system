using System.Collections.Generic;
using UnityEngine;

namespace Dave6.SurfaceReactionSystem
{
    [System.Serializable]
    public class RendererSurfaceBinding
    {
        public Texture albedo;
        public Surface surface;
    }
    [System.Serializable]
    public class TerrainSurfaceBinding
    {
        public TerrainLayer terrainLayer;
        public Surface surface;
    }
    [CreateAssetMenu(fileName = "SurfaceReactionDatabase", menuName = "DaveAssets/SurfaceReactionSystem/Surface Reaction Database")]
    public class SurfaceReactionDatabase : ScriptableObject
    {
        public List<SurfaceReaction> reactions = new();

        public Surface defaultSurface;
        [Header("Renderer Surface Mapping")]
        public List<RendererSurfaceBinding> rendererSurfaces = new();
        [Header("Terrain Surface Mapping")]
        public List<TerrainSurfaceBinding> terrainSurfaces = new();

        static readonly int m_BaseMap = Shader.PropertyToID("_BaseMap");
        static readonly int m_MainTex = Shader.PropertyToID("_MainTex");


        public Surface GetSurfaceFromRenderer(Renderer renderer)
        {
            if (renderer == null) return null;

            var material = renderer.sharedMaterial;
            if (material == null) return null;

            if (!material.HasProperty(m_BaseMap) && !material.HasProperty(m_MainTex)) return null;

            var texture = material.mainTexture;
            if (texture == null) return null;

            foreach (var binding in rendererSurfaces)
            {
                if (binding.albedo == texture)
                {
                    return binding.surface;
                }
            }

            return null;
        }
        public IEnumerable<SurfaceHit> GetSurfaceHitsFromTerrain(Terrain terrain, Vector3 position)
        {
            // 월드 좌표 -> terrain local 좌표 변환
            // 알파맵 좌표 계산
            // 해당 위치의 layer weight 배열 얻기
            // weight > 0 인 것만 surfaceHit으로 변환

            TerrainData data = terrain.terrainData;
            Vector3 terrainPos = position - terrain.transform.position;

            int mapX = Mathf.FloorToInt(terrainPos.x / data.size.x * data.alphamapWidth);
            int mapZ = Mathf.FloorToInt(terrainPos.z / data.size.z * data.alphamapHeight);

            float [,,] alphas = data.GetAlphamaps(mapX, mapZ, 1, 1);

            for (int i = 0; i <data.terrainLayers.Length; i++)
            {
                float weight = alphas[0, 0, i];
                if (weight <= 0f) continue;

                var layer = data.terrainLayers[i];

                foreach (var binding in terrainSurfaces)
                {
                    if (binding.terrainLayer == layer)
                    {
                        yield return new SurfaceHit(binding.surface, weight);
                        break;
                    }
                }
            }
        }
    }
}