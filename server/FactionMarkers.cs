using GTANetworkAPI;
using System.Collections.Generic;

namespace JJiGolem.Factions.DynamicMarkers
{
    internal class FactionMarker
    {
        public uint Id { get; set; }
        public uint FactionId { get; set; }
        public List<uint> AvailableForFactions { get; set; }

        public Vector3 Position { get; set; }
        public uint Dimension { get; set; }

        public BlipDetails Blip { get; set; }
        public MarkerDetails Marker { get; set; }
        public TextLabelDetails Label { get; set; }
    }

    internal class MarkerDetails
    {
        private const float DefaultScale = 1.0f;
        private const int MinSpriteValue = 0;
        private const int MaxSpriteValue = 44;

        public uint Sprite { get; private set; }
        public float Scale { get; private set; }
        public Color Color { get; private set; }

        public MarkerDetails SetScale(float scale)
        {
            Scale = scale >= 0.0f ? scale : DefaultScale;
            return this;
        }

        public MarkerDetails SetSprite(uint sprite)
        {
            Sprite = Math.Clamp(sprite, MinSpriteValue, MaxSpriteValue);
            return this;
        }

        public MarkerDetails SetColor(int r, int g, int b, int alpha = 255)
        {
            Color = new Color(r, g, b, alpha);
            return this;
        }
    }

    internal class TextLabelDetails
    {
        private const int DefaultFont = 0;
        private static readonly List<int> _fonts = new List<int>() { 0, 1, 2, 4, 7 };

        public string Text { get; private set; } = string.Empty;
        public float TextOffsetZ { get; private set; }
        public float TextRange { get; private set; }
        public int TextFont { get; private set; }
        public Color TextColor { get; private set; }
        public bool SeeThrough { get; private set; }

        public TextLabelDetails AddLine(string line)
        {
            if (string.IsNullOrWhiteSpace(Text))
                return SetText(line);

            Text += "\n" + line;
            return this;
        }

        public TextLabelDetails SetText(string text)
        {
            Text = text;
            return this;
        }

        public TextLabelDetails SetFont(int font)
        {
            TextFont = _fonts.Contains(font) ? font : DefaultFont;
            return this;
        }

        public TextLabelDetails SetOffsetZ(float offsetZ)
        {
            TextOffsetZ = offsetZ;
            return this;
        }

        public TextLabelDetails SetRange(float range)
        {
            TextRange = range;
            return this;
        }

        public TextLabelDetails SetColor(Color color)
        {
            TextColor = color;
            return this;
        }

        public TextLabelDetails SetColor(int r, int g, int b, int alpha = 255)
        {
            TextColor = new Color(r, g, b, alpha);
            return this;
        }

        public TextLabelDetails SetSeeThrough(bool toggle)
        {
            SeeThrough = toggle;
            return this;
        }
    }

    internal class BlipDetails
    {
        private const float DefaultScale = 1.0f;
        private const int MaxNameLength = 43;
        private const uint MinBlipSpriteValue = 1;
        private const uint MaxBlipSpriteValue = 838;
        private const byte MinBlipColorValue = 1;
        private const byte MaxBlipColorValue = 85;
        private const int FullTransparencyAlpha = 0;
        private const int FullVisibilityAlpha = 255;

        public string Name { get; private set; }
        public uint Sprite { get; private set; }
        public float Scale { get; private set; }
        public int Alpha  { get; private set; }
        public byte Color { get; private set; }
        public float DrawDistance { get; private set; }
        public bool ShortRange { get; private set; }

        public BlipDetails SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > MaxNameLength)
                throw new ArgumentException(nameof(name));

            Name = name;
            return this;
        }

        public BlipDetails SetSprite(uint sprite)
        {
            if (sprite > MaxBlipSpriteValue || sprite < MinBlipSpriteValue)
                throw new ArgumentException(nameof(sprite));
            
            Sprite = sprite;
            return this;
        }

        public BlipDetails SetScale(float scale)
        {
            Scale = scale >= 0.0f ? scale : DefaultScale;
            return this;
        }

        public BlipDetails SetAlpha(int alpha)
        {
            Alpha = Math.Clamp(alpha, FullTransparencyAlpha, FullVisibilityAlpha);
            return this;
        }

        public BlipDetails SetColor(byte color)
        {
            Color = Math.Clamp(color, MinBlipColorValue, MaxBlipColorValue);
            return this;
        }
    }
}
