using GTANetworkAPI;
using System.Collections.Generic;

namespace JJiGolem.Factions.DynamicMarkers
{
    internal class FactionMarkerLoader
    {
        public static FactionMarkerLoader Instance => new FactionMarkerLoader();
        public IReadOnlyCollection<FactionMarker> Markers => _markers;

        private FactionMarkerLoader()
        {
            _clientTrigger = new FactionMarkerClientTrigger();
            _markers = new List<FactionMarker>();
        }

        private readonly FactionMarkerClientTrigger _clientTrigger;
        private readonly List<FactionMarker> _markers;

        public void AddMarker(FactionMarker marker)
        {
            if (_markers.Contains(marker))
                return;

            _markers.Add(marker);
            // UpdateMarker(marker)
        }

        public void UpdateMarker(FactionMarker marker)
        {
            Player[] players = NAPI.Pools.GetAllPlayers().ToList().FindAll(p => HasPermissionToMarker(p, marker)).ToArray();
            if (players.Length < 1)
                return;

            _clientTrigger.UpdateMarker(players, marker);
        }

        public void LoadMarkerToPlayer(Player player, FactionMarker marker)
        {
            if (marker == null)
                return;

            _clientTrigger.LoadMarker(player, marker);
        }

        public void UnloadMarkerToPlayer(Player player, FactionMarker marker)
        {
            if (marker == null)
                return;

            _clientTrigger.UnloadMarker(player, marker);
        }
        
        public void LoadAvailableMarkers(Player player)
        {
            List<FactionMarker> accessMarkers = _markers.FindAll(m => HasPermissionToMarker(player, m));
            if (accessMarkers.Count < 1)
                return;

            _clientTrigger.LoadMarkers(player, accessMarkers.AsReadOnly());
        }

        public void UnloadAvailableMarkers(Player player)
        {
            List<uint> accessMarkers = _markers.FindAll(m => HasPermissionToMarker(player, m)).Select(m => m.Id);
            if (accessMarkers.Count < 1)
                return;

            _clientTrigger.UnloadMarkersByIds(accessMarkers);
        }

        public void LoadMarkersToPlayer(Player player, uint factionId)
        {
            if (!_markers.Exists(x => x.FactionId == factionId))
                return;

            List<FactionMarker> markers = _markers.FindAll(x => x.FactionId == factionId);
            if (markers.Count < 1)
                return;

            _clientTrigger.LoadMarkers(player, markers.AsReadOnly());
        }

        public void UnLoadMarkersToPlayer(Player player, uint factionId)
        {
            _clientTrigger.UnLoadMarkersByFaction(player, factionId);
        }

        private bool HasPermissionToMarker(Player player, FactionMarker marker)
        {
            uint faction = GetPlayerFaction(player);
            return marker.FactionId == faction || marker.AvailableForFactions.Contains(faction);
        }

        private uint GetPlayerFaction(Player player)
        {
            // implement it for yourself
            return 1;
        }
    }
}
