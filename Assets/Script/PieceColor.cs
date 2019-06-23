using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tomino
{
    using Mapping = Dictionary<PieceType, Color>;

    public static class PieceTypeExtension
    {
        private static Lazy<Mapping> colorMapping = new Lazy<Mapping>(CreateColorMapping);

        public static Color GetColor(this PieceType type) => colorMapping.Value[type];

        private static Mapping CreateColorMapping()
        {
            var mapping = new Mapping();
            int index = 0;
            foreach (PieceType type in Enum.GetValues(typeof(PieceType)))
            {
                var color = Color.black;
                ColorUtility.TryParseHtmlString(Constant.ColorPalette.Blocks[index++], out color);
                mapping.Add(type, color);
            }
            return mapping;
        }
    }
}
