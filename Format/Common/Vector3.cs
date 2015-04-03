﻿using System.Collections;

namespace MMD
{
    namespace Format
    {
        namespace Common
        {
            public class Vector3 : Chunk
            {
                public float x, y, z;

                public override void Read(System.IO.BinaryReader r)
                {
                    x = ReadFloat(r);
                    y = ReadFloat(r);
                    z = ReadFloat(r);
                }
            }
        }
    }
}