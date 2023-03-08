namespace JJiGolem.Factions.DynamicMarkers
{
    internal class IdGenerator
    {
        private uint _nextId = 1;

        public IdGenerator(uint start = 1)
        {
            _nextId = start;
        }

        public void Set(uint id)
        {
            _nextId = id;
        }

        public uint Next()
        {
            return _nextId++;
        }
    }
}
