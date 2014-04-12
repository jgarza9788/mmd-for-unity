﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;


namespace MMD
{
    public class ToFormatUtil
    {
        // ShiftJISからUTF-8に変換してstringで返す
        private static string ConvertByteToString(byte[] bytes, string line_feed_code = null)
        {
            // パディングの消去, 文字を詰める
            if (bytes[0] == 0) return "";
            int count;
            for (count = 0; count < bytes.Length; count++) if (bytes[count] == 0) break;
            byte[] buf = new byte[count];		// NULL文字を含めるとうまく行かない
            for (int i = 0; i < count; i++)
            {
                buf[i] = bytes[i];
            }

#if UNITY_EDITOR
#if UNITY_STANDALONE_OSX
            // こっちはエディタを使う場合
            buf = Encoding.Convert(Encoding.GetEncoding(932), Encoding.UTF8, buf);
#else
            // GetEncoding(0) = ANSI文字エンコード = SJISらしい
            buf = Encoding.Convert(Encoding.GetEncoding(0), Encoding.UTF8, buf);
#endif // UNITY_STANDALONE_OSX
#else
            string str_buf = USEncoder.ToEncoding.ToUnicode(buf);
		    buf = Encoding.Convert(Encoding.Unicode, Encoding.UTF8, buf);
#endif // UNITY_EDITOR
            string result = Encoding.UTF8.GetString(buf);
            if (null != line_feed_code)
            {
                //改行コード統一(もしくは除去)
                result = result.Replace("\r\n", "\n").Replace('\r', '\n').Replace("\n", line_feed_code);
            }
            return result;
        }

        private static Vector3 ReadSinglesToVector3(BinaryReader bin)
        {
            const int count = 3;
            float[] result = new float[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = bin.ReadSingle();
                if (float.IsNaN(result[i])) result[i] = 0.0f; //非数値なら回避
            }
            return new Vector3(result[0], result[1], result[2]);
        }

        private static Color ReadSinglesToColor(BinaryReader bin)
        {
            const int count = 4;
            float[] result = new float[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = bin.ReadSingle();
            }
            return new Color(result[0], result[1], result[2], result[3]);
        }

        private static Color ReadSinglesToColor(BinaryReader bin, float fix_alpha)
        {
            const int count = 3;
            float[] result = new float[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = bin.ReadSingle();
            }
            return new Color(result[0], result[1], result[2], fix_alpha);
        }

        private static Quaternion ReadSinglesToQuaternion(BinaryReader bin)
        {
            const int count = 4;
            float[] result = new float[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = bin.ReadSingle();
                if (float.IsNaN(result[i])) result[i] = 0.0f; //非数値なら回避
            }
            return new Quaternion(result[0], result[1], result[2], result[3]);
        }
    }
}