namespace Character.Utils
{
    public class PlayerColorLayer
    {
        public int Layer { get; }
        public bool IsAvailable { get; private set; }

        public PlayerColorLayer(int layer, bool isAvailable)
        {
            Layer = layer;
            IsAvailable = isAvailable;
        }

        public void SetAvailable(bool isAvailable)
        {
            IsAvailable = isAvailable;
        }
    }
}
