
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Muvuca
{
    public static class Util
    {
        public static string SerializeVector2(Vector2 vec2) => $"{vec2.x}|{vec2.y}";

        public static Vector2 DeserializeVector2(string text)
        {
            var numbers = text.Split('|');
            if (float.TryParse(numbers[0], out var x) && float.TryParse(numbers[1], out var y))
                return new Vector2(x, y);
            throw new InvalidCastException($"Serialized text: '{text}' is not a Vector2!");
        }
        public static float ClampAngle(float angle, float min, float max) {
            var start = (min + max) * 0.5f - 180;
            float floor = Mathf.FloorToInt((angle - start) / 360) * 360;
            return Mathf.Clamp(angle, min + floor, max + floor);
        }
        public static string SerializeVector3Array(Vector3[] aVectors)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Vector3 v in aVectors)
            {
                sb.Append(v.x).Append(" ").Append(v.y).Append(" ").Append(v.z).Append("|");
            }
            if (sb.Length > 0) // remove last "|"
                sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public static Vector3[] DeserializeVector3Array(string aData)
        {
            string[] vectors = aData.Split('|');
            List<Vector3> result = new(vectors.Length);
            for (int i = 0; i < vectors.Length; i++)
            {
                string[] values = vectors[i].Split(' ');
                if (values.Length != 3)
                    throw new System.FormatException("component count mismatch. Expected 3 components but got " + values.Length);
                result.Add(new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2])));
            }
            return result.ToArray();
        }


        public static float Map(this float value, float fromSource, float toSource, float fromTarget, float toTarget)
        {
            return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
        }
    }
}