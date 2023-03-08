const markers = []

mp.events.add("Client:FactionMarkers:UpdateMarker", updateMarker)
mp.events.add("Client:FactionMarkers:LoadMarker", loadMarker)
mp.events.add("Client:FactionMarkers:UnloadMarker", unloadMarker)
mp.events.add("Client:FactionMarkers:Load", loadMarkers)
mp.events.add("Client:FactionMarkers:UnLoad", unLoadMarkers)
mp.events.add("Client:FactionMarkers:UnloadByIds", unloadMarkersByIds)

function updateMarker(fMarkerJson) {
    if (!fMarkerJson || typeof fMarkerJson != 'string') {
        return;
    }

    const fMarkerData = JSON.parse(fMarkerJson);
    unloadMarker(fMarkerData.Id);
    loadMarker(fMarkerData);
}

function loadMarker(fMarker) {
    if (!fMarker) {
        return;
    }

    markers.push({
        Id: fMarker.Id,
        FactionId: fMarker.FactionId,
        Blip: createBlip(fMarker),
        Marker: createMarker(fMarker),
        Label: createTextLabel(fMarker)
    });
}

function unloadMarker(factionMarkerId) {
    const index = markers.findIndex(x => x.Id == factionMarkerId);
    if (index < 0) {
        return;
    }

    const fMarker = markers[index];
    if (!fMarker) {
        return;
    }

    if (fMarker.Blip) {
        fMarker.Blip.destroy();
    }

    if (fMarker.Marker) {
        fMarker.Marker.destroy();
    }

    if (fMarker.Label) {
        fMarker.Label.destroy();
    }

    markers.splice(index, 1);
}

function loadMarkers(json) {
    if (!json || typeof json != 'string') {
        return;
    }

    try {
        const data = JSON.parse(json);
        for (let i = 0; i < data.length; i++) {
            const fMarker = data[i];
            loadMarker(fMarker);
        }
    } catch (error) {
        mp.gui.chat.push("FactionMarkers load failed: " + error);
    }
}

function unLoadMarkers(factionId) {
    const factionMarkers = markers.filter(x => x.FactionId == factionId);
    for (let i = 0; i < factionMarkers.length; i++) {
        const marker = factionMarkers[i];
        unloadMarker(marker.Id);
    }
}

function unloadMarkersByIds(markerIdsJson) {
    if (!json || typeof json != 'string') {
        return;
    }

    try {
        const data = JSON.parse(markerIdsJson);
        for (let i = 0; i < data.length; i++) {
            const fMarkerIds = data[i];
            unloadMarker(fMarkerIds);
        }
    } catch (error) {
        mp.gui.chat.push("FactionMarkers unload failed: " + error);
    }
}

function createBlip(fMarker) {
    if (!fMarker || !fMarker.Blip) {
        return null;
    }

    const blipData = fMarker.Blip;
    return mp.blips.new(blipData.Sprite, new mp.Vector3(fMarker.Position.x, fMarker.Position.y, fMarker.Position.z), {
        name: blipData.name,
        scale: blipData.Scale,
        color: blipData.Color,
        alpha: blipData.Alpha,
        drawDistance: blipData.DrawDistance,
        shortRange: blipData.ShortRange,
        dimension: fMarker.Dimension,
        radius: blipData.Scale,
    });
}

function createMarker(fMarker) {
    if (!fMarker || !fMarker.Marker) {
        return null;
    }

    const markerData = fMarker.Marker;
    return mp.markers.new(markerData.Sprite, new mp.Vector3(fMarker.Position.x, fMarker.Position.y, fMarker.Position.z), markerData.Scale, {
        direction: new mp.Vector3(),
        rotation: new mp.Vector3(),
        color: [markerData.Color.Red, markerData.Color.Green, markerData.Color.Blue, markerData.Color.Alpha],
        visible: true,
        dimension: fMarker.Dimension
    });
}

function createTextLabel(fMarker) {
    if (!fMarker || !fMarker.Label) {
        return null;
    }

    const label = fMarker.Label;
    return mp.labels.new(label.Text, new mp.Vector3(fMarker.Position.x, fMarker.Position.y, fMarker.Position.z + label.TextOffsetZ), {
        los: label.SeeThrough,
        font: label.TextFont,
        drawDistance: label.TextRange,
        color: [label.TextColor.Red, label.TextColor.Green, label.TextColor.Blue, label.TextColor.Alpha],
        dimension: fMarker.Dimension
    });
}