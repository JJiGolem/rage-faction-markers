using System.Collections.Generic;

namespace JJiGolem.Factions.DynamicMarkers
{
    internal class FactionMarkerBuilder
    {
        private static readonly IdGenerator _idGenerator = new IdGenerator();
        private readonly FactionMarker _marker;

        public FactionMarkerBuilder(uint factionId, Vector3 position)
        {
            _marker = new FactionMarker()
            {
                FactionId = factionId,
                AvailableForFactions = new List<uint>(),
                Position = position
            };

            _marker.AvailableForFactions.Add(factionId);
        }

        public FactionMarkerBuilder SetPosition(Vector3 position)
        {
            _marker.Position = position;
            return this;
        }

        public FactionMarkerBuilder SetDimension(uint dimension)
        {
            _marker.Dimension = dimension;
            return this;
        }

        public FactionMarkerBuilder AddAvailableFaction(params uint[] factions)
        {
            _marker.AvailableForFactions ??= new List<uint>();
            _marker.AvailableForFactions.AddRange(factions);
            return this;
        }

        public FactionMarkerBuilder AddMarker(MarkerDetails markerDetails)
        {
            _marker.Marker = markerDetails;
            return this;
        }

        public FactionMarkerBuilder AddTextLabel(TextLabelDetails labelDetails)
        {
            _marker.Label = labelDetails;
            return this;
        }

        public FactionMarkerBuilder AddBlip(BlipDetails blipDetails)
        {
            _marker.Blip = blipDetails;
            return this;
        }

        public FactionMarker Build()
        {
            if (_marker.Marker == null && _marker.Label == null && _marker.Blip == null)
                throw new InvalidOperationException();

            _marker.Id = _idGenerator.Next();
            //FactionMarkerLoader.Instance.AddMarker(_marker);
            return _marker;
        }
    }
}
