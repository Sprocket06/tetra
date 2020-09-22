using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace tetra
{
    public static class VectorDictSerializer
    {

        public static Dictionary<Vector2, Vector2[]> ReadVectorDict(byte[] data)
        {
            using var ms = new MemoryStream(data);
            using var reader = new BinaryReader(ms);

            var dictSize = reader.ReadInt32();
            var dict = new Dictionary<Vector2, Vector2[]>(dictSize);

            for (int i = 0; i < dictSize; i++)
            {
                var vector = new Vector2(reader.ReadSingle(), reader.ReadSingle());

                var arraySize = reader.ReadInt32();
                var array = new Vector2[arraySize];
                for (int j = 0; j < array.Length; j++)
                {
                    array[j] = new Vector2(reader.ReadSingle(), reader.ReadSingle());
                }

                dict.Add(vector, array);
            }

            return dict;
        }

        public static byte[] WriteVectorDict(Dictionary<Vector2, Vector2[]> data)
        {
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);

            writer.Write(data.Count);
            foreach (var keyValuePair in data)
            {
                // Write vector2
                writer.Write(keyValuePair.Key.X);
                writer.Write(keyValuePair.Key.Y);

                // Write Vector2 Array
                writer.Write(keyValuePair.Value.Length);
                foreach (var vector in keyValuePair.Value)
                {
                    writer.Write(vector.X);
                    writer.Write(vector.Y);
                }
            }

            return ms.ToArray();
        }

    }
}
