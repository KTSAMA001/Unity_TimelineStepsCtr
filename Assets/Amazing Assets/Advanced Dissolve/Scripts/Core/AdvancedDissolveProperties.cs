using System.Collections.Generic;
using UnityEngine;


namespace AmazingAssets.AdvancedDissolve
{
    static public class AdvancedDissolveProperties
    {
        [System.Serializable]
        public class Cutout
        {
            [System.Serializable]
            public class Standard
            {
                class IDs
                {
                    public int clip;

                    public int map1;
                    public int map1Tiling;
                    public int map1Offset;
                    public int map1Scroll;
                    public int map1Intensity;
                    public int map1Channel;
                    public int map1Invert;

                    public int map2;
                    public int map2Tiling;
                    public int map2Offset;
                    public int map2Scroll;
                    public int map2Intensity;
                    public int map2Channel;
                    public int map2Invert;

                    public int map3;
                    public int map3Tiling;
                    public int map3Offset;
                    public int map3Scroll;
                    public int map3Intensity;
                    public int map3Channel;
                    public int map3Invert;

                    public int mapsBlendType;
                    public int mapsTriplanarMappingSpace;
                    public int mapsScreenSpaceUVScale;
                    public int baseInvert;

                    public IDs(int ID)
                    {
                        string id = ID == 0 ? string.Empty : ("_ID" + ID);

                        clip = Shader.PropertyToID("_AdvancedDissolveCutoutStandardClip" + id);

                        map1 = Shader.PropertyToID("_AdvancedDissolveCutoutStandardMap1" + id);
                        map1Tiling = Shader.PropertyToID("_AdvancedDissolveCutoutStandardMap1Tiling" + id);
                        map1Offset = Shader.PropertyToID("_AdvancedDissolveCutoutStandardMap1Offset" + id);
                        map1Scroll = Shader.PropertyToID("_AdvancedDissolveCutoutStandardMap1Scroll" + id);
                        map1Intensity = Shader.PropertyToID("_AdvancedDissolveCutoutStandardMap1Intensity" + id);
                        map1Channel = Shader.PropertyToID("_AdvancedDissolveCutoutStandardMap1Channel" + id);
                        map1Invert = Shader.PropertyToID("_AdvancedDissolveCutoutStandardMap1Invert" + id);

                        map2 = Shader.PropertyToID("_AdvancedDissolveCutoutStandardMap2" + id);
                        map2Tiling = Shader.PropertyToID("_AdvancedDissolveCutoutStandardMap2Tiling" + id);
                        map2Offset = Shader.PropertyToID("_AdvancedDissolveCutoutStandardMap2Offset" + id);
                        map2Scroll = Shader.PropertyToID("_AdvancedDissolveCutoutStandardMap2Scroll" + id);
                        map2Intensity = Shader.PropertyToID("_AdvancedDissolveCutoutStandardMap2Intensity" + id);
                        map2Channel = Shader.PropertyToID("_AdvancedDissolveCutoutStandardMap2Channel" + id);
                        map2Invert = Shader.PropertyToID("_AdvancedDissolveCutoutStandardMap2Invert" + id);

                        map3 = Shader.PropertyToID("_AdvancedDissolveCutoutStandardMap3" + id);
                        map3Tiling = Shader.PropertyToID("_AdvancedDissolveCutoutStandardMap3Tiling" + id);
                        map3Offset = Shader.PropertyToID("_AdvancedDissolveCutoutStandardMap3Offset" + id);
                        map3Scroll = Shader.PropertyToID("_AdvancedDissolveCutoutStandardMap3Scroll" + id);
                        map3Intensity = Shader.PropertyToID("_AdvancedDissolveCutoutStandardMap3Intensity" + id);
                        map3Channel = Shader.PropertyToID("_AdvancedDissolveCutoutStandardMap3Channel" + id);
                        map3Invert = Shader.PropertyToID("_AdvancedDissolveCutoutStandardMap3Invert" + id);

                        mapsBlendType = Shader.PropertyToID("_AdvancedDissolveCutoutStandardMapsBlendType" + id);
                        mapsTriplanarMappingSpace = Shader.PropertyToID("_AdvancedDissolveCutoutStandardMapsTriplanarMappingSpace" + id);
                        mapsScreenSpaceUVScale = Shader.PropertyToID("_AdvancedDissolveCutoutStandardMapsScreenSpaceUVScale" + id);
                        baseInvert = Shader.PropertyToID("_AdvancedDissolveCutoutStandardBaseInvert" + id);
                    }
                }

                static IDs[] ids = new IDs[] { new IDs(0), new IDs(1), new IDs(2), new IDs(3), new IDs(4) };



                public enum Property
                {
                    Clip,
                    Map1, Map1Tiling, Map1Offset, Map1Scroll, Map1Intensity, Map1Channel, Map1Invert,
                    Map2, Map2Tiling, Map2Offset, Map2Scroll, Map2Intensity, Map2Channel, Map2Invert,
                    Map3, Map3Tiling, Map3Offset, Map3Scroll, Map3Intensity, Map3Channel, Map3Invert,
                    MapsBlendType, TriplanarMappingSpace, ScreenSpaceUVScale, BaseInvert
                }
                public enum MapsBlendType { Multiply, Add }
                public enum TriplanarMappingSpace { World, Local }
                public enum ScreenSpaceUVScale { Constant, CameraRelative }
                public enum MapChannel { Red, Green, Blue, Alpha }



                [Range(0, 1)] public float clip = 0;

                [Space(10)]
                public Texture2D map1;
                public Vector3 map1Tiling = Vector3.one;
                public Vector3 map1Offset = Vector3.zero;
                public Vector3 map1Scroll = Vector3.zero;
                [Range(0, 1)] public float map1Intensity = 1;
                public MapChannel map1Channel = MapChannel.Alpha;
                public bool map1Invert = false;

                [Space(10)]
                public Texture2D map2;
                public Vector3 map2Tiling = Vector3.one;
                public Vector3 map2Offset = Vector3.zero;
                public Vector3 map2Scroll = Vector3.zero;
                [Range(0, 1)] public float map2Intensity = 1;
                public MapChannel map2Channel = MapChannel.Alpha;
                public bool map2Invert = false;

                [Space(10)]
                public Texture2D map3;
                public Vector3 map3Tiling = Vector3.one;
                public Vector3 map3Offset = Vector3.zero;
                public Vector3 map3Scroll = Vector3.zero;
                [Range(0, 1)] public float map3Intensity = 1;
                public MapChannel map3Channel = MapChannel.Alpha;
                public bool map3Invert = false;

                [Space(10)]
                public MapsBlendType mapsBlendType = MapsBlendType.Multiply;
                public TriplanarMappingSpace triplanarMappingSpace = TriplanarMappingSpace.World;
                public ScreenSpaceUVScale screenSpaceUVScale = ScreenSpaceUVScale.Constant;
                public bool baseInvert = false;


                public void UpdateLocal(List<Material> materials)
                {
                    if (materials == null)
                        return;

                    for (int i = 0; i < materials.Count; i++)
                    {
                        UpdateLocal(materials[i]);
                    }
                }
                public void UpdateLocal(Material material)
                {
                    if (material == null)
                        return;

                    UpdateLocalProperty(material, Property.Clip, clip);

                    UpdateLocalProperty(material, Property.Map1, map1);
                    UpdateLocalProperty(material, Property.Map1Tiling, map1Tiling);
                    UpdateLocalProperty(material, Property.Map1Offset, map1Offset);
                    UpdateLocalProperty(material, Property.Map1Scroll, map1Scroll);
                    UpdateLocalProperty(material, Property.Map1Intensity, map1Intensity);
                    UpdateLocalProperty(material, Property.Map1Channel, map1Channel);
                    UpdateLocalProperty(material, Property.Map1Invert, map1Invert);

                    UpdateLocalProperty(material, Property.Map2, map2);
                    UpdateLocalProperty(material, Property.Map2Tiling, map2Tiling);
                    UpdateLocalProperty(material, Property.Map2Offset, map2Offset);
                    UpdateLocalProperty(material, Property.Map2Scroll, map2Scroll);
                    UpdateLocalProperty(material, Property.Map2Intensity, map2Intensity);
                    UpdateLocalProperty(material, Property.Map2Channel, map2Channel);
                    UpdateLocalProperty(material, Property.Map2Invert, map2Invert);

                    UpdateLocalProperty(material, Property.Map3, map3);
                    UpdateLocalProperty(material, Property.Map3Tiling, map3Tiling);
                    UpdateLocalProperty(material, Property.Map3Offset, map3Offset);
                    UpdateLocalProperty(material, Property.Map3Scroll, map3Scroll);
                    UpdateLocalProperty(material, Property.Map3Intensity, map3Intensity);
                    UpdateLocalProperty(material, Property.Map3Channel, map3Channel);
                    UpdateLocalProperty(material, Property.Map3Invert, map3Invert);

                    UpdateLocalProperty(material, Property.MapsBlendType, mapsBlendType);
                    UpdateLocalProperty(material, Property.TriplanarMappingSpace, triplanarMappingSpace);
                    UpdateLocalProperty(material, Property.ScreenSpaceUVScale, screenSpaceUVScale);
                    UpdateLocalProperty(material, Property.BaseInvert, baseInvert);
                }
                public void UpdateGlobal(AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID globalControlID)
                {
                    if (globalControlID == AdvancedDissolveKeywords.GlobalControlID.None)
                        return;

                    UpdateGlobalProperty(globalControlID, Property.Clip, clip);

                    UpdateGlobalProperty(globalControlID, Property.Map1, map1);
                    UpdateGlobalProperty(globalControlID, Property.Map1Tiling, map1Tiling);
                    UpdateGlobalProperty(globalControlID, Property.Map1Offset, map1Offset);
                    UpdateGlobalProperty(globalControlID, Property.Map1Scroll, map1Scroll);
                    UpdateGlobalProperty(globalControlID, Property.Map1Intensity, map1Intensity);
                    UpdateGlobalProperty(globalControlID, Property.Map1Channel, map1Channel);
                    UpdateGlobalProperty(globalControlID, Property.Map1Invert, map1Invert);

                    UpdateGlobalProperty(globalControlID, Property.Map2, map2);
                    UpdateGlobalProperty(globalControlID, Property.Map2Tiling, map2Tiling);
                    UpdateGlobalProperty(globalControlID, Property.Map2Offset, map2Offset);
                    UpdateGlobalProperty(globalControlID, Property.Map2Scroll, map2Scroll);
                    UpdateGlobalProperty(globalControlID, Property.Map2Intensity, map2Intensity);
                    UpdateGlobalProperty(globalControlID, Property.Map2Channel, map2Channel);
                    UpdateGlobalProperty(globalControlID, Property.Map2Invert, map2Invert);

                    UpdateGlobalProperty(globalControlID, Property.Map3, map3);
                    UpdateGlobalProperty(globalControlID, Property.Map3Tiling, map3Tiling);
                    UpdateGlobalProperty(globalControlID, Property.Map3Offset, map3Offset);
                    UpdateGlobalProperty(globalControlID, Property.Map3Scroll, map3Scroll);
                    UpdateGlobalProperty(globalControlID, Property.Map3Intensity, map3Intensity);
                    UpdateGlobalProperty(globalControlID, Property.Map3Channel, map3Channel);
                    UpdateGlobalProperty(globalControlID, Property.Map3Invert, map3Invert);

                    UpdateGlobalProperty(globalControlID, Property.MapsBlendType, mapsBlendType);
                    UpdateGlobalProperty(globalControlID, Property.TriplanarMappingSpace, triplanarMappingSpace);
                    UpdateGlobalProperty(globalControlID, Property.ScreenSpaceUVScale, screenSpaceUVScale);
                    UpdateGlobalProperty(globalControlID, Property.BaseInvert, baseInvert);
                }


                static public void UpdateLocalProperty(Material material, Property property, object value)
                {
                    if (material == null)
                        return;

                    if (value == null && !(property == Property.Map1 || property == Property.Map2 || property == Property.Map3))
                        return;

                    System.Type type = value == null ? null : value.GetType();


                    switch (property)
                    {
                        case Property.Clip:
                            if (type == typeof(float))
                                material.SetFloat(ids[0].clip, (float)value);
                            break;
                        case Property.Map1:
                            if (value == null)
                                material.SetTexture(ids[0].map1, null);
                            else if (type == typeof(Texture2D))
                                material.SetTexture(ids[0].map1, (Texture2D)value);
                            else if (type == typeof(Texture))
                                material.SetTexture(ids[0].map1, (Texture)value);
                            break;
                        case Property.Map1Tiling:
                            if (type == typeof(Vector3))
                                material.SetVector(ids[0].map1Tiling, (Vector3)value);
                            break;
                        case Property.Map1Offset:
                            if (type == typeof(Vector3))
                                material.SetVector(ids[0].map1Offset, (Vector3)value);
                            break;
                        case Property.Map1Scroll:
                            if (type == typeof(Vector3))
                                material.SetVector(ids[0].map1Scroll, (Vector3)value);
                            break;
                        case Property.Map1Intensity:
                            if (type == typeof(float))
                                material.SetFloat(ids[0].map1Intensity, (float)value);
                            break;
                        case Property.Map1Channel:
                            if (type == typeof(MapChannel))
                                material.SetInt(ids[0].map1Channel, (int)value);
                            break;
                        case Property.Map1Invert:
                            if (type == typeof(bool))
                                material.SetInt(ids[0].map1Invert, (bool)value ? 1 : 0);
                            break;
                        case Property.Map2:
                            if (value == null)
                                material.SetTexture(ids[0].map2, null);
                            else if (type == typeof(Texture2D))
                                material.SetTexture(ids[0].map2, (Texture2D)value);
                            else if (type == typeof(Texture))
                                material.SetTexture(ids[0].map2, (Texture)value);
                            break;
                        case Property.Map2Tiling:
                            if (type == typeof(Vector3))
                                material.SetVector(ids[0].map2Tiling, (Vector3)value);
                            break;
                        case Property.Map2Offset:
                            if (type == typeof(Vector3))
                                material.SetVector(ids[0].map2Offset, (Vector3)value);
                            break;
                        case Property.Map2Scroll:
                            if (type == typeof(Vector3))
                                material.SetVector(ids[0].map2Scroll, (Vector3)value);
                            break;
                        case Property.Map2Intensity:
                            if (type == typeof(float))
                                material.SetFloat(ids[0].map2Intensity, (float)value);
                            break;
                        case Property.Map2Channel:
                            if (type == typeof(MapChannel))
                                material.SetInt(ids[0].map2Channel, (int)value);
                            break;
                        case Property.Map2Invert:
                            if (type == typeof(bool))
                                material.SetInt(ids[0].map2Invert, (bool)value ? 1 : 0);
                            break;
                        case Property.Map3:
                            if (value == null)
                                material.SetTexture(ids[0].map3, null);
                            else if (type == typeof(Texture2D))
                                material.SetTexture(ids[0].map3, (Texture2D)value);
                            else if (type == typeof(Texture))
                                material.SetTexture(ids[0].map3, (Texture)value);
                            break;
                        case Property.Map3Tiling:
                            if (type == typeof(Vector3))
                                material.SetVector(ids[0].map3Tiling, (Vector3)value);
                            break;
                        case Property.Map3Offset:
                            if (type == typeof(Vector3))
                                material.SetVector(ids[0].map3Offset, (Vector3)value);
                            break;
                        case Property.Map3Scroll:
                            if (type == typeof(Vector3))
                                material.SetVector(ids[0].map3Scroll, (Vector3)value);
                            break;
                        case Property.Map3Intensity:
                            if (type == typeof(float))
                                material.SetFloat(ids[0].map3Intensity, (float)value);
                            break;
                        case Property.Map3Channel:
                            if (type == typeof(MapChannel))
                                material.SetInt(ids[0].map3Channel, (int)value);
                            break;
                        case Property.Map3Invert:
                            if (type == typeof(bool))
                                material.SetInt(ids[0].map3Invert, (bool)value ? 1 : 0);
                            break;
                        case Property.MapsBlendType:
                            if (type == typeof(MapsBlendType))
                                material.SetInt(ids[0].mapsBlendType, (int)value);
                            break;
                        case Property.TriplanarMappingSpace:
                            if (type == typeof(TriplanarMappingSpace))
                                material.SetInt(ids[0].mapsTriplanarMappingSpace, (int)value);
                            break;
                        case Property.ScreenSpaceUVScale:
                            if (type == typeof(ScreenSpaceUVScale))
                                material.SetInt(ids[0].mapsScreenSpaceUVScale, (int)value);
                            break;
                        case Property.BaseInvert:
                            if (type == typeof(bool))
                                material.SetInt(ids[0].baseInvert, (bool)value ? 1 : 0);
                            break;
                        default:
                            break;
                    }
                }
                static public void UpdateGlobalProperty(AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID globalControlID, Property property, object value)
                {
                    if (globalControlID == AdvancedDissolveKeywords.GlobalControlID.None)
                        return;

                    if (value == null && !(property == Property.Map1 || property == Property.Map2 || property == Property.Map3))
                        return;

                    System.Type type = value == null ? null : value.GetType();


                    switch (property)
                    {
                        case Property.Clip:
                            if (type == typeof(float))
                                Shader.SetGlobalFloat(ids[(int)globalControlID].clip, (float)value);
                            break;
                        case Property.Map1:
                            if (value == null)
                                Shader.SetGlobalTexture(ids[(int)globalControlID].map1, null);
                            else if (type == typeof(Texture2D))
                                Shader.SetGlobalTexture(ids[(int)globalControlID].map1, (Texture2D)value);
                            else if (type == typeof(Texture))
                                Shader.SetGlobalTexture(ids[(int)globalControlID].map1, (Texture)value);
                            break;
                        case Property.Map1Tiling:
                            if (type == typeof(Vector3))
                                Shader.SetGlobalVector(ids[(int)globalControlID].map1Tiling, (Vector3)value);
                            break;
                        case Property.Map1Offset:
                            if (type == typeof(Vector3))
                                Shader.SetGlobalVector(ids[(int)globalControlID].map1Offset, (Vector3)value);
                            break;
                        case Property.Map1Scroll:
                            if (type == typeof(Vector3))
                                Shader.SetGlobalVector(ids[(int)globalControlID].map1Scroll, (Vector3)value);
                            break;
                        case Property.Map1Intensity:
                            if (type == typeof(float))
                                Shader.SetGlobalFloat(ids[(int)globalControlID].map1Intensity, (float)value);
                            break;
                        case Property.Map1Channel:
                            if (type == typeof(MapChannel))
                                Shader.SetGlobalInt(ids[(int)globalControlID].map1Channel, (int)value);
                            break;
                        case Property.Map1Invert:
                            if (type == typeof(bool))
                                Shader.SetGlobalInt(ids[(int)globalControlID].map1Invert, (bool)value ? 1 : 0);
                            break;
                        case Property.Map2:
                            if (value == null)
                                Shader.SetGlobalTexture(ids[(int)globalControlID].map2, null);
                            else if (type == typeof(Texture2D))
                                Shader.SetGlobalTexture(ids[(int)globalControlID].map2, (Texture2D)value);
                            else if (type == typeof(Texture))
                                Shader.SetGlobalTexture(ids[(int)globalControlID].map2, (Texture)value);
                            break;
                        case Property.Map2Tiling:
                            if (type == typeof(Vector3))
                                Shader.SetGlobalVector(ids[(int)globalControlID].map2Tiling, (Vector3)value);
                            break;
                        case Property.Map2Offset:
                            if (type == typeof(Vector3))
                                Shader.SetGlobalVector(ids[(int)globalControlID].map2Offset, (Vector3)value);
                            break;
                        case Property.Map2Scroll:
                            if (type == typeof(Vector3))
                                Shader.SetGlobalVector(ids[(int)globalControlID].map2Scroll, (Vector3)value);
                            break;
                        case Property.Map2Intensity:
                            if (type == typeof(float))
                                Shader.SetGlobalFloat(ids[(int)globalControlID].map2Intensity, (float)value);
                            break;
                        case Property.Map2Channel:
                            if (type == typeof(MapChannel))
                                Shader.SetGlobalInt(ids[(int)globalControlID].map2Channel, (int)value);
                            break;
                        case Property.Map2Invert:
                            if (type == typeof(bool))
                                Shader.SetGlobalInt(ids[(int)globalControlID].map2Invert, (bool)value ? 1 : 0);
                            break;
                        case Property.Map3:
                            if (value == null)
                                Shader.SetGlobalTexture(ids[(int)globalControlID].map3, null);
                            else if (type == typeof(Texture2D))
                                Shader.SetGlobalTexture(ids[(int)globalControlID].map3, (Texture2D)value);
                            else if (type == typeof(Texture))
                                Shader.SetGlobalTexture(ids[(int)globalControlID].map3, (Texture)value);
                            break;
                        case Property.Map3Tiling:
                            if (type == typeof(Vector3))
                                Shader.SetGlobalVector(ids[(int)globalControlID].map3Tiling, (Vector3)value);
                            break;
                        case Property.Map3Offset:
                            if (type == typeof(Vector3))
                                Shader.SetGlobalVector(ids[(int)globalControlID].map3Offset, (Vector3)value);
                            break;
                        case Property.Map3Scroll:
                            if (type == typeof(Vector3))
                                Shader.SetGlobalVector(ids[(int)globalControlID].map3Scroll, (Vector3)value);
                            break;
                        case Property.Map3Intensity:
                            if (type == typeof(float))
                                Shader.SetGlobalFloat(ids[(int)globalControlID].map3Intensity, (float)value);
                            break;
                        case Property.Map3Channel:
                            if (type == typeof(MapChannel))
                                Shader.SetGlobalInt(ids[(int)globalControlID].map3Channel, (int)value);
                            break;
                        case Property.Map3Invert:
                            if (type == typeof(bool))
                                Shader.SetGlobalInt(ids[(int)globalControlID].map3Invert, (bool)value ? 1 : 0);
                            break;
                        case Property.MapsBlendType:
                            if (type == typeof(MapsBlendType))
                                Shader.SetGlobalInt(ids[(int)globalControlID].mapsBlendType, (int)value);
                            break;
                        case Property.TriplanarMappingSpace:
                            if (type == typeof(TriplanarMappingSpace))
                                Shader.SetGlobalInt(ids[(int)globalControlID].mapsTriplanarMappingSpace, (int)value);
                            break;
                        case Property.ScreenSpaceUVScale:
                            if (type == typeof(ScreenSpaceUVScale))
                                Shader.SetGlobalInt(ids[(int)globalControlID].mapsScreenSpaceUVScale, (int)value);
                            break;
                        case Property.BaseInvert:
                            if (type == typeof(bool))
                                Shader.SetGlobalInt(ids[(int)globalControlID].baseInvert, (bool)value ? 1 : 0);
                            break;
                        default:
                            break;
                    }

                }
            }

            [System.Serializable]
            public class Geometric
            {
                class IDs
                {
                    public int invert;
                    public int noise;

                    public int xyzAxis;
                    public int xyzStyle;
                    public int xyzSpace;
                    public int xyzRollout;
                    public int xyzPivotPointPosition;

                    public int position1;
                    public int normal1;
                    public int radius1;
                    public int height1;
                    public int size1;
                    public int matrixTRS1;

                    public int position2;
                    public int normal2;
                    public int radius2;
                    public int height2;
                    public int size2;
                    public int matrixTRS2;

                    public int position3;
                    public int normal3;
                    public int radius3;
                    public int height3;
                    public int size3;
                    public int matrixTRS3;

                    public int position4;
                    public int normal4;
                    public int radius4;
                    public int height4;
                    public int size4;
                    public int matrixTRS4;
                    

                    public IDs(int ID)
                    {
                        string id = ID == 0 ? string.Empty : ("_ID" + ID);


                        invert = Shader.PropertyToID("_AdvancedDissolveCutoutGeometricInvert" + id);
                        noise = Shader.PropertyToID("_AdvancedDissolveCutoutGeometricNoise" + id);

                        xyzAxis = Shader.PropertyToID("_AdvancedDissolveCutoutGeometricXYZAxis" + id);
                        xyzStyle = Shader.PropertyToID("_AdvancedDissolveCutoutGeometricXYZStyle" + id);
                        xyzSpace = Shader.PropertyToID("_AdvancedDissolveCutoutGeometricXYZSpace" + id);
                        xyzRollout = Shader.PropertyToID("_AdvancedDissolveCutoutGeometricXYZRollout" + id);
                        xyzPivotPointPosition = Shader.PropertyToID("_AdvancedDissolveCutoutGeometricXYZPosition" + id);

                        position1 = Shader.PropertyToID("_AdvancedDissolveCutoutGeometric1Position" + id);
                        normal1 = Shader.PropertyToID("_AdvancedDissolveCutoutGeometric1Normal" + id);
                        radius1 = Shader.PropertyToID("_AdvancedDissolveCutoutGeometric1Radius" + id);
                        height1 = Shader.PropertyToID("_AdvancedDissolveCutoutGeometric1Height" + id);
                        size1 = Shader.PropertyToID("_AdvancedDissolveCutoutGeometric1Size" + id);
                        matrixTRS1 = Shader.PropertyToID("_AdvancedDissolveCutoutGeometric1MatrixTRS" + id);

                        position2 = Shader.PropertyToID("_AdvancedDissolveCutoutGeometric2Position" + id);
                        normal2 = Shader.PropertyToID("_AdvancedDissolveCutoutGeometric2Normal" + id);
                        radius2 = Shader.PropertyToID("_AdvancedDissolveCutoutGeometric2Radius" + id);
                        height2 = Shader.PropertyToID("_AdvancedDissolveCutoutGeometric2Height" + id);
                        size2 = Shader.PropertyToID("_AdvancedDissolveCutoutGeometric2Size" + id);
                        matrixTRS2 = Shader.PropertyToID("_AdvancedDissolveCutoutGeometric2MatrixTRS" + id);

                        position3 = Shader.PropertyToID("_AdvancedDissolveCutoutGeometric3Position" + id);
                        normal3 = Shader.PropertyToID("_AdvancedDissolveCutoutGeometric3Normal" + id);
                        radius3 = Shader.PropertyToID("_AdvancedDissolveCutoutGeometric3Radius" + id);
                        height3 = Shader.PropertyToID("_AdvancedDissolveCutoutGeometric3Height" + id);
                        size3 = Shader.PropertyToID("_AdvancedDissolveCutoutGeometric3Size" + id);
                        matrixTRS3 = Shader.PropertyToID("_AdvancedDissolveCutoutGeometric3MatrixTRS" + id);

                        position4 = Shader.PropertyToID("_AdvancedDissolveCutoutGeometric4Position" + id);
                        normal4 = Shader.PropertyToID("_AdvancedDissolveCutoutGeometric4Normal" + id);
                        radius4 = Shader.PropertyToID("_AdvancedDissolveCutoutGeometric4Radius" + id);
                        height4 = Shader.PropertyToID("_AdvancedDissolveCutoutGeometric4Height" + id);
                        size4 = Shader.PropertyToID("_AdvancedDissolveCutoutGeometric4Size" + id);
                        matrixTRS4 = Shader.PropertyToID("_AdvancedDissolveCutoutGeometric4MatrixTRS" + id);                        
                    }
                }

                static IDs[] ids = new IDs[] { new IDs(0), new IDs(1), new IDs(2), new IDs(3), new IDs(4) };


                public enum Property
                {
                    XYZAxis, XYZStyle, XYZSpace, XYZRollout, XYZPosition,
                    Position1, Normal1, Radius1, Height1, Size1, MatrixTRS1,
                    Position2, Normal2, Radius2, Height2, Size2, MatrixTRS2,
                    Position3, Normal3, Radius3, Height3, Size3, MatrixTRS3,
                    Position4, Normal4, Radius4, Height4, Size4, MatrixTRS4,
                    Invert, Noise
                }
                public enum XYZAxis { X, Y, Z }
                public enum XYZStyle { Linear, Rollout }
                public enum XYZSpace { World, Local }



                static void UpdateLocal(Material material, Property property, object value)
                {
                    if (value == null || material == null)
                        return;

                    System.Type type = value.GetType();


                    switch (property)
                    {
                        case Geometric.Property.XYZAxis:
                            if (type == typeof(XYZAxis))
                                material.SetInt(ids[0].xyzAxis, (int)value);
                            break;
                        case Geometric.Property.XYZStyle:
                            if (type == typeof(XYZStyle))
                                material.SetInt(ids[0].xyzStyle, (int)value);
                            break;
                        case Geometric.Property.XYZSpace:
                            if (type == typeof(XYZSpace))
                                material.SetInt(ids[0].xyzSpace, (int)value);
                            break;
                        case Geometric.Property.XYZRollout:
                            if (type == typeof(float))
                                material.SetFloat(ids[0].xyzRollout, Mathf.Abs((float)value));
                            break;
                        case Geometric.Property.XYZPosition:
                            if (type == typeof(Vector3))
                                material.SetVector(ids[0].xyzPivotPointPosition, (Vector3)value);
                            break;
                        case Geometric.Property.Position1:
                            if (type == typeof(Vector3))
                                material.SetVector(ids[0].position1, (Vector3)value);
                            break;
                        case Geometric.Property.Normal1:
                            if (type == typeof(Vector3))
                                material.SetVector(ids[0].normal1, (Vector3)value);
                            break;
                        case Geometric.Property.Radius1:
                            if (type == typeof(float))
                                material.SetFloat(ids[0].radius1, (float)value);
                            break;
                        case Geometric.Property.Height1:
                            if (type == typeof(float))
                                material.SetFloat(ids[0].height1, (float)value);
                            break;
                        case Geometric.Property.Size1:
                            if (type == typeof(Vector3))
                                material.SetVector(ids[0].size1, (Vector3)value);
                            break;
                        case Geometric.Property.MatrixTRS1:
                            if (type == typeof(Matrix4x4))
                                material.SetMatrix(ids[0].matrixTRS1, (Matrix4x4)value);
                            break;
                        case Geometric.Property.Position2:
                            if (type == typeof(Vector3))
                                material.SetVector(ids[0].position2, (Vector3)value);
                            break;
                        case Geometric.Property.Normal2:
                            if (type == typeof(Vector3))
                                material.SetVector(ids[0].normal2, (Vector3)value);
                            break;
                        case Geometric.Property.Radius2:
                            if (type == typeof(float))
                                material.SetFloat(ids[0].radius2, (float)value);
                            break;
                        case Geometric.Property.Height2:
                            if (type == typeof(float))
                                material.SetFloat(ids[0].height2, (float)value);
                            break;
                        case Geometric.Property.Size2:
                            if (type == typeof(Vector3))
                                material.SetVector(ids[0].size2, (Vector3)value);
                            break;
                        case Geometric.Property.MatrixTRS2:
                            if (type == typeof(Matrix4x4))
                                material.SetMatrix(ids[0].matrixTRS2, (Matrix4x4)value);
                            break;
                        case Geometric.Property.Position3:
                            if (type == typeof(Vector3))
                                material.SetVector(ids[0].position3, (Vector3)value);
                            break;
                        case Geometric.Property.Normal3:
                            if (type == typeof(Vector3))
                                material.SetVector(ids[0].normal3, (Vector3)value);
                            break;
                        case Geometric.Property.Radius3:
                            if (type == typeof(float))
                                material.SetFloat(ids[0].radius3, (float)value);
                            break;
                        case Geometric.Property.Height3:
                            if (type == typeof(float))
                                material.SetFloat(ids[0].height3, (float)value);
                            break;
                        case Geometric.Property.Size3:
                            if (type == typeof(Vector3))
                                material.SetVector(ids[0].size3, (Vector3)value);
                            break;
                        case Geometric.Property.MatrixTRS3:
                            if (type == typeof(Matrix4x4))
                                material.SetMatrix(ids[0].matrixTRS3, (Matrix4x4)value);
                            break;
                        case Geometric.Property.Position4:
                            if (type == typeof(Vector3))
                                material.SetVector(ids[0].position4, (Vector3)value);
                            break;
                        case Geometric.Property.Normal4:
                            if (type == typeof(Vector3))
                                material.SetVector(ids[0].normal4, (Vector3)value);
                            break;
                        case Geometric.Property.Radius4:
                            if (type == typeof(float))
                                material.SetFloat(ids[0].radius4, (float)value);
                            break;
                        case Geometric.Property.Height4:
                            if (type == typeof(float))
                                material.SetFloat(ids[0].height4, (float)value);
                            break;
                        case Geometric.Property.Size4:
                            if (type == typeof(Vector3))
                                material.SetVector(ids[0].size4, (Vector3)value);
                            break;
                        case Geometric.Property.MatrixTRS4:
                            if (type == typeof(Matrix4x4))
                                material.SetMatrix(ids[0].matrixTRS4, (Matrix4x4)value);
                            break;
                        case Geometric.Property.Invert:
                            if (type == typeof(bool))
                                material.SetInt(ids[0].invert, (bool)value ? 1 : 0);
                            break;
                        case Geometric.Property.Noise:
                            if (type == typeof(float))
                                material.SetFloat(ids[0].noise, (float)value);
                            break;
                        default:
                            break;
                    }
                }
                static void UpdateGlobal(AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID globalControlID, Property property, object value)
                {
                    if (value == null || globalControlID == AdvancedDissolveKeywords.GlobalControlID.None)
                        return;

                    System.Type type = value.GetType();


                    switch (property)
                    {
                        case Geometric.Property.XYZAxis:
                            if (type == typeof(XYZAxis))
                                Shader.SetGlobalInt(ids[(int)globalControlID].xyzAxis, (int)value);
                            break;
                        case Geometric.Property.XYZStyle:
                            if (type == typeof(XYZStyle))
                                Shader.SetGlobalInt(ids[(int)globalControlID].xyzStyle, (int)value);
                            break;
                        case Geometric.Property.XYZSpace:
                            if (type == typeof(XYZSpace))
                                Shader.SetGlobalInt(ids[(int)globalControlID].xyzSpace, (int)value);
                            break;
                        case Geometric.Property.XYZRollout:
                            if (type == typeof(float))
                                Shader.SetGlobalFloat(ids[(int)globalControlID].xyzRollout, Mathf.Abs((float)value));
                            break;
                        case Geometric.Property.XYZPosition:
                            if (type == typeof(Vector3))
                                Shader.SetGlobalVector(ids[(int)globalControlID].xyzPivotPointPosition, (Vector3)value);
                            break;
                        case Geometric.Property.Position1:
                            if (type == typeof(Vector3))
                                Shader.SetGlobalVector(ids[(int)globalControlID].position1, (Vector3)value);
                            break;
                        case Geometric.Property.Normal1:
                            if (type == typeof(Vector3))
                                Shader.SetGlobalVector(ids[(int)globalControlID].normal1, (Vector3)value);
                            break;
                        case Geometric.Property.Radius1:
                            if (type == typeof(float))
                                Shader.SetGlobalFloat(ids[(int)globalControlID].radius1, (float)value);
                            break;
                        case Geometric.Property.Height1:
                            if (type == typeof(float))
                                Shader.SetGlobalFloat(ids[(int)globalControlID].height1, (float)value);
                            break;
                        case Geometric.Property.Size1:
                            if (type == typeof(Vector3))
                                Shader.SetGlobalVector(ids[(int)globalControlID].size1, (Vector3)value);
                            break;
                        case Geometric.Property.MatrixTRS1:
                            if (type == typeof(Matrix4x4))
                                Shader.SetGlobalMatrix(ids[(int)globalControlID].matrixTRS1, (Matrix4x4)value);
                            break;
                        case Geometric.Property.Position2:
                            if (type == typeof(Vector3))
                                Shader.SetGlobalVector(ids[(int)globalControlID].position2, (Vector3)value);
                            break;
                        case Geometric.Property.Normal2:
                            if (type == typeof(Vector3))
                                Shader.SetGlobalVector(ids[(int)globalControlID].normal2, (Vector3)value);
                            break;
                        case Geometric.Property.Radius2:
                            if (type == typeof(float))
                                Shader.SetGlobalFloat(ids[(int)globalControlID].radius2, (float)value);
                            break;
                        case Geometric.Property.Height2:
                            if (type == typeof(float))
                                Shader.SetGlobalFloat(ids[(int)globalControlID].height2, (float)value);
                            break;
                        case Geometric.Property.Size2:
                            if (type == typeof(Vector3))
                                Shader.SetGlobalVector(ids[(int)globalControlID].size2, (Vector3)value);
                            break;
                        case Geometric.Property.MatrixTRS2:
                            if (type == typeof(Matrix4x4))
                                Shader.SetGlobalMatrix(ids[(int)globalControlID].matrixTRS2, (Matrix4x4)value);
                            break;
                        case Geometric.Property.Position3:
                            if (type == typeof(Vector3))
                                Shader.SetGlobalVector(ids[(int)globalControlID].position3, (Vector3)value);
                            break;
                        case Geometric.Property.Normal3:
                            if (type == typeof(Vector3))
                                Shader.SetGlobalVector(ids[(int)globalControlID].normal3, (Vector3)value);
                            break;
                        case Geometric.Property.Radius3:
                            if (type == typeof(float))
                                Shader.SetGlobalFloat(ids[(int)globalControlID].radius3, (float)value);
                            break;
                        case Geometric.Property.Height3:
                            if (type == typeof(float))
                                Shader.SetGlobalFloat(ids[(int)globalControlID].height3, (float)value);
                            break;
                        case Geometric.Property.Size3:
                            if (type == typeof(Vector3))
                                Shader.SetGlobalVector(ids[(int)globalControlID].size3, (Vector3)value);
                            break;
                        case Geometric.Property.MatrixTRS3:
                            if (type == typeof(Matrix4x4))
                                Shader.SetGlobalMatrix(ids[(int)globalControlID].matrixTRS3, (Matrix4x4)value);
                            break;
                        case Geometric.Property.Position4:
                            if (type == typeof(Vector3))
                                Shader.SetGlobalVector(ids[(int)globalControlID].position4, (Vector3)value);
                            break;
                        case Geometric.Property.Normal4:
                            if (type == typeof(Vector3))
                                Shader.SetGlobalVector(ids[(int)globalControlID].normal4, (Vector3)value);
                            break;
                        case Geometric.Property.Radius4:
                            if (type == typeof(float))
                                Shader.SetGlobalFloat(ids[(int)globalControlID].radius4, (float)value);
                            break;
                        case Geometric.Property.Height4:
                            if (type == typeof(float))
                                Shader.SetGlobalFloat(ids[(int)globalControlID].height4, (float)value);
                            break;
                        case Geometric.Property.Size4:
                            if (type == typeof(Vector3))
                                Shader.SetGlobalVector(ids[(int)globalControlID].size4, (Vector3)value);
                            break;
                        case Geometric.Property.MatrixTRS4:
                            if (type == typeof(Matrix4x4))
                                Shader.SetGlobalMatrix(ids[(int)globalControlID].matrixTRS4, (Matrix4x4)value);
                            break;
                        case Geometric.Property.Invert:
                            if (type == typeof(bool))
                                Shader.SetGlobalInt(ids[(int)globalControlID].invert, (bool)value ? 1 : 0);
                            break;
                        case Geometric.Property.Noise:
                            if (type == typeof(float))
                                Shader.SetGlobalFloat(ids[(int)globalControlID].noise, (float)value);
                            break;
                        default:
                            break;
                    }
                }


                static public class UpdateLocalProperty
                {
                    static public void XYZAxis(Material material, XYZAxis xyzAxis, XYZStyle xyzStyle, XYZSpace xyzSpace, float axisRollout, Vector3 axisPosition)
                    {
                        UpdateLocal(material, Property.XYZAxis, xyzAxis);
                        UpdateLocal(material, Property.XYZStyle, xyzStyle);
                        UpdateLocal(material, Property.XYZSpace, xyzSpace);
                        UpdateLocal(material, Property.XYZRollout, axisRollout);
                        UpdateLocal(material, Property.XYZPosition, axisPosition);
                    }
                    static public void Plane(Material material, AdvancedDissolveKeywords.CutoutGeometricCount countID, Vector3 position, Vector3 normal)
                    {
                        switch (countID)
                        {
                            case AdvancedDissolveKeywords.CutoutGeometricCount.One:
                                UpdateLocal(material, Property.Position1, position);
                                UpdateLocal(material, Property.Normal1, normal);
                                break;
                            case AdvancedDissolveKeywords.CutoutGeometricCount.Two:
                                UpdateLocal(material, Property.Position2, position);
                                UpdateLocal(material, Property.Normal2, normal);
                                break;
                            case AdvancedDissolveKeywords.CutoutGeometricCount.Three:
                                UpdateLocal(material, Property.Position3, position);
                                UpdateLocal(material, Property.Normal3, normal);
                                break;
                            case AdvancedDissolveKeywords.CutoutGeometricCount.Four:
                                UpdateLocal(material, Property.Position4, position);
                                UpdateLocal(material, Property.Normal4, normal);
                                break;
                            default:
                                break;
                        }
                        
                    }
                    static public void Sphere(Material material, AdvancedDissolveKeywords.CutoutGeometricCount countID, Vector3 position, float radius)
                    {
                        switch (countID)
                        {
                            case AdvancedDissolveKeywords.CutoutGeometricCount.One:
                                UpdateLocal(material, Property.Position1, position);
                                UpdateLocal(material, Property.Radius1, radius);
                                break;
                            case AdvancedDissolveKeywords.CutoutGeometricCount.Two:
                                UpdateLocal(material, Property.Position2, position);
                                UpdateLocal(material, Property.Radius2, radius);
                                break;
                            case AdvancedDissolveKeywords.CutoutGeometricCount.Three:
                                UpdateLocal(material, Property.Position3, position);
                                UpdateLocal(material, Property.Radius3, radius);
                                break;
                            case AdvancedDissolveKeywords.CutoutGeometricCount.Four:
                                UpdateLocal(material, Property.Position4, position);
                                UpdateLocal(material, Property.Radius4, radius);
                                break;
                            default:
                                break;
                        }

                    }
                    static public void Cube(Material material, AdvancedDissolveKeywords.CutoutGeometricCount countID, Vector3 position, Quaternion rotation, Vector3 size)
                    {
                        size *= 0.5f;
                        Matrix4x4 TRS = Matrix4x4.TRS(position, rotation, Vector3.one).inverse;

                        switch (countID)
                        {
                            case AdvancedDissolveKeywords.CutoutGeometricCount.One:
                                UpdateLocal(material, Property.Size1, size);
                                UpdateLocal(material, Property.MatrixTRS1, TRS);
                                break;
                            case AdvancedDissolveKeywords.CutoutGeometricCount.Two:
                                UpdateLocal(material, Property.Size2, size);
                                UpdateLocal(material, Property.MatrixTRS2, TRS);
                                break;
                            case AdvancedDissolveKeywords.CutoutGeometricCount.Three:
                                UpdateLocal(material, Property.Size3, size);
                                UpdateLocal(material, Property.MatrixTRS3, TRS);
                                break;
                            case AdvancedDissolveKeywords.CutoutGeometricCount.Four:
                                UpdateLocal(material, Property.Size4, size);
                                UpdateLocal(material, Property.MatrixTRS4, TRS);
                                break;
                            default:
                                break;
                        }
                    }
                    static public void Capsule(Material material, AdvancedDissolveKeywords.CutoutGeometricCount countID, Vector3 startPosition, Vector3 endPosition, float radius)
                    {
                        float height = Vector3.Distance(startPosition, endPosition);
                        Vector3 normal = Vector3.Normalize(endPosition - startPosition);

                        switch (countID)
                        {
                            case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.One:
                                UpdateLocal(material, Geometric.Property.Position1, startPosition);
                                UpdateLocal(material, Geometric.Property.Normal1, normal);
                                UpdateLocal(material, Geometric.Property.Radius1, radius);
                                UpdateLocal(material, Geometric.Property.Height1, height);
                                break;

                            case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Two:
                                UpdateLocal(material, Geometric.Property.Position2, startPosition);
                                UpdateLocal(material, Geometric.Property.Normal2, normal);
                                UpdateLocal(material, Geometric.Property.Radius2, radius);
                                UpdateLocal(material, Geometric.Property.Height2, height);
                                break;

                            case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Three:
                                UpdateLocal(material, Geometric.Property.Position3, startPosition);
                                UpdateLocal(material, Geometric.Property.Normal3, normal);
                                UpdateLocal(material, Geometric.Property.Radius3, radius);
                                UpdateLocal(material, Geometric.Property.Height3, height);
                                break;

                            case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Four:
                                UpdateLocal(material, Geometric.Property.Position4, startPosition);
                                UpdateLocal(material, Geometric.Property.Normal4, normal);
                                UpdateLocal(material, Geometric.Property.Radius4, radius);
                                UpdateLocal(material, Geometric.Property.Height4, height);
                                break;

                            default:
                                break;
                        }
                    }
                    static public void ConeSmooth(Material material, AdvancedDissolveKeywords.CutoutGeometricCount countID, Vector3 startPosition, Vector3 endPosition, float radius)
                    {
                        //Same as Capsule
                        Capsule(material, countID, startPosition, endPosition, radius);
                    }

                    static public void Invert(Material material, bool invert)
                    {
                        UpdateLocal(material, Property.Invert, invert);
                    }
                    static public void Noise(Material material, float noise)
                    {
                        UpdateLocal(material, Property.Noise, noise);
                    }
                }

                static public class UpdateGlobalProperty
                {
                    static public void XYZAxis(AdvancedDissolveKeywords.GlobalControlID globalControlID, XYZAxis xyzAxis, XYZStyle xyzStyle, XYZSpace xyzSpace, float axisRollout, Vector3 position)
                    {
                        UpdateGlobal(globalControlID, Property.XYZAxis, xyzAxis);
                        UpdateGlobal(globalControlID, Property.XYZStyle, xyzStyle);
                        UpdateGlobal(globalControlID, Property.XYZSpace, xyzSpace);
                        UpdateGlobal(globalControlID, Property.XYZRollout, axisRollout);
                        UpdateGlobal(globalControlID, Property.XYZPosition, position);
                    }
                    static public void Plane(AdvancedDissolveKeywords.GlobalControlID globalControlID, AdvancedDissolveKeywords.CutoutGeometricCount countID, Vector3 position, Vector3 normal)
                    {
                        switch (countID)
                        {
                            case AdvancedDissolveKeywords.CutoutGeometricCount.One:
                                UpdateGlobal(globalControlID, Property.Position1, position);
                                UpdateGlobal(globalControlID, Property.Normal1, normal);
                                break;
                            case AdvancedDissolveKeywords.CutoutGeometricCount.Two:
                                UpdateGlobal(globalControlID, Property.Position2, position);
                                UpdateGlobal(globalControlID, Property.Normal2, normal);
                                break;
                            case AdvancedDissolveKeywords.CutoutGeometricCount.Three:
                                UpdateGlobal(globalControlID, Property.Position3, position);
                                UpdateGlobal(globalControlID, Property.Normal3, normal);
                                break;
                            case AdvancedDissolveKeywords.CutoutGeometricCount.Four:
                                UpdateGlobal(globalControlID, Property.Position4, position);
                                UpdateGlobal(globalControlID, Property.Normal4, normal);
                                break;
                            default:
                                break;
                        }

                    }
                    static public void Sphere(AdvancedDissolveKeywords.GlobalControlID globalControlID, AdvancedDissolveKeywords.CutoutGeometricCount countID, Vector3 position, float radius)
                    {
                        switch (countID)
                        {
                            case AdvancedDissolveKeywords.CutoutGeometricCount.One:
                                UpdateGlobal(globalControlID, Property.Position1, position);
                                UpdateGlobal(globalControlID, Property.Radius1, radius);
                                break;
                            case AdvancedDissolveKeywords.CutoutGeometricCount.Two:
                                UpdateGlobal(globalControlID, Property.Position2, position);
                                UpdateGlobal(globalControlID, Property.Radius2, radius);
                                break;
                            case AdvancedDissolveKeywords.CutoutGeometricCount.Three:
                                UpdateGlobal(globalControlID, Property.Position3, position);
                                UpdateGlobal(globalControlID, Property.Radius3, radius);
                                break;
                            case AdvancedDissolveKeywords.CutoutGeometricCount.Four:
                                UpdateGlobal(globalControlID, Property.Position4, position);
                                UpdateGlobal(globalControlID, Property.Radius4, radius);
                                break;
                            default:
                                break;
                        }

                    }
                    static public void Cube(AdvancedDissolveKeywords.GlobalControlID globalControlID, AdvancedDissolveKeywords.CutoutGeometricCount countID, Vector3 position, Quaternion rotation, Vector3 size)
                    {
                        size *= 0.5f;
                        Matrix4x4 TRS = Matrix4x4.TRS(position, rotation, Vector3.one).inverse;

                        switch (countID)
                        {
                            case AdvancedDissolveKeywords.CutoutGeometricCount.One:
                                UpdateGlobal(globalControlID, Property.Size1, size);
                                UpdateGlobal(globalControlID, Property.MatrixTRS1, TRS);
                                break;
                            case AdvancedDissolveKeywords.CutoutGeometricCount.Two:
                                UpdateGlobal(globalControlID, Property.Size2, size);
                                UpdateGlobal(globalControlID, Property.MatrixTRS2, TRS);
                                break;
                            case AdvancedDissolveKeywords.CutoutGeometricCount.Three:
                                UpdateGlobal(globalControlID, Property.Size3, size);
                                UpdateGlobal(globalControlID, Property.MatrixTRS3, TRS);
                                break;
                            case AdvancedDissolveKeywords.CutoutGeometricCount.Four:
                                UpdateGlobal(globalControlID, Property.Size4, size);
                                UpdateGlobal(globalControlID, Property.MatrixTRS4, TRS);
                                break;
                            default:
                                break;
                        }
                    }
                    static public void Capsule(AdvancedDissolveKeywords.GlobalControlID globalControlID, AdvancedDissolveKeywords.CutoutGeometricCount countID, Vector3 startPosition, Vector3 endPosition, float radius)
                    {
                        float height = Vector3.Distance(startPosition, endPosition);
                        Vector3 normal = Vector3.Normalize(endPosition - startPosition);


                        switch (countID)
                        {
                            case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.One:
                                UpdateGlobal(globalControlID, Geometric.Property.Position1, startPosition);
                                UpdateGlobal(globalControlID, Geometric.Property.Normal1, normal);
                                UpdateGlobal(globalControlID, Geometric.Property.Radius1, radius);
                                UpdateGlobal(globalControlID, Geometric.Property.Height1, height);
                                break;

                            case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Two:
                                UpdateGlobal(globalControlID, Geometric.Property.Position2, startPosition);
                                UpdateGlobal(globalControlID, Geometric.Property.Normal2, normal);
                                UpdateGlobal(globalControlID, Geometric.Property.Radius2, radius);
                                UpdateGlobal(globalControlID, Geometric.Property.Height2, height);
                                break;

                            case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Three:
                                UpdateGlobal(globalControlID, Geometric.Property.Position3, startPosition);
                                UpdateGlobal(globalControlID, Geometric.Property.Normal3, normal);
                                UpdateGlobal(globalControlID, Geometric.Property.Radius3, radius);
                                UpdateGlobal(globalControlID, Geometric.Property.Height3, height);
                                break;

                            case AdvancedDissolve.AdvancedDissolveKeywords.CutoutGeometricCount.Four:
                                UpdateGlobal(globalControlID, Geometric.Property.Position4, startPosition);
                                UpdateGlobal(globalControlID, Geometric.Property.Normal4, normal);
                                UpdateGlobal(globalControlID, Geometric.Property.Radius4, radius);
                                UpdateGlobal(globalControlID, Geometric.Property.Height4, height);
                                break;

                            default:
                                break;
                        }
                    }
                    static public void ConeSmooth(AdvancedDissolveKeywords.GlobalControlID globalControlID, AdvancedDissolveKeywords.CutoutGeometricCount countID, Vector3 startPosition, Vector3 endPosition, float radius)
                    {
                        //Same as Capsule
                        Capsule(globalControlID, countID, startPosition, endPosition, radius);
                    }

                    static public void Invert(AdvancedDissolveKeywords.GlobalControlID globalControlID, bool invert)
                    {
                        UpdateGlobal(globalControlID, Property.Invert, invert);
                    }
                    static public void Noise(AdvancedDissolveKeywords.GlobalControlID globalControlID, float noise)
                    {
                        UpdateGlobal(globalControlID, Property.Noise, noise);
                    }
                }
            }
        }


        [System.Serializable]
        public class Edge
        {
            [System.Serializable]
            public class Base
            {
                class IDs
                {
                    public int widthStandard;
                    public int widthGeometric;
                    public int shape;

                    public int color;
                    public int colorTransparency;
                    public int colorIntensity;

                    public IDs(int ID)
                    {
                        string id = ID == 0 ? string.Empty : ("_ID" + ID);

                        widthStandard = Shader.PropertyToID("_AdvancedDissolveEdgeBaseWidthStandard" + id);
                        widthGeometric = Shader.PropertyToID("_AdvancedDissolveEdgeBaseWidthGeometric" + id);
                        shape = Shader.PropertyToID("_AdvancedDissolveEdgeBaseShape" + id);

                        color = Shader.PropertyToID("_AdvancedDissolveEdgeBaseColor" + id);
                        colorTransparency = Shader.PropertyToID("_AdvancedDissolveEdgeBaseColorTransparency" + id);
                        colorIntensity = Shader.PropertyToID("_AdvancedDissolveEdgeBaseColorIntensity" + id);
                    }
                }

                static IDs[] ids = new IDs[] { new IDs(0), new IDs(1), new IDs(2), new IDs(3), new IDs(4) };



                public enum Property
                {
                    WidthStandard, WidthGeometric, Shape,
                    Color, ColorTransparency, ColorIntensity                   
                }
                public enum Shape { Solid, Smooth, Smoother }



                [Range(0, 1)] public float widthCutoutStandard = 0.1f;
                [Range(0, 1)] public float widthCutoutGeometric = 0.1f;
                public Shape shape;

                [Space(10)]
                [ColorUsage(false)] public Color color = Color.green;
                [Range(0, 1)] public float colorTransparency = 1;
                public float colorIntensity = 0;

               

                public void UpdateLocal(List<Material> materials)
                {
                    if (materials == null)
                        return;

                    for (int i = 0; i < materials.Count; i++)
                    {
                        UpdateLocal(materials[i]);
                    }
                }
                public void UpdateLocal(Material materia)
                {
                    UpdateLocalProperty(materia, Property.WidthStandard, widthCutoutStandard);
                    UpdateLocalProperty(materia, Property.WidthGeometric, widthCutoutGeometric);
                    UpdateLocalProperty(materia, Property.Shape, shape);

                    UpdateLocalProperty(materia, Property.Color, color);
                    UpdateLocalProperty(materia, Property.ColorTransparency, colorTransparency);
                    UpdateLocalProperty(materia, Property.ColorIntensity, colorIntensity);                   
                }
                public void UpdateGlobal(AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID globalControlID)
                {
                    UpdateGlobalProperty(globalControlID, Property.WidthStandard, widthCutoutStandard);
                    UpdateGlobalProperty(globalControlID, Property.WidthGeometric, widthCutoutGeometric);
                    UpdateGlobalProperty(globalControlID, Property.Shape, shape);

                    UpdateGlobalProperty(globalControlID, Property.Color, color);
                    UpdateGlobalProperty(globalControlID, Property.ColorTransparency, colorTransparency);
                    UpdateGlobalProperty(globalControlID, Property.ColorIntensity, colorIntensity);                   
                }

                static public void UpdateLocalProperty(Material material, Property property, object value)
                {
                    if (material == null)
                        return;

                    System.Type type = value == null ? null : value.GetType();


                    switch (property)
                    {
                        case Property.WidthStandard:
                            if (type == typeof(float))
                                material.SetFloat(ids[0].widthStandard, (float)value);
                            break;
                        case Property.WidthGeometric:
                            if (type == typeof(float))
                                material.SetFloat(ids[0].widthGeometric, (float)value);
                            break;
                        case Property.Shape:
                            if (type == typeof(Shape))
                                material.SetInt(ids[0].shape, (int)value);
                            break;
                        case Property.Color:
                            if (type == typeof(Color))
                                material.SetColor(ids[0].color, QualitySettings.activeColorSpace == ColorSpace.Linear ? ((Color)value).linear : (Color)value);
                            break;
                        case Property.ColorTransparency:
                            if (type == typeof(float))
                                material.SetFloat(ids[0].colorTransparency, (float)value);
                            break;
                        case Property.ColorIntensity:
                            if (type == typeof(float))
                            {
                                float v = (float)value;
                                v = v < 0 ? 0 : v;
                                float exp = Mathf.Exp(v) - 1;
                                exp = exp < 0 ? 0 : exp;

                                material.SetVector(ids[0].colorIntensity, new Vector2(v, exp));
                            }
                            break;
                        
                        default:
                            break;
                    }
                }
                static public void UpdateGlobalProperty(AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID globalControlID, Property property, object value)
                {
                    if (globalControlID == AdvancedDissolveKeywords.GlobalControlID.None)
                        return;


                    System.Type type = value == null ? null : value.GetType();


                    switch (property)
                    {
                        case Property.WidthStandard:
                            if (type == typeof(float))
                                Shader.SetGlobalFloat(ids[(int)globalControlID].widthStandard, (float)value);
                            break;
                        case Property.WidthGeometric:
                            if (type == typeof(float))
                                Shader.SetGlobalFloat(ids[(int)globalControlID].widthGeometric, (float)value);
                            break;
                        case Property.Shape:
                            if (type == typeof(Shape))
                                Shader.SetGlobalInt(ids[(int)globalControlID].shape, (int)value);
                            break;
                        case Property.Color:
                            if (type == typeof(Color))
                                Shader.SetGlobalColor(ids[(int)globalControlID].color, QualitySettings.activeColorSpace == ColorSpace.Linear ? ((Color)value).linear : (Color)value);
                            break;
                        case Property.ColorTransparency:
                            if (type == typeof(float))
                                Shader.SetGlobalFloat(ids[(int)globalControlID].colorTransparency, (float)value);
                            break;
                        case Property.ColorIntensity:
                            if (type == typeof(float))
                            {
                                float v = (float)value;
                                v = v < 0 ? 0 : v;
                                float exp = Mathf.Exp(v) - 1;
                                exp = exp < 0 ? 0 : exp;


                                Shader.SetGlobalVector(ids[(int)globalControlID].colorIntensity, new Vector2(v, exp));
                            }
                            break;                      

                        default:
                            break;
                    }
                }
            }

            [System.Serializable]
            public class AdditionalColor
            {
                class IDs
                {
                    public int color;
                    public int colorTransparency;
                    public int colorIntensity;

                    public int clipInterpolation;

                    public int map;
                    public int mapTiling;
                    public int mapOffset;
                    public int mapScroll;
                    public int mapReverse;
                    public int mapMipMap;
                    public int phaseOffset;
                    public int alphaOffset;     


                    public IDs(int ID)
                    {
                        string id = ID == 0 ? string.Empty : ("_ID" + ID);

                        color = Shader.PropertyToID("_AdvancedDissolveEdgeAdditionalColor" + id);
                        colorTransparency = Shader.PropertyToID("_AdvancedDissolveEdgeAdditionalColorTransparency" + id);
                        colorIntensity = Shader.PropertyToID("_AdvancedDissolveEdgeAdditionalColorIntensity" + id);

                        clipInterpolation = Shader.PropertyToID("_AdvancedDissolveEdgeAdditionalColorClipInterpolation" + id);

                        map = Shader.PropertyToID("_AdvancedDissolveEdgeAdditionalColorMap" + id);
                        mapTiling = Shader.PropertyToID("_AdvancedDissolveEdgeAdditionalColorMapTiling" + id);
                        mapOffset = Shader.PropertyToID("_AdvancedDissolveEdgeAdditionalColorMapOffset" + id);
                        mapScroll = Shader.PropertyToID("_AdvancedDissolveEdgeAdditionalColorMapScroll" + id);
                        mapReverse = Shader.PropertyToID("_AdvancedDissolveEdgeAdditionalColorMapReverse" + id);
                        phaseOffset = Shader.PropertyToID("_AdvancedDissolveEdgeAdditionalColorPhaseOffset" + id);
                        alphaOffset = Shader.PropertyToID("_AdvancedDissolveEdgeAdditionalColorAlphaOffset" + id);
                        mapMipMap = Shader.PropertyToID("_AdvancedDissolveEdgeAdditionalColorMapMipmap" + id);                        
                    }
                }

                static IDs[] ids = new IDs[] { new IDs(0), new IDs(1), new IDs(2), new IDs(3), new IDs(4) };


                public enum Property
                {                    
                    Map, MapTiling, MapOffset, MapScroll, MapReverse, MapMipmap, PhaseOffset, AlphaOffset,
                    Color, ColorTransparency, ColorIntensity,
                    ClipInterpolation,                    
                }
            
                public Texture2D map;
                public Vector2 mapTiling = Vector2.one;
                public Vector2 mapOffset = Vector2.zero;
                public Vector2 mapScroll = Vector2.zero;
                public bool mapReverse = false;
                [Range(0, 10)] public int mapMipmap;
                public float phaseOffset;
                [Range(-1, 1)] public float alphaOffset;
                

                [Space(10)]
                [ColorUsage(false)] public Color color = Color.red;
                [Range(0, 1)] public float colorTransparency = 1;
                public float colorIntensity;

                [Space(10)]
                public bool clipInterpolation = false;
               

                public void UpdateLocal(List<Material> materials)
                {
                    if (materials == null)
                        return;

                    for (int i = 0; i < materials.Count; i++)
                    {
                        UpdateLocal(materials[i]);
                    }
                }
                public void UpdateLocal(Material materia)
                {                    
                    UpdateLocalProperty(materia, Property.Map, map);
                    UpdateLocalProperty(materia, Property.MapTiling, mapTiling);
                    UpdateLocalProperty(materia, Property.MapOffset, mapOffset);
                    UpdateLocalProperty(materia, Property.MapScroll, mapScroll);
                    UpdateLocalProperty(materia, Property.MapReverse, mapReverse);
                    UpdateLocalProperty(materia, Property.PhaseOffset, phaseOffset);
                    UpdateLocalProperty(materia, Property.AlphaOffset, alphaOffset);
                    UpdateLocalProperty(materia, Property.MapMipmap, mapMipmap);

                    UpdateLocalProperty(materia, Property.Color, color);
                    UpdateLocalProperty(materia, Property.ColorTransparency, colorTransparency);
                    UpdateLocalProperty(materia, Property.ColorIntensity, colorIntensity);

                    UpdateLocalProperty(materia, Property.ClipInterpolation, clipInterpolation);                    
                }
                public void UpdateGlobal(AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID globalControlID)
                {         
                    UpdateGlobalProperty(globalControlID, Property.Map, map);
                    UpdateGlobalProperty(globalControlID, Property.MapTiling, mapTiling);
                    UpdateGlobalProperty(globalControlID, Property.MapOffset, mapOffset);
                    UpdateGlobalProperty(globalControlID, Property.MapScroll, mapScroll);
                    UpdateGlobalProperty(globalControlID, Property.MapReverse, mapReverse);
                    UpdateGlobalProperty(globalControlID, Property.PhaseOffset, phaseOffset);
                    UpdateGlobalProperty(globalControlID, Property.AlphaOffset, alphaOffset);
                    UpdateGlobalProperty(globalControlID, Property.MapMipmap, mapMipmap);

                    UpdateGlobalProperty(globalControlID, Property.Color, color);
                    UpdateGlobalProperty(globalControlID, Property.ColorTransparency, colorTransparency);
                    UpdateGlobalProperty(globalControlID, Property.ColorIntensity, colorIntensity);

                    UpdateGlobalProperty(globalControlID, Property.ClipInterpolation, clipInterpolation);                    
                }

                static public void UpdateLocalProperty(Material material, Property property, object value)
                {
                    if (material == null)
                        return;

                    if (value == null && !(property == Property.Map))
                        return;

                    System.Type type = value == null ? null : value.GetType();


                    switch (property)
                    {                       
                        case Property.Map:
                            if (value == null)
                                material.SetTexture(ids[0].map, null);
                            else if (type == typeof(Texture2D))
                                material.SetTexture(ids[0].map, (Texture2D)value);
                            else if (type == typeof(Texture))
                                material.SetTexture(ids[0].map, (Texture)value);
                            break;
                        case Property.MapTiling:
                            if (type == typeof(Vector2))
                                material.SetVector(ids[0].mapTiling, (Vector2)value);
                            else if (type == typeof(float))
                                material.SetVector(ids[0].mapTiling, Vector4.one * (float)value);
                            break;
                        case Property.MapOffset:
                            if (type == typeof(Vector2))
                                material.SetVector(ids[0].mapOffset, (Vector2)value);
                            else if(type == typeof(float))
                                material.SetVector(ids[0].mapOffset, Vector4.one * (float)value);
                            break;
                        case Property.MapScroll:
                            if (type == typeof(Vector2))
                                material.SetVector(ids[0].mapScroll, (Vector2)value);
                            else if (type == typeof(float))
                                material.SetVector(ids[0].mapScroll, Vector4.one * (float)value);
                            break;
                        case Property.MapReverse:
                            if (type == typeof(bool))
                                material.SetInt(ids[0].mapReverse, (bool)value ? 1 : 0);
                            break;
                        case Property.MapMipmap:
                            if (type == typeof(float))
                                material.SetFloat(ids[0].mapMipMap, (float)value);
                            break;
                        case Property.PhaseOffset:
                            if (type == typeof(float))
                                material.SetFloat(ids[0].phaseOffset, (float)value);
                            break;
                        case Property.AlphaOffset:
                            if (type == typeof(float))
                                material.SetFloat(ids[0].alphaOffset, (float)value);
                            break;                        
                        case Property.Color:
                            if (type == typeof(Color))
                                material.SetColor(ids[0].color, QualitySettings.activeColorSpace == ColorSpace.Linear ? ((Color)value).linear : (Color)value);
                            break;
                        case Property.ColorTransparency:
                            if (type == typeof(float))
                                material.SetFloat(ids[0].colorTransparency, (float)value);
                            break;
                        case Property.ColorIntensity:
                            if (type == typeof(float))
                            {
                                float v = (float)value;
                                v = v < 0 ? 0 : v;
                                float exp = Mathf.Exp(v) - 1;
                                exp = exp < 0 ? 0 : exp;

                                material.SetVector(ids[0].colorIntensity, new Vector2(v, exp));
                            }
                            break;
                        case Property.ClipInterpolation:
                            if (type == typeof(bool))
                                material.SetInt(ids[0].clipInterpolation, (bool)value ? 1 : 0);
                            break;                       

                        default:
                            break;
                    }
                }
                static public void UpdateGlobalProperty(AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID globalControlID, Property property, object value)
                {
                    if (globalControlID == AdvancedDissolveKeywords.GlobalControlID.None)
                        return;

                    if (value == null && !(property == Property.Map))
                        return;

                    System.Type type = value == null ? null : value.GetType();


                    switch (property)
                    {                       
                        case Property.Map:
                            if (value == null)
                                Shader.SetGlobalTexture(ids[(int)globalControlID].map, null);
                            else if (type == typeof(Texture2D))
                                Shader.SetGlobalTexture(ids[(int)globalControlID].map, (Texture2D)value);
                            else if (type == typeof(Texture))
                                Shader.SetGlobalTexture(ids[(int)globalControlID].map, (Texture)value);
                            break;
                        case Property.MapTiling:
                            if (type == typeof(Vector2))
                                Shader.SetGlobalVector(ids[(int)globalControlID].mapTiling, (Vector2)value);
                            else if (type == typeof(float))
                                Shader.SetGlobalVector(ids[(int)globalControlID].mapTiling, Vector4.one * (float)value);
                            break;
                        case Property.MapOffset:
                            if (type == typeof(Vector2))
                                Shader.SetGlobalVector(ids[(int)globalControlID].mapOffset, (Vector2)value);
                            else if (type == typeof(float))
                                Shader.SetGlobalVector(ids[(int)globalControlID].mapOffset, Vector4.one * (float)value);
                            break;
                        case Property.MapScroll:
                            if (type == typeof(Vector2))
                                Shader.SetGlobalVector(ids[(int)globalControlID].mapScroll, (Vector2)value);
                            else if (type == typeof(float))
                                Shader.SetGlobalVector(ids[(int)globalControlID].mapScroll, Vector4.one * (float)value);
                            break;
                        case Property.MapReverse:
                            if (type == typeof(bool))
                                Shader.SetGlobalInt(ids[(int)globalControlID].mapReverse, (bool)value ? 1 : 0);
                            break;
                        case Property.PhaseOffset:
                            if (type == typeof(float))
                                Shader.SetGlobalFloat(ids[(int)globalControlID].phaseOffset, (float)value);
                            break;
                        case Property.AlphaOffset:
                            if (type == typeof(float))
                                Shader.SetGlobalFloat(ids[(int)globalControlID].alphaOffset, (float)value);
                            break;
                        case Property.MapMipmap:
                            if (type == typeof(float))
                                Shader.SetGlobalFloat(ids[(int)globalControlID].mapMipMap, (float)value);
                            break;
                        case Property.Color:
                            if (type == typeof(Color))
                                Shader.SetGlobalColor(ids[(int)globalControlID].color, QualitySettings.activeColorSpace == ColorSpace.Linear ? ((Color)value).linear : (Color)value);
                            break;
                        case Property.ColorTransparency:
                            if (type == typeof(float))
                                Shader.SetGlobalFloat(ids[(int)globalControlID].colorTransparency, (float)value);
                            break;
                        case Property.ColorIntensity:
                            if (type == typeof(float))
                            {
                                float v = (float)value;
                                v = v < 0 ? 0 : v;
                                float exp = Mathf.Exp(v) - 1;
                                exp = exp < 0 ? 0 : exp;

                                Shader.SetGlobalVector(ids[(int)globalControlID].colorIntensity, new Vector2(v, exp));
                            }
                            break;
                        case Property.ClipInterpolation:
                            if (type == typeof(bool))
                                Shader.SetGlobalInt(ids[(int)globalControlID].clipInterpolation, (bool)value ? 1 : 0);
                            break;
                       
                        default:
                            break;
                    }
                }
            }

            [System.Serializable]
            public class UVDistortion
            {
                class IDs
                {
                    public int map;
                    public int mapTiling;
                    public int mapOffset;
                    public int mapScroll;
                    public int strength;

                    public IDs(int ID)
                    {
                        string id = ID == 0 ? string.Empty : ("_ID" + ID);

                        map = Shader.PropertyToID("_AdvancedDissolveEdgeUVDistortionMap" + id);
                        mapTiling = Shader.PropertyToID("_AdvancedDissolveEdgeUVDistortionMapTiling" + id);
                        mapOffset = Shader.PropertyToID("_AdvancedDissolveEdgeUVDistortionMapOffset" + id);
                        mapScroll = Shader.PropertyToID("_AdvancedDissolveEdgeUVDistortionMapScroll" + id);
                        strength = Shader.PropertyToID("_AdvancedDissolveEdgeUVDistortionStrength" + id);
                    }
                }

                static IDs[] ids = new IDs[] { new IDs(0), new IDs(1), new IDs(2), new IDs(3), new IDs(4) };


                public enum Property
                {
                    Map, MapTiling, MapOffset, MapScroll, Strength,
                }

                public Texture2D map;
                public Vector2 mapTiling = Vector2.one;
                public Vector2 mapOffset = Vector2.zero;
                public Vector2 mapScroll = Vector2.zero;
                public float strength;


                public void UpdateLocal(List<Material> materials)
                {
                    if (materials == null)
                        return;

                    for (int i = 0; i < materials.Count; i++)
                    {
                        UpdateLocal(materials[i]);
                    }
                }
                public void UpdateLocal(Material materia)
                {
                    UpdateLocalProperty(materia, Property.Map, map);
                    UpdateLocalProperty(materia, Property.MapTiling, mapTiling);
                    UpdateLocalProperty(materia, Property.MapOffset, mapOffset);
                    UpdateLocalProperty(materia, Property.MapScroll, mapScroll);
                    UpdateLocalProperty(materia, Property.Strength, strength);
                }
                public void UpdateGlobal(AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID globalControlID)
                {
                    UpdateGlobalProperty(globalControlID, Property.Map, map);
                    UpdateGlobalProperty(globalControlID, Property.MapTiling, mapTiling);
                    UpdateGlobalProperty(globalControlID, Property.MapOffset, mapOffset);
                    UpdateGlobalProperty(globalControlID, Property.MapScroll, mapScroll);
                    UpdateGlobalProperty(globalControlID, Property.Strength, strength);
                }

                static public void UpdateLocalProperty(Material material, Property property, object value)
                {
                    if (material == null)
                        return;

                    if (value == null && !(property == Property.Map))
                        return;

                    System.Type type = value == null ? null : value.GetType();


                    switch (property)
                    {
                        case Property.Map:
                            if (value == null)
                                material.SetTexture(ids[0].map, null);
                            else if (type == typeof(Texture2D))
                                material.SetTexture(ids[0].map, (Texture2D)value);
                            else if (type == typeof(Texture))
                                material.SetTexture(ids[0].map, (Texture)value);
                            break;
                        case Property.MapTiling:
                            if (type == typeof(Vector2))
                                material.SetVector(ids[0].mapTiling, (Vector2)value);
                            break;
                        case Property.MapOffset:
                            if (type == typeof(Vector2))
                                material.SetVector(ids[0].mapOffset, (Vector2)value);
                            break;
                        case Property.MapScroll:
                            if (type == typeof(Vector2))
                                material.SetVector(ids[0].mapScroll, (Vector2)value);
                            break;
                        case Property.Strength:
                            if (type == typeof(float))
                                material.SetFloat(ids[0].strength, (float)value);
                            break;

                        default:
                            break;
                    }
                }
                static public void UpdateGlobalProperty(AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID globalControlID, Property property, object value)
                {
                    if (globalControlID == AdvancedDissolveKeywords.GlobalControlID.None)
                        return;

                    if (value == null && !(property == Property.Map))
                        return;

                    System.Type type = value == null ? null : value.GetType();


                    switch (property)
                    {
                        case Property.Map:
                            if (value == null)
                                Shader.SetGlobalTexture(ids[(int)globalControlID].map, null);
                            else if (type == typeof(Texture2D))
                                Shader.SetGlobalTexture(ids[(int)globalControlID].map, (Texture2D)value);
                            else if (type == typeof(Texture))
                                Shader.SetGlobalTexture(ids[(int)globalControlID].map, (Texture)value);
                            break;
                        case Property.MapTiling:
                            if (type == typeof(Vector2))
                                Shader.SetGlobalVector(ids[(int)globalControlID].mapTiling, (Vector2)value);
                            break;
                        case Property.MapOffset:
                            if (type == typeof(Vector2))
                                Shader.SetGlobalVector(ids[(int)globalControlID].mapOffset, (Vector2)value);
                            break;
                        case Property.MapScroll:
                            if (type == typeof(Vector2))
                                Shader.SetGlobalVector(ids[(int)globalControlID].mapScroll, (Vector2)value);
                            break;
                        case Property.Strength:
                            if (type == typeof(float))
                                Shader.SetGlobalFloat(ids[(int)globalControlID].strength, (float)value);
                            break;
                        default:
                            break;
                    }
                }
            }

            [System.Serializable]
            public class GlobalIllumination
            {
                class IDs
                {
                    public int metaPassMultiplier;

                    public IDs(int ID)
                    {
                        string id = ID == 0 ? string.Empty : ("_ID" + ID);

                        metaPassMultiplier = Shader.PropertyToID("_AdvancedDissolveEdgeGIMetaPassMultiplier" + ID);
                    }

                }

                static IDs[] ids = new IDs[] { new IDs(0), new IDs(1), new IDs(2), new IDs(3), new IDs(4) };


                public float metaPassMultiplier;


                public void UpdateLocal(List<Material> materials)
                {
                    if (materials == null)
                        return;

                    for (int i = 0; i < materials.Count; i++)
                    {
                        UpdateLocal(materials[i]);
                    }
                }
                public void UpdateLocal(Material materia)
                {
                    UpdateLocalProperty(materia, metaPassMultiplier);
                }
                public void UpdateGlobal(AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID globalControlID)
                {
                    UpdateGlobalProperty(globalControlID,  metaPassMultiplier);
                }


                static public void UpdateLocalProperty(Material material, float value)
                {
                    if (material == null)
                        return;

                    material.SetFloat(ids[0].metaPassMultiplier, value);

                }
                static public void UpdateGlobalProperty(AdvancedDissolve.AdvancedDissolveKeywords.GlobalControlID globalControlID, float value)
                {
                    if (globalControlID == AdvancedDissolveKeywords.GlobalControlID.None)
                        return;

                    Shader.SetGlobalFloat(ids[(int)globalControlID].metaPassMultiplier, value);
                }
            }
        }
    }
}
