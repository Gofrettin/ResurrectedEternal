using ResurrectedEternal;
using RRFull;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

[Flags]
public enum ContentsFlag : uint
{
    CONTENTS_EMPTY = 0,
    CONTENTS_SOLID = 0x1,
    CONTENTS_WINDOW = 0x2,
    CONTENTS_AUX = 0x4,
    CONTENTS_GRATE = 0x8,
    CONTENTS_SLIME = 0x10,
    CONTENTS_WATER = 0x20,
    CONTENTS_MIST = 0x40,
    CONTENTS_OPAQUE = 0x80,
    CONTENTS_TESTFOGVOLUME = 0x100,
    CONTENTS_UNUSED = 0x200,
    CONTENTS_UNUSED6 = 0x400,
    CONTENTS_TEAM1 = 0x800,
    CONTENTS_TEAM2 = 0x1000,
    CONTENTS_IGNORE_NODRAW_OPAQUE = 0x2000,
    CONTENTS_MOVEABLE = 0x4000,
    CONTENTS_AREAPORTAL = 0x8000,
    CONTENTS_PLAYERCLIP = 0x10000,
    CONTENTS_MONSTERCLIP = 0x20000,
    CONTENTS_CURRENT_0 = 0x40000,
    CONTENTS_CURRENT_90 = 0x80000,
    CONTENTS_CURRENT_180 = 0x100000,
    CONTENTS_CURRENT_270 = 0x200000,
    CONTENTS_CURRENT_UP = 0x400000,
    CONTENTS_CURRENT_DOWN = 0x800000,
    CONTENTS_ORIGIN = 0x1000000,
    CONTENTS_MONSTER = 0x2000000,
    CONTENTS_DEBRIS = 0x4000000,
    CONTENTS_DETAIL = 0x8000000,
    CONTENTS_TRANSLUCENT = 0x10000000,
    CONTENTS_LADDER = 0x20000000,
    CONTENTS_HITBOX = 0x40000000
}

enum eLumpIndex : uint
{
    LUMP_ENTITIES = 0,
    LUMP_PLANES = 1,
    LUMP_TEXDATA = 2,
    LUMP_VERTEXES = 3,
    LUMP_VISIBILITY = 4,
    LUMP_NODES = 5,
    LUMP_TEXINFO = 6,
    LUMP_FACES = 7,
    LUMP_LIGHTING = 8,
    LUMP_OCCLUSION = 9,
    LUMP_LEAFS = 10,
    LUMP_EDGES = 12,
    LUMP_SURFEDGES = 13,
    LUMP_MODELS = 14,
    LUMP_WORLDLIGHTS = 15,
    LUMP_LEAFFACES = 16,
    LUMP_LEAFBRUSHES = 17,
    LUMP_BRUSHES = 18,
    LUMP_BRUSHSIDES = 19,
    LUMP_AREAS = 20,
    LUMP_AREAPORTALS = 21,
    LUMP_PORTALS = 22,
    LUMP_CLUSTERS = 23,
    LUMP_PORTALVERTS = 24,
    LUMP_CLUSTERPORTALS = 25,
    LUMP_DISPINFO = 26,
    LUMP_ORIGINALFACES = 27,
    LUMP_PHYSCOLLIDE = 29,
    LUMP_VERTNORMALS = 30,
    LUMP_VERTNORMALINDICES = 31,
    LUMP_DISP_LIGHTMAP_ALPHAS = 32,
    LUMP_DISP_VERTS = 33,
    LUMP_DISP_LIGHTMAP_SAMPLE_POSITIONS = 34,
    LUMP_GAME_LUMP = 35,
    LUMP_LEAFWATERDATA = 36,
    LUMP_PRIMITIVES = 37,
    LUMP_PRIMVERTS = 38,
    LUMP_PRIMINDICES = 39,
    LUMP_PAKFILE = 40,
    LUMP_CLIPPORTALVERTS = 41,
    LUMP_CUBEMAPS = 42,
    LUMP_TEXDATA_STRING_DATA = 43,
    LUMP_TEXDATA_STRING_TABLE = 44,
    LUMP_OVERLAYS = 45,
    LUMP_LEAFMINDISTTOWATER = 46,
    LUMP_FACE_MACRO_TEXTURE_INFO = 47,
    LUMP_DISP_TRIS = 48,
    LUMP_PHYSCOLLIDESURFACE = 49,
    LUMP_WATEROVERLAYS = 50,
    LUMP_LEAF_AMBIENT_INDEX_HDR = 51,
    LUMP_LEAF_AMBIENT_INDEX = 52,
    LUMP_LIGHTING_HDR = 53,
    LUMP_WORLDLIGHTS_HDR = 54,
    LUMP_LEAF_AMBIENT_LIGHTING_HDR = 55,
    LUMP_LEAF_AMBIENT_LIGHTING = 56,
    LUMP_XZIPPAKFILE = 57,
    LUMP_FACES_HDR = 58,
    LUMP_MAP_FLAGS = 59,
    LUMP_OVERLAY_FADES = 60,
    LUMP_OVERLAY_SYSTEM_LEVELS = 61,
    LUMP_PHYSLEVEL = 62,
    LUMP_DISP_MULTIBLEND = 63
};

[StructLayout(LayoutKind.Sequential)]
public struct cplane_t
{
    public Vector3 m_Normal;
    public float m_Distance;
    public byte m_Type;
    public byte m_SignBits;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    private byte[] m_Pad;
}
[StructLayout(LayoutKind.Sequential)]
public struct dbrush_t
{
    public int m_Firstside;
    public int m_Numsides;
    public int m_Contents;
}

[StructLayout(LayoutKind.Sequential)]
public struct dbrushside_t
{
    public ushort m_Planenum;
    public short m_Texinfo;
    public short m_Dispinfo;
    public byte m_Bevel;
    public byte m_Thin;
}

[StructLayout(LayoutKind.Sequential)]
public struct dedge_t
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public ushort[] m_V;
}

[StructLayout(LayoutKind.Sequential)]
public struct dface_t
{
    public ushort m_Planenum;
    public byte m_Side;
    public byte m_OnNode;
    public int m_Firstedge;
    public short m_Numedges;
    public short m_Texinfo;
    public short m_Dispinfo;
    public short m_SurfaceFogVolumeID;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public byte[] m_Styles;

    public int m_Lightofs;
    public float m_Area;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public int[] m_LightmapTextureMinsInLuxels;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public int[] m_LightmapTextureSizeInLuxels;

    public int m_OrigFace;
    public ushort m_NumPrims;
    public ushort m_FIrstPrimID;
    public ushort m_SmoothingGroups;
}

[StructLayout(LayoutKind.Sequential)]
public struct dgamelump_t
{
    public int m_Id;
    public ushort m_Flags;
    public ushort m_Version;
    public int m_FileOfs;
    public int m_FileLen;
}

[StructLayout(LayoutKind.Sequential)]
public struct dgamelumpheader_t
{
    public int m_LumpCount;
}

[StructLayout(LayoutKind.Sequential)]
public struct dheader_t
{
    public int m_Ident;
    public int m_Version;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = BSPFlags.HEADER_LUMPS)]
    public lump_t[] m_Lumps;
    public int m_MapRevision;
}

[StructLayout(LayoutKind.Sequential)]
public struct dleaf_t
{
    public ContentsFlag m_Contents;
    public short m_Cluster;
    public short m_Area;
    public area_flags m_AreaFlags;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public short[] m_Mins;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public short[] m_Maxs;

    public ushort m_Firstleafface;
    public ushort m_Numleaffaces;
    public ushort m_Firstleafbrush;
    public ushort m_Numleafbrushes;
    public short m_LeafWaterDataID;
}

[StructLayout(LayoutKind.Sequential)]
public struct area_flags
{
    private short m_Data;

    public short m_Area { get { return (short)(m_Data & 0x1ff); } }
    public short m_Flags { get { return (short)(m_Data >> 9); } }
}

[StructLayout(LayoutKind.Sequential)]
struct dnode_t
{
    public int m_Planenum;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public int[] m_Children;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public short[] m_Mins;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public short[] m_Maxs;

    public ushort m_Firstface;
    public ushort m_Numfaces;
    public short m_Area;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    private byte[] m_Pad;
}

[StructLayout(LayoutKind.Sequential)]
struct dplane_t
{
    public Vector3 m_Normal;
    public float m_Distance;
    public byte m_Type;
}
[StructLayout(LayoutKind.Sequential)]
public struct lump_t
{
    public int m_Fileofs;
    public int m_Filelen;
    public int m_Version;
    //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
    //public string m_FourCC;
    public int m_FourCC;
}
[StructLayout(LayoutKind.Sequential)]
public struct mvertex_t
{
    public Vector3 m_Position;
}
[StructLayout(LayoutKind.Sequential)]
public struct Polygon
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = BSPFlags.MAX_SURFINFO_VERTS)]
    public Vector3[] m_Verts;

    public int m_nVerts;
    public VPlane m_Plane;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = BSPFlags.MAX_SURFINFO_VERTS)]
    public VPlane[] m_EdgePlanes;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = BSPFlags.MAX_SURFINFO_VERTS)]
    public Vector3[] m_Vec2D;

    public int m_Skip;
}
[StructLayout(LayoutKind.Sequential)]
public struct snode_t
{
    public int m_Planenum;
    public long m_pPlane;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public int[] m_Children;

    public long m_LeafChildren;
    public long m_NodeChildren;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public short[] m_Mins;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public short[] m_Maxs;

    public ushort m_Firstface;
    public ushort m_Numfaces;
    public short m_Area;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    private byte[] m_Pad;

    public snode_t(int a)
    {
        m_Planenum = 0;
        m_pPlane = 0;
        m_Children = new int[2];
        m_LeafChildren = 0;
        m_NodeChildren = 0;
        m_Mins = new short[3];
        m_Maxs = new short[3];
        m_Firstface = 0;
        m_Numfaces = 0;
        m_Area = 0;
        m_Pad = new byte[2];
    }
}
[StructLayout(LayoutKind.Sequential)]
public struct StaticPropDictLump_t
{
    public int m_DictEntries;
}

[StructLayout(LayoutKind.Sequential)]
public struct StaticPropDictLumpName
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
    public string m_Name;
}

[StructLayout(LayoutKind.Sequential)]
public struct StaticPropLeafLump_t
{
    public int m_LeafEntries;
}

[StructLayout(LayoutKind.Sequential)]
public struct StaticPropLump_t
{
    // v4
    public Vector3 Origin;       // origin
    public Vector3 Angles;       // orientation (pitch roll yaw)
    public ushort PropType;    // index into model name dictionary
    public ushort FirstLeaf;   // index into leaf array
    public ushort LeafCount;
    public byte Solid;       // solidity type
    public byte Flags;
    public int Skin;        // model skin numbers
    public float FadeMinDist;
    public float FadeMaxDist;
    public Vector3 LightingOrigin;  // for lighting
                                    // since v5
    public float ForcedFadeScale; // fade distance scale
                                  // v6 and v7 only
                                  //public ushort MinDXLevel;      // minimum DirectX version to be visible
                                  //public ushort MaxDXLevel;      // maximum DirectX version to be visible
                                  // since v8
    public byte MinCPULevel;
    public byte MaxCPULevel;
    public byte MinGPULevel;
    public byte MaxGPULevel;
    // since v7
    public int DiffuseModulation; // per instance color and alpha modulation
                                  // since v10
    public float unknown;
    // since v9
    public bool DisableX360;     // if true, don't show on XBox 360
}

[StructLayout(LayoutKind.Sequential)]
public struct texinfo_t
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public Vector4[] m_TextureVecs;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public Vector4[] m_LightmapVecs;

    public int m_Flags;
    public int m_Texdata;
}

[StructLayout(LayoutKind.Sequential)]
public struct Vector4
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] m_Elements;
}

struct trace_t
{
    /// Determine if plane is NOT valid
    public bool m_AllSolid;
    /// Determine if the start point was in a solid area
    public bool m_StartSolid;
    /// Time completed, 1.0 = didn't hit anything :)
    public float m_Fraction;
    public float m_FractionLeftSolid;
    /// Final trace position
    public Vector3 m_EndPos;
    public cplane_t m_pPlane;
    public int m_Contents;
    public dbrush_t m_pBrush;
    public int m_nBrushSide;

    public static trace_t Create()
    {
        trace_t t = new trace_t();
        t.m_AllSolid = true;
        t.m_StartSolid = true;
        t.m_Fraction = 1f;
        t.m_FractionLeftSolid = 1f;
        t.m_EndPos = Vector3.Zero;
        t.m_Contents = 0;
        t.m_nBrushSide = 0;

        return t;
    }
}

[StructLayout(LayoutKind.Sequential)]
public struct VPlane
{
    public Vector3 m_Origin;
    public float m_Distance;

    public VPlane(Vector3 origin, float dist)
    {
        m_Origin = origin;
        m_Distance = dist;
    }

    public float DistTo(Vector3 location)
    {
        return EngineMath.Vector3Dot(m_Origin, location) - m_Distance;
    }
}