using GTANetworkAPI;
using System.Collections.Generic;

namespace JJiGolem.Factions.DynamicMarkers
{
    internal class FactionMarkerClientTrigger
    {
        private const string UpdateMarkerClientEvent = "Client:FactionMarkers:UpdateMarker";
        private const string LoadMarkerClientEvent = "Client:FactionMarkers:LoadMarker";
        private const string UnloadMarkerClientEvent = "Client:FactionMarkers:UnloadMarker";
        private const string MarkersLoadClientEvent = "Client:FactionMarkers:Load";
        private const string MarkersUnloadByIdsClientEvent = "Client:FactionMarkers:UnloadByIds";
        private const string MarkersUnloadByFactionClientEvent = "Client:FactionMarkers:UnloadByFaction";

        public void UpdateMarker(Player[] players, FactionMarker marker)
        {
            NAPI.ClientEventThreadSafe.TriggerClientEventToPlayers(players, UpdateMarkerClientEvent, ToJson(marker));
        }

        public void LoadMarker(Player player, FactionMarker marker)
        {
            NAPI.ClientEventThreadSafe.TriggerClientEvent(player, LoadMarkerClientEvent, marker.FactionId, ToJson(marker));
        }

        public void UnloadMarker(Player player, FactionMarker marker)
        {
            NAPI.ClientEventThreadSafe.TriggerClientEvent(player, UnloadMarkerClientEvent, marker.Id);
        }

        public void LoadMarkers(Player player, IReadOnlyList<FactionMarker> markers)
        {
            NAPI.ClientEventThreadSafe.TriggerClientEvent(player, MarkersLoadClientEvent, ToJson(markers));
        }

        public void UnloadMarkersByIds(Player player, IReadOnlyList<uint> markersIds)
        {
            NAPI.ClientEventThreadSafe.TriggerClientEvent(player, MarkersUnloadByIdsClientEvent, ToJson(markersIds));
        }

        public void UnLoadMarkersByFaction(Player player, uint faction)
        {
            NAPI.ClientEventThreadSafe.TriggerClientEvent(player, MarkersUnloadByFactionClientEvent, faction);
        }

        private string ToJson(object obj)
        {
            return NAPI.Util.ToJson(obj);
        }
    }
}
