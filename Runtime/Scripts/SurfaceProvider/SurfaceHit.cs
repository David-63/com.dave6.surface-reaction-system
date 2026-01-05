namespace Dave6.SurfaceReactionSystem
{
    public struct SurfaceHit
    {
        public Surface surface;
        public float weight; // terrain alpha 같은 거
        public SurfaceHit(Surface surface, float weight)
        {
            this.surface = surface;
            this.weight = weight;
        }
    }
}