// ----------------------------------------------------------------------------
// <auto-generated>
// This is autogenerated code by CppSharp.
// Do not edit this file or all your changes will be lost after re-generation.
// </auto-generated>
// ----------------------------------------------------------------------------
using System;
using System.Runtime.InteropServices;
using System.Security;

namespace FriBidiSharp
{
    public unsafe partial class FriBidiSharpOthers
    {
        public partial struct __Internal
        {
            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="?fribidi_debug_status@@YAHXZ")]
            internal static extern int DebugStatus();

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="?fribidi_set_debug@@YAHH@Z")]
            internal static extern int SetDebug(int state);
        }

        public static int DebugStatus()
        {
            var __ret = __Internal.DebugStatus();
            return __ret;
        }

        public static int SetDebug(int state)
        {
            var __ret = __Internal.SetDebug(state);
            return __ret;
        }
    }

    public enum JoiningType
    {
        U = 0,
        R = 5,
        D = 7,
        C = 3,
        T = 12,
        L = 6,
        G = 16,
        Junk = 17
    }

    public enum CharSet
    {
        NotFound = 0,
        UTF8 = 1,
        CapRtl = 2,
        ISO8859_6 = 3,
        ISO8859_8 = 4,
        CP1255 = 5,
        CP1256 = 6,
        NumPlusOne = 7
    }

    public unsafe partial class FriBidiSharpMain
    {
        public partial struct __Internal
        {
            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_get_bidi_type")]
            internal static extern uint GetBidiType(uint ch);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_get_bidi_types")]
            internal static extern void GetBidiTypes(uint[] str, int len, uint[] btypes);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_get_bidi_type_name")]
            internal static extern global::System.IntPtr GetBidiTypeName(uint t);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_get_par_direction")]
            internal static extern uint GetParDirection(uint[] bidi_types, int len);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_get_par_embedding_levels_ex")]
            internal static extern sbyte GetParEmbeddingLevelsEx(uint[] bidi_types, uint[] bracket_types, int len, uint[] pbase_dir, sbyte[] embedding_levels);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_reorder_line")]
            internal static extern sbyte ReorderLine(global::FriBidiSharp.Flags flags, uint[] bidi_types, int len, int off, uint base_dir, sbyte[] embedding_levels, uint[] visual_str, int[] map);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_get_joining_type")]
            internal static extern byte GetJoiningType(uint ch);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_get_joining_types")]
            internal static extern void GetJoiningTypes(uint[] str, int len, byte[] jtypes);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_get_joining_type_name")]
            internal static extern global::System.IntPtr GetJoiningTypeName(byte j);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_join_arabic")]
            internal static extern void JoinArabic(uint[] bidi_types, int len, sbyte[] embedding_levels, byte[] ar_props);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_get_mirror_char")]
            internal static extern int GetMirrorChar(uint ch, uint[] mirrored_ch);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_shape_mirroring")]
            internal static extern void ShapeMirroring(sbyte[] embedding_levels, int len, uint[] str);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_get_bracket")]
            internal static extern uint GetBracket(uint ch);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_get_bracket_types")]
            internal static extern void GetBracketTypes(uint[] str, int len, uint[] types, uint[] btypes);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_shape_arabic")]
            internal static extern void ShapeArabic(global::FriBidiSharp.Flags flags, sbyte[] embedding_levels, int len, byte[] ar_props, uint[] str);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_shape")]
            internal static extern void Shape(global::FriBidiSharp.Flags flags, sbyte[] embedding_levels, int len, byte[] ar_props, uint[] str);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_charset_to_unicode")]
            internal static extern int CharsetToUnicode(global::FriBidiSharp.CharSet char_set, sbyte[] s, int len, uint[] us);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_unicode_to_charset")]
            internal static extern int UnicodeToCharset(global::FriBidiSharp.CharSet char_set, uint[] us, int len, sbyte[] s);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_parse_charset")]
            internal static extern global::FriBidiSharp.CharSet ParseCharset(sbyte[] s);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_char_set_name")]
            internal static extern global::System.IntPtr CharSetName(global::FriBidiSharp.CharSet char_set);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_char_set_title")]
            internal static extern global::System.IntPtr CharSetTitle(global::FriBidiSharp.CharSet char_set);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_char_set_desc")]
            internal static extern global::System.IntPtr CharSetDesc(global::FriBidiSharp.CharSet char_set);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_mirroring_status")]
            internal static extern int MirroringStatus();

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_set_mirroring")]
            internal static extern int SetMirroring(int state);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_reorder_nsm_status")]
            internal static extern int ReorderNsmStatus();

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_set_reorder_nsm")]
            internal static extern int SetReorderNsm(int state);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_log2vis_get_embedding_levels")]
            internal static extern sbyte Log2visGetEmbeddingLevels(uint[] bidi_types, int len, uint[] pbase_dir, sbyte[] embedding_levels);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_get_type")]
            internal static extern uint GetType(uint ch);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_get_type_internal")]
            internal static extern uint GetTypeInternal(uint ch);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_remove_bidi_marks")]
            internal static extern int RemoveBidiMarks(uint[] str, int len, int[] positions_to_this, int[] position_from_this_list, sbyte[] embedding_levels);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_log2vis")]
            internal static extern sbyte Log2vis(uint[] str, int len, uint[] pbase_dir, uint[] visual_str, int[] positions_L_to_V, int[] positions_V_to_L, sbyte[] embedding_levels);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("fribidi", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint="fribidi_get_par_embedding_levels")]
            internal static extern sbyte GetParEmbeddingLevels(uint[] bidi_types, int len, uint[] pbase_dir, sbyte[] embedding_levels);
        }

        public static uint GetBidiType(uint ch)
        {
            var __ret = __Internal.GetBidiType(ch);
            return __ret;
        }

        public static void GetBidiTypes(uint[] str, int len, uint[] btypes)
        {
            __Internal.GetBidiTypes(str, len, btypes);
        }

        public static string GetBidiTypeName(uint t)
        {
            var __ret = __Internal.GetBidiTypeName(t);
            return Marshal.PtrToStringAnsi(__ret);
        }

        public static uint GetParDirection(uint[] bidi_types, int len)
        {
            var __ret = __Internal.GetParDirection(bidi_types, len);
            return __ret;
        }

        public static sbyte GetParEmbeddingLevelsEx(uint[] bidi_types, uint[] bracket_types, int len, uint[] pbase_dir, sbyte[] embedding_levels)
        {
            var __ret = __Internal.GetParEmbeddingLevelsEx(bidi_types, bracket_types, len, pbase_dir, embedding_levels);
            return __ret;
        }

        public static sbyte ReorderLine(global::FriBidiSharp.Flags flags, uint[] bidi_types, int len, int off, uint base_dir, sbyte[] embedding_levels, uint[] visual_str, int[] map)
        {
            var __ret = __Internal.ReorderLine(flags, bidi_types, len, off, base_dir, embedding_levels, visual_str, map);
            return __ret;
        }

        public static byte GetJoiningType(uint ch)
        {
            var __ret = __Internal.GetJoiningType(ch);
            return __ret;
        }

        public static void GetJoiningTypes(uint[] str, int len, byte[] jtypes)
        {
            __Internal.GetJoiningTypes(str, len, jtypes);
        }

        public static string GetJoiningTypeName(byte j)
        {
            var __ret = __Internal.GetJoiningTypeName(j);
            return Marshal.PtrToStringAnsi(__ret);
        }

        public static void JoinArabic(uint[] bidi_types, int len, sbyte[] embedding_levels, byte[] ar_props)
        {
            __Internal.JoinArabic(bidi_types, len, embedding_levels, ar_props);
        }

        public static int GetMirrorChar(uint ch, uint[] mirrored_ch)
        {
            var __ret = __Internal.GetMirrorChar(ch, mirrored_ch);
            return __ret;
        }

        public static void ShapeMirroring(sbyte[] embedding_levels, int len, uint[] str)
        {
            __Internal.ShapeMirroring(embedding_levels, len, str);
        }

        public static uint GetBracket(uint ch)
        {
            var __ret = __Internal.GetBracket(ch);
            return __ret;
        }

        public static void GetBracketTypes(uint[] str, int len, uint[] types, uint[] btypes)
        {
            __Internal.GetBracketTypes(str, len, types, btypes);
        }

        public static void ShapeArabic(global::FriBidiSharp.Flags flags, sbyte[] embedding_levels, int len, byte[] ar_props, uint[] str)
        {
            __Internal.ShapeArabic(flags, embedding_levels, len, ar_props, str);
        }

        public static void Shape(global::FriBidiSharp.Flags flags, sbyte[] embedding_levels, int len, byte[] ar_props, uint[] str)
        {
            __Internal.Shape(flags, embedding_levels, len, ar_props, str);
        }

        public static int CharsetToUnicode(global::FriBidiSharp.CharSet char_set, sbyte[] s, int len, uint[] us)
        {
            var __ret = __Internal.CharsetToUnicode(char_set, s, len, us);
            return __ret;
        }

        public static int UnicodeToCharset(global::FriBidiSharp.CharSet char_set, uint[] us, int len, sbyte[] s)
        {
            var __ret = __Internal.UnicodeToCharset(char_set, us, len, s);
            return __ret;
        }

        public static global::FriBidiSharp.CharSet ParseCharset(sbyte[] s)
        {
            var __ret = __Internal.ParseCharset(s);
            return __ret;
        }

        public static string CharSetName(global::FriBidiSharp.CharSet char_set)
        {
            var __ret = __Internal.CharSetName(char_set);
            return Marshal.PtrToStringAnsi(__ret);
        }

        public static string CharSetTitle(global::FriBidiSharp.CharSet char_set)
        {
            var __ret = __Internal.CharSetTitle(char_set);
            return Marshal.PtrToStringAnsi(__ret);
        }

        public static string CharSetDesc(global::FriBidiSharp.CharSet char_set)
        {
            var __ret = __Internal.CharSetDesc(char_set);
            return Marshal.PtrToStringAnsi(__ret);
        }

        public static int MirroringStatus()
        {
            var __ret = __Internal.MirroringStatus();
            return __ret;
        }

        public static int SetMirroring(int state)
        {
            var __ret = __Internal.SetMirroring(state);
            return __ret;
        }

        public static int ReorderNsmStatus()
        {
            var __ret = __Internal.ReorderNsmStatus();
            return __ret;
        }

        public static int SetReorderNsm(int state)
        {
            var __ret = __Internal.SetReorderNsm(state);
            return __ret;
        }

        public static sbyte Log2visGetEmbeddingLevels(uint[] bidi_types, int len, uint[] pbase_dir, sbyte[] embedding_levels)
        {
            var __ret = __Internal.Log2visGetEmbeddingLevels(bidi_types, len, pbase_dir, embedding_levels);
            return __ret;
        }

        public static uint GetType(uint ch)
        {
            var __ret = __Internal.GetType(ch);
            return __ret;
        }

        public static uint GetTypeInternal(uint ch)
        {
            var __ret = __Internal.GetTypeInternal(ch);
            return __ret;
        }

        public static int RemoveBidiMarks(uint[] str, int len, int[] positions_to_this, int[] position_from_this_list, sbyte[] embedding_levels)
        {
            var __ret = __Internal.RemoveBidiMarks(str, len, positions_to_this, position_from_this_list, embedding_levels);
            return __ret;
        }

        public static sbyte Log2vis(uint[] str, int len, uint[] pbase_dir, uint[] visual_str, int[] positions_L_to_V, int[] positions_V_to_L, sbyte[] embedding_levels)
        {
            var __ret = __Internal.Log2vis(str, len, pbase_dir, visual_str, positions_L_to_V, positions_V_to_L, embedding_levels);
            return __ret;
        }

        public static sbyte GetParEmbeddingLevels(uint[] bidi_types, int len, uint[] pbase_dir, sbyte[] embedding_levels)
        {
            var __ret = __Internal.GetParEmbeddingLevels(bidi_types, len, pbase_dir, embedding_levels);
            return __ret;
        }
    }

    public enum NamedChar
    {
        LRM = 8206,
        RLM = 8207,
        LRE = 8234,
        RLE = 8235,
        PDF = 8236,
        LRO = 8237,
        RLO = 8238,
        LRI = 8294,
        RLI = 8295,
        FSI = 8296,
        PDI = 8297,
        LS = 8232,
        PS = 8233,
        ZWNJ = 8204,
        ZWJ = 8205,
        HebrewAlef = 1488,
        ArabicAlef = 1575,
        ArabicZero = 1632,
        PersianZero = 1776,
        ZWNBSP = 65279,
        FILL = 65279
    }

    [Flags]
    public enum Flags
    {
        ShapeMirroring = 1,
        ReorderNsm = 2,
        ArabPres = 256,
        ArabLiga = 512,
        ArabConsole = 1024,
        RemoveBidi = 65536,
        RemoveJoining = 131072,
        RemoveSpecials = 262144
    }

    [Flags]
    public enum TypeMasks
    {
        FRIBIDI_MASK_RTL = 1,
        FRIBIDI_MASK_ARABIC = 2,
        FRIBIDI_MASK_STRONG = 16,
        FRIBIDI_MASK_WEAK = 32,
        FRIBIDI_MASK_NEUTRAL = 64,
        FRIBIDI_MASK_SENTINEL = 128,
        FRIBIDI_MASK_LETTER = 256,
        FRIBIDI_MASK_NUMBER = 512,
        FRIBIDI_MASK_NUMSEPTER = 1024,
        FRIBIDI_MASK_SPACE = 2048,
        FRIBIDI_MASK_EXPLICIT = 4096,
        FRIBIDI_MASK_ISOLATE = 32768,
        FRIBIDI_MASK_SEPARATOR = 8192,
        FRIBIDI_MASK_OVERRIDE = 16384,
        FRIBIDI_MASK_FIRST = 33554432,
        FRIBIDI_MASK_ES = 65536,
        FRIBIDI_MASK_ET = 131072,
        FRIBIDI_MASK_CS = 262144,
        FRIBIDI_MASK_NSM = 524288,
        FRIBIDI_MASK_BN = 1048576,
        FRIBIDI_MASK_BS = 2097152,
        FRIBIDI_MASK_SS = 4194304,
        FRIBIDI_MASK_WS = 8388608,
        FRIBIDI_MASK_PRIVATE = 16777216
    }

    public enum Type
    {
        FRIBIDI_TYPE_LTR_VAL = 272,
        FRIBIDI_TYPE_RTL_VAL = 273,
        FRIBIDI_TYPE_AL_VAL = 275,
        FRIBIDI_TYPE_LRE_VAL = 4112,
        FRIBIDI_TYPE_RLE_VAL = 4113,
        FRIBIDI_TYPE_LRO_VAL = 20496,
        FRIBIDI_TYPE_RLO_VAL = 20497,
        FRIBIDI_TYPE_PDF_VAL = 4128,
        FRIBIDI_TYPE_EN_VAL = 544,
        FRIBIDI_TYPE_AN_VAL = 546,
        FRIBIDI_TYPE_ES_VAL = 66592,
        FRIBIDI_TYPE_ET_VAL = 132128,
        FRIBIDI_TYPE_CS_VAL = 263200,
        FRIBIDI_TYPE_NSM_VAL = 524320,
        FRIBIDI_TYPE_BN_VAL = 1050656,
        FRIBIDI_TYPE_BS_VAL = 2107456,
        FRIBIDI_TYPE_SS_VAL = 4204608,
        FRIBIDI_TYPE_WS_VAL = 8390720,
        FRIBIDI_TYPE_ON_VAL = 64,
        FRIBIDI_TYPE_WLTR_VAL = 32,
        FRIBIDI_TYPE_WRTL_VAL = 33,
        FRIBIDI_TYPE_SENTINEL = 128,
        FRIBIDI_TYPE_PRIVATE = 16777216,
        FRIBIDI_TYPE_LRI_VAL = 32832,
        FRIBIDI_TYPE_RLI_VAL = 32833,
        FRIBIDI_TYPE_FSI_VAL = 33587264,
        FRIBIDI_TYPE_PDI_VAL = 32864,
        FRIBIDI_TYPE_LTR = 272,
        FRIBIDI_TYPE_RTL = 273,
        FRIBIDI_TYPE_AL = 275,
        FRIBIDI_TYPE_EN = 544,
        FRIBIDI_TYPE_AN = 546,
        FRIBIDI_TYPE_ES = 66592,
        FRIBIDI_TYPE_ET = 132128,
        FRIBIDI_TYPE_CS = 263200,
        FRIBIDI_TYPE_NSM = 524320,
        FRIBIDI_TYPE_BN = 1050656,
        FRIBIDI_TYPE_BS = 2107456,
        FRIBIDI_TYPE_SS = 4204608,
        FRIBIDI_TYPE_WS = 8390720,
        FRIBIDI_TYPE_ON = 64,
        FRIBIDI_TYPE_LRE = 4112,
        FRIBIDI_TYPE_RLE = 4113,
        FRIBIDI_TYPE_LRO = 20496,
        FRIBIDI_TYPE_RLO = 20497,
        FRIBIDI_TYPE_PDF = 4128,
        FRIBIDI_TYPE_LRI = 32832,
        FRIBIDI_TYPE_RLI = 32833,
        FRIBIDI_TYPE_FSI = 33587264,
        FRIBIDI_TYPE_PDI = 32864,
        FRIBIDI_TYPE_WLTR = 32,
        FRIBIDI_TYPE_WRTL = 33
    }

    public enum DeprecatedType
    {
        FRIBIDI_PAR_LTR = 272,
        FRIBIDI_PAR_RTL = 273,
        FRIBIDI_PAR_ON = 64,
        FRIBIDI_PAR_WLTR = 32,
        FRIBIDI_PAR_WRTL = 33
    }

    [Flags]
    public enum JoinMasks
    {
        FRIBIDI_MASK_JOINS_RIGHT = 1,
        FRIBIDI_MASK_JOINS_LEFT = 2,
        FRIBIDI_MASK_ARAB_SHAPES = 4,
        FRIBIDI_MASK_TRANSPARENT = 8,
        FRIBIDI_MASK_IGNORED = 16,
        FRIBIDI_MASK_LIGATURED = 32
    }
}
