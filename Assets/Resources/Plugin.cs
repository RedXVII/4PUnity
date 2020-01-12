//Version 3.0
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Animate
{
    public class LayerStateObject
    {
        public GameObject gobj;
        public Animation anim;
        public string LayerStateName;
        public string Clipped_by;
        public string Layer_type;
        public string LT;
        public string Layer_typeClipper;
        public string SpriteType;
        public float zDepth;
        public int backLayerSortingOrder;
        public int frontLayerSortingOrder;
        public int SymbolDuration;
        public LayerStateObject ParentClipper;
        public LayerStateObject ParentLayer;
        public LayerStateObject parent;
        public LayerStateObject ParentSymbol;
        public List<string> children = new List<string>();
        public Dictionary<int, string> spriteMask = new Dictionary<int, string>();
        public Dictionary<int, string> spriteName = new Dictionary<int, string>();
        public Dictionary<int, decomposedMat> arr = new Dictionary<int, decomposedMat>();
        public Dictionary<int, Matrix3> Matarr = new Dictionary<int, Matrix3>();
        public Dictionary<int,int> sortingorder = new Dictionary<int, int>();
        public Dictionary<int, ColorEffect> color = new Dictionary<int, ColorEffect>();
        public Dictionary<int, Colour> colorraw = new Dictionary<int, Colour>();
        public Dictionary<int, bool> SpriteEnabled = new Dictionary<int, bool>();
    }

    [System.Serializable]
    public class ParsedSpriteMap
    {
        public Atlas ATLAS;
        public meta meta;
    }
    [System.Serializable]
    public class Atlas
    {
        public Sprite1[] SPRITES;
    }
    [System.Serializable]
    public class Sprite1
    {
        public SubSprite SPRITE;
    }
    [System.Serializable]
    public class SubSprite
    {
        public string name;
        public int x;
        public int y;
        public int w;
        public int h;
        public bool rotated;
    }
    public class SpriteObject
    {
        public Sprite sp;
        public string name;
        public bool rotated; 
    }
    [System.Serializable]
    public class ParsedAnimation
    {
        public AnimationFile ANIMATION;
        public SymbolDict SYMBOL_DICTIONARY;
        public meta metadata;
        public AnimationFile AN;
        public SymbolDict SD;
        public meta MD;
    }
    [System.Serializable]
    public class AnimationFile
    {
        public string name;
        public StageProperties StageInstance;
        public string SYMBOL_name;
        public TimeLine TIMELINE;
        public string N;
        public StageProperties STI;
        public string SN;
        public TimeLine TL;
    }

    [System.Serializable]
    public class StageProperties
    {
        public InstanceProperties SYMBOL_Instance;
        public InstanceProperties SI;
    }

    [System.Serializable]
    public class InstanceProperties
    {
        public string SYMBOL_name;
        public string Instance_Name;
        public AtlasSprite bitmap;
        public string symbolType;
        public int firstFrame;
        public string loop;
        public Vector2 transformationPoint;
        public Matrix3 Matrix3D;
        public string SN;
        public  string IN;
        public AtlasSprite BM;
        public string ST;
        public int FF;
        public string LP;
        public Vector2 TRP;
        public Matrix3 M3D;
        public decomposedMat DecomposedMatrix;
        public Colour color;
    }
    [System.Serializable]
    public class Matrix3
    {
        public float m00;
        public float m01;
        public float m02;
        public float m03;
        public float m10;
        public float m11;
        public float m12;
        public float m13;
        public float m20;
        public float m21;
        public float m22;
        public float m23;
        public float m30;
        public float m31;
        public float m32;
        public float m33;
    }
    [System.Serializable]
    public class SymbolDict
    {
        public Symbol[] Symbols;
        public Symbol[] S;
    }
    [System.Serializable]
    public class meta
    {
        public float framerate;
        public float FRT;
        public Size size;
    }
    [System.Serializable]
    public class TimeLine
    {
        public Layer[] LAYERS;
        public Layer[] L;
        public int TimelineDuration;
        public Matrix3 M3D;
        public decomposedMat DecomposedMatrix;
    }
    [System.Serializable]
    public class Layer
    {
        public string Layer_name;
        public string Layer_type;
        public string LN;
        public string LT;
        public string Clipped_by;
        public Frame[] Frames;
        public Frame[] FR;
        public int LayerDuration;
    }
    [System.Serializable]
    public class Frame
    {
        public string name;
        public int index;
        public int duration;
        public string N;
        public int I;
        public int DU;
        public float zDepth;
        public Element[] elements;
        public Element[] E;
    }

    [System.Serializable]
    public class Element
    {
        public InstanceProperties SYMBOL_Instance;
        public AtlasSprite ATLAS_SPRITE_instance;
        public InstanceProperties SI;
        public AtlasSprite ASI;
    }

    [System.Serializable]
    public class AtlasSprite
    {
        public string name;
        public Vector3 Position;
        public string N;
        public Vector3 POS;
    }

    [System.Serializable]
    public class Symbol
    {
        public string SYMBOL_name;
        public TimeLine TIMELINE;
        public string SN;
        public TimeLine TL;
    }

    [System.Serializable]
    public class decomposedMat
    {
        public Vector3 Position;
        public Vector3 Rotation;
        public Vector3 Scaling;
    }

    [System.Serializable]
    public class Size
    {
        public float w;
        public float h;
    }

    [System.Serializable]
    public class Colour
    {
        public string mode;
        public string tintColor;
        public float tintMultiplier;
        public float RedMultiplier;
        public float greenMultiplier;
        public float blueMultiplier;
        public float alphaMultiplier;
        public int redOffset;
        public int greenOffset;
        public int blueOffset;
        public int AlphaOffset;
        public float brightness;
        public string M;
        public string TC;
        public float TM;
        public float RM;
        public float GM;
        public float BM;
        public float AM;
        public int RO;
        public int GO;
        public int BO;
        public int AO;
        public float BRT;
     }
    public class ColorEffect
    {
        public Color colorMultiplier;
        public Color colorOffset;
    }
    public class MainAnimation
    {
        public Sprite[] sprites;
        public float framerate;
        public TimeLine MainTimeline = new TimeLine();
        Dictionary<string, SpriteObject> SpriteDict = new Dictionary<string, SpriteObject>();
        Dictionary<string, TimeLine> SymDict = new Dictionary<string, TimeLine>();
        Dictionary<string, int> LayerState = new Dictionary<string, int>();
        Dictionary<string, int> FrameLevelDict = new Dictionary<string, int>();
        public Dictionary<string, LayerStateObject> LayerStateObjDICT = new Dictionary<string, LayerStateObject>();
        public LayerStateObject mainGameObject = new LayerStateObject();
        Matrix3 ScaleMatrix = new Matrix3();
        decomposedMat ScaleDemat = new decomposedMat();
        int updateCounter = 0;
        int startCounter = 0;
        int endCounter = 0;
        bool JsonOptimize = false;
        public void Init()
        {
            SpriteSlicerAll();
            AnimationParser();
            AnimationPreprocessor();
            Time.fixedDeltaTime = 1f;
            Time.timeScale = framerate;
        }
        public int SpriteCount(DirectoryInfo d)
        {
            int i = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                if (fi.Extension.Contains("png") && fi.Name.Contains("spritemap"))
                    i++;
            }
            return i;
        }
        public void SpriteSlicerAll()
        {
            DirectoryInfo root = new DirectoryInfo("Assets/Resources/");
            for (int index = 1; index <= SpriteCount(root); index++)
            {
                SpriteSlice(index);
            }
        }
        public void SpriteSlice(int index)
        {
            ParsedSpriteMap spritemapjson;
            using (StreamReader r = new StreamReader(@"Assets/Resources/spritemap"+index+".json"))
            {
                string json = r.ReadToEnd();
                spritemapjson = JsonUtility.FromJson<ParsedSpriteMap>(json);
            }
            sprites = Resources.LoadAll<Sprite>("spritemap"+index);

            foreach (var sp in sprites)
            {
                SpriteObject sobj = new SpriteObject();
                sobj.name = sp.name;
                sobj.sp = sp;
                sobj.rotated = false;
                SpriteDict.Add(sobj.name, sobj);
            }
            foreach (var sprites in spritemapjson.ATLAS.SPRITES)
            {
                if (sprites.SPRITE.rotated)
                {
                    SpriteObject sobj = SpriteDict[sprites.SPRITE.name];
                    sobj.rotated = true;
                    SpriteDict[sobj.name] = sobj;
                }
            }
        }
        public void AnimationParser()
        {
            using (StreamReader r = new StreamReader(@"Assets/Resources/Animation.json"))
            {
                string json = r.ReadToEnd();
                ParsedAnimation animjson = JsonUtility.FromJson<ParsedAnimation>(json);
                if (animjson.ANIMATION.TIMELINE != null)
                    JsonOptimize = false;
                else
                    JsonOptimize = true;
                if (JsonOptimize)
                {
                    framerate = animjson.MD.FRT;
                    mainGameObject.LayerStateName = animjson.AN.SN;
                    if (animjson.AN.STI.SI != null)
                        mainGameObject.arr[0] = ComputeDecomposedMat(animjson.AN.STI.SI.M3D);
                    MainTimeline = animjson.AN.TL;
                    if (animjson.AN.TL.M3D != null)
                    {
                        ScaleMatrix = animjson.AN.TL.M3D;
                        ScaleDemat = ComputeDecomposedMat(ScaleMatrix);
                    }
                    foreach (var layer in MainTimeline.L)
                    {
                        foreach (var frame in layer.FR)
                        {
                            if (layer.LayerDuration < frame.I + frame.DU)
                                layer.LayerDuration = frame.I + frame.DU;

                        }
                        if (MainTimeline.TimelineDuration < layer.LayerDuration)
                            MainTimeline.TimelineDuration = layer.LayerDuration;
                    }
                    if (animjson.SD.S != null)
                    {
                        foreach (var Symbols in animjson.SD.S)
                        {
                            SymDict[Symbols.SN] = Symbols.TL;
                            foreach (var layer in Symbols.TL.L)
                            {
                                foreach (var frame in layer.FR)
                                {
                                    if (layer.LayerDuration < frame.I + frame.DU)
                                        layer.LayerDuration = frame.I + frame.DU;
                                }
                                if (Symbols.TL.TimelineDuration < layer.LayerDuration)
                                    Symbols.TL.TimelineDuration = layer.LayerDuration;
                            }
                        }
                    }
                }
                else
                {
                    framerate = animjson.metadata.framerate;
                    mainGameObject.LayerStateName = animjson.ANIMATION.SYMBOL_name;
                    if (animjson.ANIMATION.StageInstance.SYMBOL_Instance != null)
                        mainGameObject.arr[0] = animjson.ANIMATION.StageInstance.SYMBOL_Instance.DecomposedMatrix;
                    MainTimeline = animjson.ANIMATION.TIMELINE;
                    if (animjson.ANIMATION.TIMELINE.DecomposedMatrix != null)
                    {
                        ScaleDemat = animjson.ANIMATION.TIMELINE.DecomposedMatrix;
                    }
                    foreach (var layer in MainTimeline.LAYERS)
                    {
                        foreach (var frame in layer.Frames)
                        {
                            if (layer.LayerDuration < frame.index + frame.duration)
                                layer.LayerDuration = frame.index + frame.duration;

                        }
                        if (MainTimeline.TimelineDuration < layer.LayerDuration)
                            MainTimeline.TimelineDuration = layer.LayerDuration;
                    }
                    if (animjson.SYMBOL_DICTIONARY.Symbols != null)
                    {
                        foreach (var Symbols in animjson.SYMBOL_DICTIONARY.Symbols)
                        {
                            SymDict[Symbols.SYMBOL_name] = Symbols.TIMELINE;
                            foreach (var layer in Symbols.TIMELINE.LAYERS)
                            {
                                foreach (var frame in layer.Frames)
                                {
                                    if (layer.LayerDuration < frame.index + frame.duration)
                                        layer.LayerDuration = frame.index + frame.duration;
                                }
                                if (Symbols.TIMELINE.TimelineDuration < layer.LayerDuration)
                                    Symbols.TIMELINE.TimelineDuration = layer.LayerDuration;
                            }
                        }
                    }
                }
            }
        }
        public void AnimationPreprocessor()
        {
            int currentFrameIndex = 0;
            mainGameObject.gobj = new GameObject();
            mainGameObject.gobj.name = mainGameObject.LayerStateName;
            mainGameObject.parent = null;
            mainGameObject.sortingorder[currentFrameIndex] = 0;
            int Layercounter = 1;
            if(JsonOptimize)
            {
                foreach (var layer in MainTimeline.L)
                {
                    Layercounter = ProcessMainLayer(layer, currentFrameIndex, mainGameObject, Layercounter, mainGameObject);
                    Layercounter++;
                }
            }
            else
            {
                foreach (var layer in MainTimeline.LAYERS)
                {
                    Layercounter = ProcessMainLayer(layer, currentFrameIndex, mainGameObject, Layercounter, mainGameObject);
                    Layercounter++;
                }
            }
            
            LayerStateObjDICT[mainGameObject.LayerStateName] = mainGameObject;
        }
        public int ProcessMainLayer(Layer layer, int currentFrameIndex, LayerStateObject ParentLSO, int Layercounter, LayerStateObject ParentSymbol)
        {
            LayerStateObject LSO = LayerInitialize(ParentLSO, layer, ParentLSO.LayerStateName,Layercounter,ParentSymbol);
            LSO.sortingorder[currentFrameIndex] = ParentLSO.sortingorder[currentFrameIndex] + Layercounter;
            LSO.frontLayerSortingOrder = -Layercounter;
            if(JsonOptimize)
            {
                foreach (var frame in layer.FR)
                {
                    Layercounter = ProcessMainFrame(frame, currentFrameIndex, LSO.LayerStateName, LSO, Layercounter);
                    currentFrameIndex += frame.DU;
                }
            }
            else
            {
                foreach (var frame in layer.Frames)
                {
                    Layercounter = ProcessMainFrame(frame, currentFrameIndex, LSO.LayerStateName, LSO, Layercounter);
                    currentFrameIndex += frame.duration;
                }
            }
            if (layer.Clipped_by != null)
            {
                LSO.ParentClipper.backLayerSortingOrder = -Layercounter;
            }
            LayerStateObjDICT[LSO.LayerStateName] = LSO;
            return Layercounter;
        }
        public int ProcessMainFrame(Frame frame, int currentFrameIndex, string uniqueID, LayerStateObject ParentLSO, int Layercounter)
        {
            int counter = 1;
            List<string> tempFrame = new List<string>();
            if(JsonOptimize)
            {
                if (frame.N != null)
                    FrameLevelDict[frame.N] = currentFrameIndex;
                int Elementcounter = Layercounter;
                for (int i = frame.E.Length - 1; i >= 0; i--)
                {
                    var ele = frame.E[i];
                    if (ele.ASI.N != null)
                    {
                        AtlasSpriteInitialize(ele.ASI, frame.zDepth, currentFrameIndex, frame.DU, uniqueID, ParentLSO, Elementcounter, ele, "atlas");
                    }
                    else if (ele.SI.SN != null)
                    {
                        string ID = uniqueID + "_" + ele.SI.SN + ele.SI.IN + counter;
                        while (tempFrame.Contains(ID))
                        {
                            counter++;
                            ID = uniqueID + "_" + ele.SI.SN + ele.SI.IN + counter;
                        }
                        tempFrame.Add(ID);
                        Elementcounter = ProcessSymbolInstance(ele, frame.zDepth, currentFrameIndex, frame.DU, ID, ParentLSO, Elementcounter);
                        if (!ParentLSO.children.Contains(ID))
                            ParentLSO.children.Add(ID);
                    }
                    Elementcounter++;
                }
                Layercounter += frame.E.Length;
            }
            else
            {
                if (frame.name != null)
                    FrameLevelDict[frame.name] = currentFrameIndex;
                int Elementcounter = Layercounter;
                for (int i = frame.elements.Length - 1; i >= 0; i--)
                {
                    var ele = frame.elements[i];
                    if (ele.ATLAS_SPRITE_instance.name != null)
                    {
                        AtlasSpriteInitialize(ele.ATLAS_SPRITE_instance, frame.zDepth, currentFrameIndex, frame.duration, uniqueID, ParentLSO, Elementcounter, ele, "atlas");
                    }
                    else if (ele.SYMBOL_Instance.SYMBOL_name != null)
                    {
                        string ID = uniqueID + "_" + ele.SYMBOL_Instance.SYMBOL_name + ele.SYMBOL_Instance.Instance_Name + counter;
                        while (tempFrame.Contains(ID))
                        {
                            counter++;
                            ID = uniqueID + "_" + ele.SYMBOL_Instance.SYMBOL_name + ele.SYMBOL_Instance.Instance_Name + counter;
                        }
                        tempFrame.Add(ID);
                        Elementcounter = ProcessSymbolInstance(ele, frame.zDepth, currentFrameIndex, frame.duration, ID, ParentLSO, Elementcounter);
                        if (!ParentLSO.children.Contains(ID))
                            ParentLSO.children.Add(ID);
                    }
                    Elementcounter++;
                }
                Layercounter += frame.elements.Length;
            }
            return Layercounter;
        }
        public LayerStateObject LayerInitialize(LayerStateObject ParentLSO, Layer nestedlayer, string uniqueSymbolID,int Layercounter,LayerStateObject ParentSymbol)
        {
            LayerStateObject LSO;
            string uniqueChildID;
            if(JsonOptimize)
            {
                if (nestedlayer.LN == "")
                    uniqueChildID = uniqueSymbolID + "BlankLayer";
                else
                    uniqueChildID = uniqueSymbolID + nestedlayer.LN;
            }
            else
            {
                if (nestedlayer.Layer_name == "")
                    uniqueChildID = uniqueSymbolID + "BlankLayer";
                else
                    uniqueChildID = uniqueSymbolID + nestedlayer.Layer_name;
            }

            if (LayerStateObjDICT.ContainsKey(uniqueChildID))
                LSO = LayerStateObjDICT[uniqueChildID];
            else
            {
                LSO = new LayerStateObject();
                LSO.LayerStateName = uniqueChildID;
                LSO.ParentLayer = ParentLSO.ParentLayer;
                LSO.Clipped_by = nestedlayer.Clipped_by;
                if(JsonOptimize)
                {
                    if (nestedlayer.LT != null)
                        LSO.LT = nestedlayer.LT;
                    if (ParentLSO.parent != null && ParentLSO.parent.LT != null)
                        LSO.LT = ParentLSO.parent.LT;

                    if (LSO.LT != "Clp" && nestedlayer.Clipped_by != null)
                        LSO.Layer_typeClipper = "Clpb";
                }
                else
                {
                    if (nestedlayer.Layer_type != null)
                        LSO.Layer_type = nestedlayer.Layer_type;
                    if (ParentLSO.parent != null && ParentLSO.parent.Layer_type != null)
                        LSO.Layer_type = ParentLSO.parent.Layer_type;

                    if (LSO.Layer_type != "Clipper" && nestedlayer.Clipped_by != null)
                        LSO.Layer_typeClipper = "Clippedby";
                }
                if (ParentLSO.parent != null && ParentLSO.parent.Layer_typeClipper != null)
                    LSO.Layer_typeClipper = ParentLSO.parent.Layer_typeClipper;

                LSO.gobj = new GameObject();
                LSO.gobj.name = LSO.LayerStateName;
                LSO.ParentSymbol = ParentSymbol;
                LSO.parent = ParentLSO;
                if (LSO.Clipped_by != null)
                {
                    if (LSO.Clipped_by == "")
                        LSO.ParentClipper = LayerStateObjDICT[uniqueSymbolID + "BlankLayer"];
                    else
                        LSO.ParentClipper = LayerStateObjDICT[uniqueSymbolID + LSO.Clipped_by];
                }
            }
            if (!ParentLSO.children.Contains(LSO.LayerStateName))
                ParentLSO.children.Add(LSO.LayerStateName);

            return LSO;
        }
        public void AtlasSpriteInitialize(AtlasSprite ele, float zDepth, int currentFrameIndex, int duration, string uniqueSymbolID, LayerStateObject ParentLSO, int counter,Element Parentele,string spritetype)
        {
            string uniqueBitmap;
            if (JsonOptimize)
                uniqueBitmap = uniqueSymbolID + ele.N;
            else
                uniqueBitmap = uniqueSymbolID + ele.name;

            LayerStateObject LSObitmap;
            if (LayerStateObjDICT.ContainsKey(uniqueBitmap))
            {
                LSObitmap = LayerStateObjDICT[uniqueBitmap];
            }
            else
            {
                LSObitmap = new LayerStateObject();
                LSObitmap.LayerStateName = uniqueBitmap;
                LSObitmap.parent = ParentLSO;
                LSObitmap.zDepth = zDepth;
                LSObitmap.SpriteType = spritetype;
                LSObitmap.gobj = new GameObject();
                LSObitmap.gobj.name = uniqueBitmap;
                LSObitmap.anim = LSObitmap.gobj.AddComponent<Animation>();
                LSObitmap.Clipped_by = ParentLSO.Clipped_by;
                if(LSObitmap.SpriteType == "atlas")
					LSObitmap.ParentSymbol = ParentLSO.ParentSymbol;
				else if (LSObitmap.SpriteType == "bitmap")
                    LSObitmap.ParentSymbol = ParentLSO;

                if (LSObitmap.SpriteType == "atlas")
                    LSObitmap.ParentLayer = ParentLSO;
                else if (LSObitmap.SpriteType == "bitmap")
                    LSObitmap.ParentLayer = ParentLSO.ParentLayer;

            }
            int index = currentFrameIndex;
            while (index < (currentFrameIndex + duration))
            {
                decomposedMat decomposed = new decomposedMat();
                if(JsonOptimize)
                    decomposed.Position = ele.POS;
                else
                    decomposed.Position = ele.Position;
                decomposed.Scaling = new Vector3(1, 1, 1);
                LSObitmap.arr[index] = decomposed;
                LSObitmap.sortingorder[index] = counter;

                if (LSObitmap.ParentSymbol.color.ContainsKey(index))
                    LSObitmap.color[index] = LSObitmap.ParentSymbol.color[index];

                if(JsonOptimize)
                {
                    if (LSObitmap.ParentLayer.LT == "Clp")
                        LSObitmap.spriteMask[index] = ele.N;
                    else
                        LSObitmap.spriteName[index] = ele.N;
                }
                else
                {
                    if (LSObitmap.ParentLayer.Layer_type == "Clipper")
                        LSObitmap.spriteMask[index] = ele.name;
                    else
                        LSObitmap.spriteName[index] = ele.name;
                }

                if (LSObitmap.ParentLayer.Layer_typeClipper != null)
                    LSObitmap.Layer_typeClipper = LSObitmap.ParentLayer.Layer_typeClipper;
               
                index++;
            }
            if (!ParentLSO.children.Contains(LSObitmap.LayerStateName))
                ParentLSO.children.Add(LSObitmap.LayerStateName);
            LayerStateObjDICT[LSObitmap.LayerStateName] = LSObitmap;
        }
        public int ProcessSymbolInstance(Element ele, float zDepth, int currentFrameIndex, int duration, string uniqueSymbolID, LayerStateObject ParentLSO, int counter)
        {
            LayerStateObject LSO;
            if (LayerStateObjDICT.ContainsKey(uniqueSymbolID))
                LSO = LayerStateObjDICT[uniqueSymbolID];
            else
            {
                LSO = new LayerStateObject();
                LSO.LayerStateName = uniqueSymbolID;
                LSO.parent = ParentLSO;
                LSO.ParentSymbol = ParentLSO.ParentSymbol;
                LSO.ParentLayer = ParentLSO;
                LSO.gobj = new GameObject();
                LSO.gobj.name = uniqueSymbolID;
                LSO.anim = LSO.gobj.AddComponent<Animation>();
                LSO.Clipped_by = ParentLSO.Clipped_by;
                if(JsonOptimize)
                {
                    if (SymDict.ContainsKey(ele.SI.SN))
                        LSO.SymbolDuration = SymDict[ele.SI.SN].TimelineDuration;
                }
                else
                {
                    if (SymDict.ContainsKey(ele.SYMBOL_Instance.SYMBOL_name))
                        LSO.SymbolDuration = SymDict[ele.SYMBOL_Instance.SYMBOL_name].TimelineDuration;
                }
            }
            int index = currentFrameIndex;
            if(JsonOptimize)
            {
                while (index < (currentFrameIndex + duration))
                {
                    decomposedMat decomposed = new decomposedMat();
                    decomposed = ComputeDecomposedMat(ele.SI.M3D);
                    LSO.arr[index] = decomposed;
                    LSO.sortingorder[index] = counter;

                    LSO.Matarr[index] = ele.SI.M3D;
                    ColorEffect color = new ColorEffect();
                    if (ele.SI.color.M != null && LSO.ParentSymbol.color.Count == 0)
                    {
                        if (ele.SI.color.M == "CA")
                            color = AlphaToAdvanced(ele.SI.color);
                        else if (ele.SI.color.M == "AD")
                            color = Advanced(ele.SI.color);
                        else if (ele.SI.color.M == "T")
                            color = TintToAdvanced(ele.SI.color);
                        else if (ele.SI.color.M == "CBRT")
                            color = BrightnessToAdvanced(ele.SI.color);
                        LSO.color[index] = color;
                    }

                    else if (ele.SI.color.M == null && LSO.ParentSymbol.color.Count != 0)
                    {
                        /*                    int temp = index;
                                            if (index > LSO.ParentSymbol.SymbolDuration)
                                                temp = index % LSO.ParentSymbol.SymbolDuration;
                        */
                        if (LSO.ParentSymbol.color.ContainsKey(index))
                            LSO.color[index] = LSO.ParentSymbol.color[index];
                    }

                    else if (ele.SI.color.M != null && LSO.ParentSymbol.color.Count != 0)
                    {
                        ColorEffect color2 = new ColorEffect();
                        if (ele.SI.color.M == "CA")
                            color = AlphaToAdvanced(ele.SI.color);
                        else if (ele.SI.color.M == "AD")
                            color = Advanced(ele.SI.color);
                        else if (ele.SI.color.M == "T")
                            color = TintToAdvanced(ele.SI.color);
                        else if (ele.SI.color.M == "CBRT")
                            color = BrightnessToAdvanced(ele.SI.color);


                        if (LSO.ParentSymbol.color.ContainsKey(index))
                            color2 = LSO.ParentSymbol.color[index];
                        else
                        {
                            color2.colorMultiplier = new Color(1, 1, 1, 1);
                            color2.colorOffset = new Color(0, 0, 0, 0);
                        }
                        LSO.color[index] = BlendColor(color, color2);
                    }
                    else
                    {
                        color.colorMultiplier = new Color(1, 1, 1, 1);
                        color.colorOffset = new Color(0, 0, 0, 0);
                        LSO.color[index] = color;
                    }

                    index++;
                }
                if (ele.SI.BM.N != null)
                {
                    LSO.Clipped_by = ParentLSO.Clipped_by;
                    LSO.LT = ParentLSO.LT;
                    AtlasSpriteInitialize(ele.SI.BM, zDepth, currentFrameIndex, duration, uniqueSymbolID, LSO, counter, ele, "bitmap");
                }
                else
                {
                    TimeLine nestedTimeline = SymDict[ele.SI.SN];

                    if (ele.SI.ST == "MC")
                    {
                        counter = ProcessMovieClip(ele, nestedTimeline, currentFrameIndex, duration, uniqueSymbolID, LSO, counter);
                    }
                    else if ((ele.SI.ST == "G") && (ele.SI.LP == "LP"))
                    {
                        counter = ProcessGraphicLoop(ele, nestedTimeline, currentFrameIndex, duration, uniqueSymbolID, LSO, counter);
                    }
                    else if ((ele.SI.ST == "G") && (ele.SI.LP == "PO"))
                    {
                        counter = ProcessGraphicPlayOnce(ele, nestedTimeline, currentFrameIndex, duration, uniqueSymbolID, LSO, counter);
                    }
                    else if ((ele.SI.ST == "G") && (ele.SI.LP == "SF"))
                    {
                        counter = ProcessGraphicSingleFrame(ele, nestedTimeline, currentFrameIndex, duration, uniqueSymbolID, LSO, counter);
                    }
                }
            }
            else
            {
                while (index < (currentFrameIndex + duration))
                {
                    decomposedMat decomposed = new decomposedMat();
                    decomposed = ele.SYMBOL_Instance.DecomposedMatrix;
                    LSO.arr[index] = decomposed;
                    LSO.sortingorder[index] = counter;
                    LSO.Matarr[index] = ele.SYMBOL_Instance.Matrix3D;
                    ColorEffect color = new ColorEffect();
                    if (ele.SYMBOL_Instance.color.mode != null && LSO.ParentSymbol.color.Count == 0)
                    {
                        if (ele.SYMBOL_Instance.color.mode == "Alpha")
                            color = AlphaToAdvanced(ele.SYMBOL_Instance.color);
                        else if (ele.SYMBOL_Instance.color.mode == "Advanced")
                            color = Advanced(ele.SYMBOL_Instance.color);
                        else if (ele.SYMBOL_Instance.color.mode == "Tint")
                            color = TintToAdvanced(ele.SYMBOL_Instance.color);
                        else if (ele.SYMBOL_Instance.color.mode == "Brightness")
                            color = BrightnessToAdvanced(ele.SYMBOL_Instance.color);
                        LSO.color[index] = color;
                    }

                    else if (ele.SYMBOL_Instance.color.mode == null && LSO.ParentSymbol.color.Count != 0)
                    {
                        /*                    int temp = index;
                                            if (index > LSO.ParentSymbol.SymbolDuration)
                                                temp = index % LSO.ParentSymbol.SymbolDuration;
                        */
                        if (LSO.ParentSymbol.color.ContainsKey(index))
                            LSO.color[index] = LSO.ParentSymbol.color[index];
                    }

                    else if (ele.SYMBOL_Instance.color.mode != null && LSO.ParentSymbol.color.Count != 0)
                    {
                        ColorEffect color2 = new ColorEffect();
                        if (ele.SYMBOL_Instance.color.mode == "Alpha")
                            color = AlphaToAdvanced(ele.SYMBOL_Instance.color);
                        else if (ele.SYMBOL_Instance.color.mode == "Advanced")
                            color = Advanced(ele.SYMBOL_Instance.color);
                        else if (ele.SYMBOL_Instance.color.mode == "Tint")
                            color = TintToAdvanced(ele.SYMBOL_Instance.color);
                        else if (ele.SYMBOL_Instance.color.mode == "Brightness")
                            color = BrightnessToAdvanced(ele.SYMBOL_Instance.color);


                        if (LSO.ParentSymbol.color.ContainsKey(index))
                            color2 = LSO.ParentSymbol.color[index];
                        else
                        {
                            color2.colorMultiplier = new Color(1, 1, 1, 1);
                            color2.colorOffset = new Color(0, 0, 0, 0);
                        }
                        LSO.color[index] = BlendColor(color, color2);
                    }
                    else
                    {
                        color.colorMultiplier = new Color(1, 1, 1, 1);
                        color.colorOffset = new Color(0, 0, 0, 0);
                        LSO.color[index] = color;
                    }

                    index++;
                }
                if (ele.SYMBOL_Instance.bitmap.name != null)
                {
                    LSO.Clipped_by = ParentLSO.Clipped_by;
                    LSO.Layer_type = ParentLSO.Layer_type;
                    AtlasSpriteInitialize(ele.SYMBOL_Instance.bitmap, zDepth, currentFrameIndex, duration, uniqueSymbolID, LSO, counter, ele, "bitmap");
                }
                else
                {
                    TimeLine nestedTimeline = SymDict[ele.SYMBOL_Instance.SYMBOL_name];

                    if (ele.SYMBOL_Instance.symbolType == "movieclip")
                    {
                        counter = ProcessMovieClip(ele, nestedTimeline, currentFrameIndex, duration, uniqueSymbolID, LSO, counter);
                    }
                    else if ((ele.SYMBOL_Instance.symbolType == "graphic") && (ele.SYMBOL_Instance.loop == "loop"))
                    {
                        counter = ProcessGraphicLoop(ele, nestedTimeline, currentFrameIndex, duration, uniqueSymbolID, LSO, counter);
                    }
                    else if ((ele.SYMBOL_Instance.symbolType == "graphic") && (ele.SYMBOL_Instance.loop == "playonce"))
                    {
                        counter = ProcessGraphicPlayOnce(ele, nestedTimeline, currentFrameIndex, duration, uniqueSymbolID, LSO, counter);
                    }
                    else if ((ele.SYMBOL_Instance.symbolType == "graphic") && (ele.SYMBOL_Instance.loop == "singleframe"))
                    {
                        counter = ProcessGraphicSingleFrame(ele, nestedTimeline, currentFrameIndex, duration, uniqueSymbolID, LSO, counter);
                    }
                }
            }
            LayerStateObjDICT[LSO.LayerStateName] = LSO;
            return counter;
        }

        //Colour Effects
        public ColorEffect AlphaToAdvanced(Colour color)
        {
            ColorEffect c = new ColorEffect();
            if(JsonOptimize)
                c.colorMultiplier = new Color(1, 1, 1, color.AM);
            else
                c.colorMultiplier = new Color(1, 1, 1, color.alphaMultiplier);
            c.colorOffset = new Color(0, 0, 0, 0);
            return c;
        }
        public ColorEffect Advanced(Colour color)
        {
            ColorEffect c = new ColorEffect();
            if(JsonOptimize)
            {
                c.colorMultiplier = new Color(color.RM, color.GM, color.BM, color.AM);
                c.colorOffset = new Color((float)color.RO / 255, (float)color.GO / 255, (float)color.BO / 255, (float)color.AO / 255);
            }
            else
            {
                c.colorMultiplier = new Color(color.RedMultiplier, color.greenMultiplier, color.blueMultiplier, color.alphaMultiplier);
                c.colorOffset = new Color((float)color.redOffset / 255, (float)color.greenOffset / 255, (float)color.blueOffset / 255, (float)color.AlphaOffset / 255);
            }
            return c;
        }
        public ColorEffect TintToAdvanced(Colour color)
        {
            ColorEffect c = new ColorEffect();
            string r;
            string g;
            string b;
            if (JsonOptimize)
            {
                r = color.TC.Substring(1, 2);
                g = color.TC.Substring(3, 2);
                b = color.TC.Substring(5, 2);
            }
            else
            {
                r = color.tintColor.Substring(1, 2);
                g = color.tintColor.Substring(3, 2);
                b = color.tintColor.Substring(5, 2);
            }
            float cr = int.Parse(r, System.Globalization.NumberStyles.HexNumber);
            float cg = int.Parse(g, System.Globalization.NumberStyles.HexNumber);
            float cb = int.Parse(b, System.Globalization.NumberStyles.HexNumber);
            float tint;
            if (JsonOptimize)
                tint = color.TM;
            else
                tint = color.tintMultiplier;
            c.colorMultiplier = new Color((1 - tint), (1 - tint), (1 - tint), 1);
            c.colorOffset = new Color((float)(cr*tint / 255), (float)(cg*tint / 255), (float)(cb*tint / 255), 0);
            return c;
        }
        public ColorEffect BrightnessToAdvanced(Colour color)
        {
            ColorEffect c = new ColorEffect();
            if(JsonOptimize)
            {
                if (color.BRT < 0)
                {
                    c.colorMultiplier = new Color((1 + color.BRT), (1 + color.BRT), (1 + color.BRT), 1);
                    c.colorOffset = new Color(0, 0, 0, 0);
                }
                else
                {
                    c.colorMultiplier = new Color((1 - color.BRT), (1 - color.BRT), (1 - color.BRT), 1);
                    c.colorOffset = new Color(color.BRT, color.BRT, color.BRT, 0);
                }
            }
            if(color.brightness<0)
            {
                c.colorMultiplier = new Color((1+color.brightness), (1+color.brightness), (1+color.brightness),1);
                c.colorOffset = new Color(0,0,0, 0);
            }
            else
            {
                c.colorMultiplier = new Color((1 - color.brightness), (1 - color.brightness), (1 - color.brightness), 1);
                c.colorOffset = new Color(color.brightness, color.brightness, color.brightness, 0);
            }
            return c;
        }
        public ColorEffect BlendColor(ColorEffect child, ColorEffect parent)
        {
            ColorEffect blendcolor = new ColorEffect();
            blendcolor.colorMultiplier.r = parent.colorMultiplier.r * child.colorMultiplier.r;
            blendcolor.colorOffset.r = (parent.colorMultiplier.r * child.colorOffset.r) + parent.colorOffset.r;
            blendcolor.colorMultiplier.g = parent.colorMultiplier.g * child.colorMultiplier.g;
            blendcolor.colorOffset.g = (parent.colorMultiplier.g * child.colorOffset.g) + parent.colorOffset.g;
            blendcolor.colorMultiplier.b = parent.colorMultiplier.b * child.colorMultiplier.b;
            blendcolor.colorOffset.b = (parent.colorMultiplier.b * child.colorOffset.b) + parent.colorOffset.b;
            blendcolor.colorMultiplier.a = parent.colorMultiplier.a * child.colorMultiplier.a;
            blendcolor.colorOffset.a = (parent.colorMultiplier.a * child.colorOffset.a) + parent.colorOffset.a;
            return blendcolor;
        }
        //Types of Symbols
        public int ProcessMovieClip(Element ele, TimeLine nestedTimeline, int currentFrameIndex, int duration, string uniqueSymbolID, LayerStateObject ParentLSO, int SymbolCounter)
        {
            int NestedFrameDuration = 0, finalDuration = 0;
            int loop = 0;
            int NestedFrameIndex;
            int loopvalue = 0;
            int loopvalue2 = 0;
            int loop2 = 0;
            int ElementCount = 0;
            int Layercounter = SymbolCounter;
            Dictionary<string, int> LayerStateTemp = new Dictionary<string, int>();
            if(JsonOptimize)
            {
                do
                {
                    foreach (var nestedlayer in nestedTimeline.L)
                    {
                        LayerStateObject LSO = LayerInitialize(ParentLSO, nestedlayer, uniqueSymbolID, Layercounter, ParentLSO);
                        string uniqueSymbolIDtemp = uniqueSymbolID + nestedlayer.LN;
                        int firstframe = 0;
                        int frameindex = 0;
                        if (loop == 0)
                        {
                            LSO.sortingorder[currentFrameIndex] = Layercounter;

                            if (LayerState.ContainsKey(uniqueSymbolIDtemp))
                                firstframe = LayerState[uniqueSymbolIDtemp];
                            for (int i = 0; i < nestedlayer.FR.Length; i++)
                            {
                                Frame frame = nestedlayer.FR[i];
                                if ((firstframe >= frame.I) && (firstframe < frame.I + frame.DU))
                                {
                                    frameindex = i;
                                    break;
                                }
                            }
                        }
                        NestedFrameIndex = currentFrameIndex + (loop2);
                        for (int j = frameindex; j < nestedlayer.FR.Length; j++)
                        {
                            List<string> tempFrame = new List<string>();
                            int counter = 1;
                            var frame = nestedlayer.FR[j];
                            if (j == frameindex)
                                NestedFrameDuration = frame.DU + frame.I - firstframe;
                            else
                                NestedFrameDuration = frame.DU;

                            if ((NestedFrameIndex + NestedFrameDuration) < (currentFrameIndex + duration))
                                finalDuration = NestedFrameDuration;
                            else
                                finalDuration = (currentFrameIndex + duration) - NestedFrameIndex;


                            int nestedElementCounter = Layercounter;
                            if (ElementCount < frame.E.Length)
                                ElementCount = frame.E.Length;
                            for (int i = frame.E.Length - 1; i >= 0; i--)
                            {
                                var nestedElement = frame.E[i];
                                if (nestedElement.ASI.N != null)
                                {
                                    AtlasSpriteInitialize(nestedElement.ASI, frame.zDepth, NestedFrameIndex, finalDuration, LSO.LayerStateName, LSO, nestedElementCounter, nestedElement, "atlas");
                                }
                                else if (nestedElement.SI.SN != null)
                                {
                                    string ID = LSO.LayerStateName + "_" + nestedElement.SI.SN + nestedElement.SI.IN + counter;
                                    while (tempFrame.Contains(ID))
                                    {
                                        counter++;
                                        ID = LSO.LayerStateName + "_" + nestedElement.SI.SN + nestedElement.SI.IN + counter;
                                    }
                                    tempFrame.Add(ID);
                                    nestedElementCounter = ProcessSymbolInstance(nestedElement, frame.zDepth, NestedFrameIndex, finalDuration, ID, LSO, nestedElementCounter);
                                    if (!LSO.children.Contains(ID))
                                        LSO.children.Add(ID);
                                }
                                nestedElementCounter++;
                            }
                            NestedFrameIndex += finalDuration;
                            if (loopvalue < NestedFrameIndex)
                            {
                                loopvalue = NestedFrameIndex;
                                loopvalue2 += finalDuration;
                            }
                            if (NestedFrameIndex >= (currentFrameIndex + duration))
                                break;
                        }
                        LayerStateTemp[uniqueSymbolIDtemp] = (NestedFrameIndex % nestedTimeline.TimelineDuration);
                        if (!LayerStateObjDICT.ContainsKey(LSO.LayerStateName))
                            LayerStateObjDICT[LSO.LayerStateName] = LSO;
                        Layercounter += ElementCount;
                    }
                    loop = loopvalue;
                    loop2 = loopvalue2;
                    foreach (var item in LayerStateTemp)
                    {
                        LayerState[item.Key] = item.Value;
                    }
                } while (loop < (currentFrameIndex + duration));
            }
            else
            {
                do
                {
                    foreach (var nestedlayer in nestedTimeline.LAYERS)
                    {
                        LayerStateObject LSO = LayerInitialize(ParentLSO, nestedlayer, uniqueSymbolID, Layercounter, ParentLSO);
                        string uniqueSymbolIDtemp = uniqueSymbolID + nestedlayer.Layer_name;
                        int firstframe = 0;
                        int frameindex = 0;
                        if (loop == 0)
                        {
                            LSO.sortingorder[currentFrameIndex] = Layercounter;

                            if (LayerState.ContainsKey(uniqueSymbolIDtemp))
                                firstframe = LayerState[uniqueSymbolIDtemp];
                            for (int i = 0; i < nestedlayer.Frames.Length; i++)
                            {
                                Frame frame = nestedlayer.Frames[i];
                                if ((firstframe >= frame.index) && (firstframe < frame.index + frame.duration))
                                {
                                    frameindex = i;
                                    break;
                                }
                            }
                        }
                        NestedFrameIndex = currentFrameIndex + (loop2);
                        for (int j = frameindex; j < nestedlayer.Frames.Length; j++)
                        {
                            List<string> tempFrame = new List<string>();
                            int counter = 1;
                            var frame = nestedlayer.Frames[j];
                            if (j == frameindex)
                                NestedFrameDuration = frame.duration + frame.index - firstframe;
                            else
                                NestedFrameDuration = frame.duration;

                            if ((NestedFrameIndex + NestedFrameDuration) < (currentFrameIndex + duration))
                                finalDuration = NestedFrameDuration;
                            else
                                finalDuration = (currentFrameIndex + duration) - NestedFrameIndex;


                            int nestedElementCounter = Layercounter;
                            if (ElementCount < frame.elements.Length)
                                ElementCount = frame.elements.Length;
                            for (int i = frame.elements.Length - 1; i >= 0; i--)
                            {
                                var nestedElement = frame.elements[i];
                                if (nestedElement.ATLAS_SPRITE_instance.name != null)
                                {
                                    AtlasSpriteInitialize(nestedElement.ATLAS_SPRITE_instance, frame.zDepth, NestedFrameIndex, finalDuration, LSO.LayerStateName, LSO, nestedElementCounter, nestedElement, "atlas");
                                }
                                else if (nestedElement.SYMBOL_Instance.SYMBOL_name != null)
                                {
                                    string ID = LSO.LayerStateName + "_" + nestedElement.SYMBOL_Instance.SYMBOL_name + nestedElement.SYMBOL_Instance.Instance_Name + counter;
                                    while (tempFrame.Contains(ID))
                                    {
                                        counter++;
                                        ID = LSO.LayerStateName + "_" + nestedElement.SYMBOL_Instance.SYMBOL_name + nestedElement.SYMBOL_Instance.Instance_Name + counter;
                                    }
                                    tempFrame.Add(ID);
                                    nestedElementCounter = ProcessSymbolInstance(nestedElement, frame.zDepth, NestedFrameIndex, finalDuration, ID, LSO, nestedElementCounter);
                                    if (!LSO.children.Contains(ID))
                                        LSO.children.Add(ID);
                                }
                                nestedElementCounter++;
                            }
                            NestedFrameIndex += finalDuration;
                            if (loopvalue < NestedFrameIndex)
                            {
                                loopvalue = NestedFrameIndex;
                                loopvalue2 += finalDuration;
                            }
                            if (NestedFrameIndex >= (currentFrameIndex + duration))
                                break;
                        }
                        LayerStateTemp[uniqueSymbolIDtemp] = (NestedFrameIndex % nestedTimeline.TimelineDuration);
                        if (!LayerStateObjDICT.ContainsKey(LSO.LayerStateName))
                            LayerStateObjDICT[LSO.LayerStateName] = LSO;
                        Layercounter += ElementCount;
                    }
                    loop = loopvalue;
                    loop2 = loopvalue2;
                    foreach (var item in LayerStateTemp)
                    {
                        LayerState[item.Key] = item.Value;
                    }
                } while (loop < (currentFrameIndex + duration));
            }
            
            return Layercounter;
        }
        public int ProcessGraphicLoop(Element ele, TimeLine nestedTimeline, int currentFrameIndex, int duration, string uniqueSymbolID, LayerStateObject ParentLSO, int SymbolCounter)
        {
            int NestedFrameDuration = 0, finalDuration = 0;
            int loop = 0;
            int NestedFrameIndex;
            int loopvalue = 0;
            int ElementCount = 0;
            int Layercounter = SymbolCounter;
            if(JsonOptimize)
            {
                do
                {
                    foreach (var nestedlayer in nestedTimeline.L)
                    {
                        LayerStateObject LSO = LayerInitialize(ParentLSO, nestedlayer, uniqueSymbolID, Layercounter, ParentLSO);
                        int firstframe = 0;
                        int frameindex = 0;
                        if (loop == 0)
                        {
                            LSO.sortingorder[currentFrameIndex] = Layercounter;
                            firstframe = ele.SI.FF - 1;
                            for (int i = 0; i < nestedlayer.FR.Length; i++)
                            {
                                Frame frame = nestedlayer.FR[i];
                                if ((firstframe >= frame.I) && (firstframe < frame.I + frame.DU))
                                {
                                    frameindex = i;
                                    break;
                                }
                            }
                        }
                        NestedFrameIndex = currentFrameIndex + (loop);
                        for (int j = frameindex; j < nestedlayer.FR.Length; j++)
                        {
                            List<string> tempFrame = new List<string>();
                            int counter = 1;
                            var frame = nestedlayer.FR[j];
                            if (j == frameindex)
                                NestedFrameDuration = frame.DU + frame.I - firstframe;
                            else
                                NestedFrameDuration = frame.DU;

                            if ((NestedFrameIndex + NestedFrameDuration) < (currentFrameIndex + duration))
                                finalDuration = NestedFrameDuration;
                            else
                                finalDuration = (currentFrameIndex + duration) - NestedFrameIndex;

                            int nestedElementCounter = Layercounter;
                            if (ElementCount < frame.E.Length)
                                ElementCount = frame.E.Length;
                            for (int i = frame.E.Length - 1; i >= 0; i--)
                            {
                                var nestedElement = frame.E[i];
                                if (nestedElement.ASI.N != null)
                                {
                                    string ID = LSO.LayerStateName + "_" + nestedElement.ASI.N + counter;
                                    while (tempFrame.Contains(ID))
                                    {
                                        counter++;
                                        ID = LSO.LayerStateName + "_" + nestedElement.ASI.N + counter;
                                    }
                                    tempFrame.Add(ID);
                                    AtlasSpriteInitialize(nestedElement.ASI, frame.zDepth, NestedFrameIndex, finalDuration, ID, LSO, nestedElementCounter, nestedElement, "atlas");
                                    if (!LSO.children.Contains(ID))
                                        LSO.children.Add(ID);
                                }
                                else if (nestedElement.SI.SN != null)
                                {
                                    string ID = LSO.LayerStateName + "_" + nestedElement.SI.SN + nestedElement.SI.IN + counter;
                                    while (tempFrame.Contains(ID))
                                    {
                                        counter++;
                                        ID = LSO.LayerStateName + "_" + nestedElement.SI.SN + nestedElement.SI.IN + counter;
                                    }
                                    tempFrame.Add(ID);
                                    nestedElementCounter = ProcessSymbolInstance(nestedElement, frame.zDepth, NestedFrameIndex, finalDuration, ID, LSO, nestedElementCounter);
                                    if (!LSO.children.Contains(ID))
                                        LSO.children.Add(ID);
                                }
                                nestedElementCounter++;
                            }
                            NestedFrameIndex += finalDuration;
                            if (loopvalue < NestedFrameIndex)
                                loopvalue = NestedFrameIndex;
                            if (NestedFrameIndex >= (currentFrameIndex + duration))
                                break;
                        }
                        if (!LayerStateObjDICT.ContainsKey(LSO.LayerStateName))
                            LayerStateObjDICT[LSO.LayerStateName] = LSO;
                        Layercounter += ElementCount;
                    }
                    loop = loopvalue;
                } while (loop < (currentFrameIndex + duration));
            }
            else
            {
                do
                {
                    foreach (var nestedlayer in nestedTimeline.LAYERS)
                    {
                        LayerStateObject LSO = LayerInitialize(ParentLSO, nestedlayer, uniqueSymbolID, Layercounter, ParentLSO);
                        int firstframe = 0;
                        int frameindex = 0;
                        if (loop == 0)
                        {
                            LSO.sortingorder[currentFrameIndex] = Layercounter;
                            firstframe = ele.SYMBOL_Instance.firstFrame - 1;
                            for (int i = 0; i < nestedlayer.Frames.Length; i++)
                            {
                                Frame frame = nestedlayer.Frames[i];
                                if ((firstframe >= frame.index) && (firstframe < frame.index + frame.duration))
                                {
                                    frameindex = i;
                                    break;
                                }
                            }
                        }
                        NestedFrameIndex = currentFrameIndex + (loop);
                        for (int j = frameindex; j < nestedlayer.Frames.Length; j++)
                        {
                            List<string> tempFrame = new List<string>();
                            int counter = 1;
                            var frame = nestedlayer.Frames[j];
                            if (j == frameindex)
                                NestedFrameDuration = frame.duration + frame.index - firstframe;
                            else
                                NestedFrameDuration = frame.duration;

                            if ((NestedFrameIndex + NestedFrameDuration) < (currentFrameIndex + duration))
                                finalDuration = NestedFrameDuration;
                            else
                                finalDuration = (currentFrameIndex + duration) - NestedFrameIndex;

                            int nestedElementCounter = Layercounter;
                            if (ElementCount < frame.elements.Length)
                                ElementCount = frame.elements.Length;
                            for (int i = frame.elements.Length - 1; i >= 0; i--)
                            {
                                var nestedElement = frame.elements[i];
                                if (nestedElement.ATLAS_SPRITE_instance.name != null)
                                {
                                    string ID = LSO.LayerStateName + "_" + nestedElement.ATLAS_SPRITE_instance.name + counter;
                                    while (tempFrame.Contains(ID))
                                    {
                                        counter++;
                                        ID = LSO.LayerStateName + "_" + nestedElement.ATLAS_SPRITE_instance.name + counter;
                                    }
                                    tempFrame.Add(ID);
                                    AtlasSpriteInitialize(nestedElement.ATLAS_SPRITE_instance, frame.zDepth, NestedFrameIndex, finalDuration, ID, LSO, nestedElementCounter, nestedElement, "atlas");
                                    if (!LSO.children.Contains(ID))
                                        LSO.children.Add(ID);
                                }
                                else if (nestedElement.SYMBOL_Instance.SYMBOL_name != null)
                                {
                                    string ID = LSO.LayerStateName + "_" + nestedElement.SYMBOL_Instance.SYMBOL_name + nestedElement.SYMBOL_Instance.Instance_Name + counter;
                                    while (tempFrame.Contains(ID))
                                    {
                                        counter++;
                                        ID = LSO.LayerStateName + "_" + nestedElement.SYMBOL_Instance.SYMBOL_name + nestedElement.SYMBOL_Instance.Instance_Name + counter;
                                    }
                                    tempFrame.Add(ID);
                                    nestedElementCounter = ProcessSymbolInstance(nestedElement, frame.zDepth, NestedFrameIndex, finalDuration, ID, LSO, nestedElementCounter);
                                    if (!LSO.children.Contains(ID))
                                        LSO.children.Add(ID);
                                }
                                nestedElementCounter++;
                            }
                            NestedFrameIndex += finalDuration;
                            if (loopvalue < NestedFrameIndex)
                                loopvalue = NestedFrameIndex;
                            if (NestedFrameIndex >= (currentFrameIndex + duration))
                                break;
                        }
                        if (!LayerStateObjDICT.ContainsKey(LSO.LayerStateName))
                            LayerStateObjDICT[LSO.LayerStateName] = LSO;
                        Layercounter += ElementCount;
                    }
                    loop = loopvalue;
                } while (loop < (currentFrameIndex + duration));
            }
            
            return Layercounter;
        }
        public int ProcessGraphicPlayOnce(Element ele, TimeLine nestedTimeline, int currentFrameIndex, int duration, string uniqueSymbolID, LayerStateObject ParentLSO, int SymbolCounter)
        {
            int NestedFrameDuration = 0, finalDuration = 0;
            int NestedFrameIndex;
            int ElementCount = 0;
            int Layercounter = SymbolCounter;
            if(JsonOptimize)
            {
                foreach (var nestedlayer in nestedTimeline.L)
                {
                    LayerStateObject LSO = LayerInitialize(ParentLSO, nestedlayer, uniqueSymbolID, Layercounter, ParentLSO);
                    int firstframe = ele.SI.FF - 1;
                    int frameindex = 0;
                    for (int i = 0; i < nestedlayer.FR.Length; i++)
                    {
                        Frame frame = nestedlayer.FR[i];
                        if ((firstframe >= frame.I) && (firstframe < frame.I + frame.DU))
                        {
                            frameindex = i;
                            break;
                        }
                    }
                    NestedFrameIndex = currentFrameIndex;
                    for (int j = frameindex; j < nestedlayer.FR.Length; j++)
                    {
                        List<string> tempFrame = new List<string>();
                        int counter = 1;
                        var frame = nestedlayer.FR[j];
                        if (j == frameindex)
                            NestedFrameDuration = frame.DU + frame.I - firstframe;
                        else
                            NestedFrameDuration = frame.DU;

                        if ((NestedFrameIndex + NestedFrameDuration) < (currentFrameIndex + duration))
                            finalDuration = NestedFrameDuration;
                        else
                            finalDuration = (currentFrameIndex + duration) - NestedFrameIndex;
                        int nestedElementCounter = Layercounter;
                        if (ElementCount < frame.E.Length)
                            ElementCount = frame.E.Length;
                        for (int i = frame.E.Length - 1; i >= 0; i--)
                        {
                            var nestedElement = frame.E[i];
                            if (nestedElement.ASI.N != null)
                            {
                                string ID = LSO.LayerStateName + "_" + nestedElement.ASI.N + counter;
                                while (tempFrame.Contains(ID))
                                {
                                    counter++;
                                    ID = LSO.LayerStateName + "_" + nestedElement.ASI.N + counter;
                                }
                                tempFrame.Add(ID);
                                AtlasSpriteInitialize(nestedElement.ASI, frame.zDepth, NestedFrameIndex, finalDuration, ID, LSO, nestedElementCounter, nestedElement, "atlas");
                                if (!LSO.children.Contains(ID))
                                    LSO.children.Add(ID);
                            }
                            else if (nestedElement.SI.SN != null)
                            {
                                string ID = LSO.LayerStateName + "_" + nestedElement.SI.SN + nestedElement.SI.IN + counter;
                                while (tempFrame.Contains(ID))
                                {
                                    counter++;
                                    ID = LSO.LayerStateName + "_" + nestedElement.SI.SN + nestedElement.SI.IN + counter;
                                }
                                tempFrame.Add(ID);
                                nestedElementCounter = ProcessSymbolInstance(nestedElement, frame.zDepth, NestedFrameIndex, finalDuration, ID, LSO, nestedElementCounter);
                                if (!LSO.children.Contains(ID))
                                    LSO.children.Add(ID);
                            }
                            nestedElementCounter++;
                        }
                        NestedFrameIndex += NestedFrameDuration;
                        if (NestedFrameIndex >= (currentFrameIndex + duration))
                            break;
                    }
                    if (!LayerStateObjDICT.ContainsKey(LSO.LayerStateName))
                        LayerStateObjDICT[LSO.LayerStateName] = LSO;
                    Layercounter += ElementCount;
                }
            }
            else
            {
                foreach (var nestedlayer in nestedTimeline.LAYERS)
                {
                    LayerStateObject LSO = LayerInitialize(ParentLSO, nestedlayer, uniqueSymbolID, Layercounter, ParentLSO);
                    int firstframe = ele.SYMBOL_Instance.firstFrame - 1;
                    int frameindex = 0;
                    for (int i = 0; i < nestedlayer.Frames.Length; i++)
                    {
                        Frame frame = nestedlayer.Frames[i];
                        if ((firstframe >= frame.index) && (firstframe < frame.index + frame.duration))
                        {
                            frameindex = i;
                            break;
                        }
                    }
                    NestedFrameIndex = currentFrameIndex;
                    for (int j = frameindex; j < nestedlayer.Frames.Length; j++)
                    {
                        List<string> tempFrame = new List<string>();
                        int counter = 1;
                        var frame = nestedlayer.Frames[j];
                        if (j == frameindex)
                            NestedFrameDuration = frame.duration + frame.index - firstframe;
                        else
                            NestedFrameDuration = frame.duration;

                        if ((NestedFrameIndex + NestedFrameDuration) < (currentFrameIndex + duration))
                            finalDuration = NestedFrameDuration;
                        else
                            finalDuration = (currentFrameIndex + duration) - NestedFrameIndex;
                        int nestedElementCounter = Layercounter;
                        if (ElementCount < frame.elements.Length)
                            ElementCount = frame.elements.Length;
                        for (int i = frame.elements.Length - 1; i >= 0; i--)
                        {
                            var nestedElement = frame.elements[i];
                            if (nestedElement.ATLAS_SPRITE_instance.name != null)
                            {
                                string ID = LSO.LayerStateName + "_" + nestedElement.ATLAS_SPRITE_instance.name + counter;
                                while (tempFrame.Contains(ID))
                                {
                                    counter++;
                                    ID = LSO.LayerStateName + "_" + nestedElement.ATLAS_SPRITE_instance.name + counter;
                                }
                                tempFrame.Add(ID);
                                AtlasSpriteInitialize(nestedElement.ATLAS_SPRITE_instance, frame.zDepth, NestedFrameIndex, finalDuration, ID, LSO, nestedElementCounter, nestedElement, "atlas");
                                if (!LSO.children.Contains(ID))
                                    LSO.children.Add(ID);
                            }
                            else if (nestedElement.SYMBOL_Instance.SYMBOL_name != null)
                            {
                                string ID = LSO.LayerStateName + "_" + nestedElement.SYMBOL_Instance.SYMBOL_name + nestedElement.SYMBOL_Instance.Instance_Name + counter;
                                while (tempFrame.Contains(ID))
                                {
                                    counter++;
                                    ID = LSO.LayerStateName + "_" + nestedElement.SYMBOL_Instance.SYMBOL_name + nestedElement.SYMBOL_Instance.Instance_Name + counter;
                                }
                                tempFrame.Add(ID);
                                nestedElementCounter = ProcessSymbolInstance(nestedElement, frame.zDepth, NestedFrameIndex, finalDuration, ID, LSO, nestedElementCounter);
                                if (!LSO.children.Contains(ID))
                                    LSO.children.Add(ID);
                            }
                            nestedElementCounter++;
                        }
                        NestedFrameIndex += NestedFrameDuration;
                        if (NestedFrameIndex >= (currentFrameIndex + duration))
                            break;
                    }
                    if (!LayerStateObjDICT.ContainsKey(LSO.LayerStateName))
                        LayerStateObjDICT[LSO.LayerStateName] = LSO;
                    Layercounter += ElementCount;
                }
            }
            
            return Layercounter;
        }
        public int ProcessGraphicSingleFrame(Element ele, TimeLine nestedTimeline, int currentFrameIndex, int duration, string uniqueSymbolID, LayerStateObject ParentLSO, int SymbolCounter)
        {
            int ElementCount = 0;
            int Layercounter = SymbolCounter;
            if(JsonOptimize)
            {
                foreach (var nestedlayer in nestedTimeline.L)
                {
                    LayerStateObject LSO = LayerInitialize(ParentLSO, nestedlayer, uniqueSymbolID, Layercounter, ParentLSO);
                    int firstframe = ele.SI.FF - 1;
                    Frame requiredframe = nestedlayer.FR[0];
                    foreach (var frame in nestedlayer.FR)
                    {
                        if ((firstframe >= frame.I) && (firstframe < frame.I + frame.DU))
                        {
                            requiredframe = frame;
                            break;
                        }
                    }
                    List<string> tempFrame = new List<string>();
                    int counter = 1;
                    int nestedElementCounter = Layercounter;
                    if (ElementCount < requiredframe.E.Length)
                        ElementCount = requiredframe.E.Length;
                    for (int i = requiredframe.E.Length - 1; i >= 0; i--)
                    {
                        var nestedElement = requiredframe.E[i];
                        if (nestedElement.ASI.N != null)
                        {
                            AtlasSpriteInitialize(nestedElement.ASI, requiredframe.zDepth, currentFrameIndex, duration, LSO.LayerStateName, LSO, nestedElementCounter, nestedElement, "atlas");
                        }
                        else if (nestedElement.SI.SN != null)
                        {
                            string ID = LSO.LayerStateName + "_" + nestedElement.SI.SN + nestedElement.SI.IN + counter;
                            while (tempFrame.Contains(ID))
                            {
                                counter++;
                                ID = LSO.LayerStateName + "_" + nestedElement.SI.SN + nestedElement.SI.IN + counter;
                            }
                            tempFrame.Add(ID);
                            nestedElementCounter = ProcessSymbolInstance(nestedElement, requiredframe.zDepth, currentFrameIndex, duration, ID, LSO, nestedElementCounter);
                            if (!LSO.children.Contains(ID))
                                LSO.children.Add(ID);
                        }
                        nestedElementCounter++;
                    }
                    if (!LayerStateObjDICT.ContainsKey(LSO.LayerStateName))
                        LayerStateObjDICT[LSO.LayerStateName] = LSO;
                    Layercounter += ElementCount;
                }
            }
            else
            {
                foreach (var nestedlayer in nestedTimeline.LAYERS)
                {
                    LayerStateObject LSO = LayerInitialize(ParentLSO, nestedlayer, uniqueSymbolID, Layercounter, ParentLSO);
                    int firstframe = ele.SYMBOL_Instance.firstFrame - 1;
                    Frame requiredframe = nestedlayer.Frames[0];
                    foreach (var frame in nestedlayer.Frames)
                    {
                        if ((firstframe >= frame.index) && (firstframe < frame.index + frame.duration))
                        {
                            requiredframe = frame;
                            break;
                        }
                    }
                    List<string> tempFrame = new List<string>();
                    int counter = 1;
                    int nestedElementCounter = Layercounter;
                    if (ElementCount < requiredframe.elements.Length)
                        ElementCount = requiredframe.elements.Length;
                    for (int i = requiredframe.elements.Length - 1; i >= 0; i--)
                    {
                        var nestedElement = requiredframe.elements[i];
                        if (nestedElement.ATLAS_SPRITE_instance.name != null)
                        {
                            AtlasSpriteInitialize(nestedElement.ATLAS_SPRITE_instance, requiredframe.zDepth, currentFrameIndex, duration, LSO.LayerStateName, LSO, nestedElementCounter, nestedElement, "atlas");
                        }
                        else if (nestedElement.SYMBOL_Instance.SYMBOL_name != null)
                        {
                            string ID = LSO.LayerStateName + "_" + nestedElement.SYMBOL_Instance.SYMBOL_name + nestedElement.SYMBOL_Instance.Instance_Name + counter;
                            while (tempFrame.Contains(ID))
                            {
                                counter++;
                                ID = LSO.LayerStateName + "_" + nestedElement.SYMBOL_Instance.SYMBOL_name + nestedElement.SYMBOL_Instance.Instance_Name + counter;
                            }
                            tempFrame.Add(ID);
                            nestedElementCounter = ProcessSymbolInstance(nestedElement, requiredframe.zDepth, currentFrameIndex, duration, ID, LSO, nestedElementCounter);
                            if (!LSO.children.Contains(ID))
                                LSO.children.Add(ID);
                        }
                        nestedElementCounter++;
                    }
                    if (!LayerStateObjDICT.ContainsKey(LSO.LayerStateName))
                        LayerStateObjDICT[LSO.LayerStateName] = LSO;
                    Layercounter += ElementCount;
                }
            }
            
            return Layercounter;
        }
        public Vector3 GetScaling()
        {
            Vector3 scale = new Vector3(1.0f, 1.0f, 1.0f);
            if (ScaleDemat != null)
            {
                scale = ScaleDemat.Scaling;
            }
            return scale;
        }
        void SetScaling()
        {
            Vector3 scale = mainGameObject.gobj.transform.localScale;
            Vector3 ResScale = GetScaling();
            if (ScaleDemat != null)
            {
                if (!float.IsNaN(ResScale.x) && !float.IsNaN(ResScale.y) && !float.IsNaN(ResScale.z))
                {
                    if (ResScale.x != 1.0f || ResScale.y != 1.0f || ResScale.z != 1.0f)
                    {
                        if(ResScale.x != 0)
                            scale.x *= ResScale.x;
                        if (ResScale.y != 0)
                            scale.y *= ResScale.y;
                        if (ResScale.z != 0)
                            scale.z *= ResScale.z;
                        mainGameObject.gobj.transform.localScale = scale;
                    }
                }
            }
        }
        //Playing the Timeline
        public void PlayAnimation(string framelevel)
        {
            CreateGameObject(mainGameObject.LayerStateName, null, framelevel);
            SetScaling();
            PlayGameObject(mainGameObject.LayerStateName, framelevel);
            updateCounter = startCounter;
        }
        public void CreateGameObject(string name, GameObject Parent, string framelevel)
        {
            if (LayerStateObjDICT.ContainsKey(name))
            {
                LayerStateObject ls = LayerStateObjDICT[name];
                if (ls.spriteName.Count != 0)
                {
                    if (ls.gobj.GetComponent<SpriteRenderer>() == null)
                        ls.gobj.AddComponent<SpriteRenderer>();
                    ls.gobj.GetComponent<SpriteRenderer>().enabled = false;
                    ls.gobj.GetComponent<SpriteRenderer>().material.shader = Shader.Find("Sprites/Effect");
#if UNITY_2017
                    if (ls.Layer_typeClipper != null)
                        ls.gobj.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
#endif
                }
#if UNITY_2017
                if (ls.spriteMask.Count != 0)
                {
                    ls.gobj.AddComponent<SpriteMask>();
                    ls.gobj.GetComponent<SpriteMask>().isCustomRangeActive = true;
                }
#endif
                if (Parent != null)
                    ls.gobj.transform.parent = Parent.transform;
                if (name == mainGameObject.LayerStateName && mainGameObject.arr.Count != 0)
                {
                    mainGameObject.gobj.transform.position = mainGameObject.arr[0].Position;
                    mainGameObject.gobj.transform.rotation = Quaternion.Euler(mainGameObject.arr[0].Rotation * Mathf.Rad2Deg);
                    mainGameObject.gobj.transform.localScale = mainGameObject.arr[0].Scaling;
                }
                else if (ls.arr.Count > 0)
                {
                    AnimationCurve PositioncurveX = new AnimationCurve();
                    AnimationCurve PositioncurveY = new AnimationCurve();
                    AnimationCurve PositioncurveZ = new AnimationCurve();
                    AnimationCurve RotationcurveX = new AnimationCurve();
                    AnimationCurve RotationcurveY = new AnimationCurve();
                    AnimationCurve RotationcurveZ = new AnimationCurve();
                    AnimationCurve RotationcurveW = new AnimationCurve();
                    AnimationCurve ScalingcurveX = new AnimationCurve();
                    AnimationCurve ScalingcurveY = new AnimationCurve();
                    AnimationCurve ScalingcurveZ = new AnimationCurve();
                    AnimationClip clip = new AnimationClip();
                    clip.legacy = true;
                    int i = 0; bool PlayTimelineflag = true;
                    if (FrameLevelDict.Count == 0)
                        i = 0;
                    else if (FrameLevelDict.ContainsKey(framelevel))
                    {
                        i = FrameLevelDict[framelevel];
                        startCounter = i;
                        PlayTimelineflag = false;
                    }
                    do
                    {
                        if (ls.arr.ContainsKey(i))
                        {
                            decomposedMat decomposed1 = ls.arr[i];
                            if(ls.spriteName.ContainsKey(i) && SpriteDict[ls.spriteName[i]].rotated && SpriteDict.ContainsKey(ls.spriteName[i]))
                            {
                                Quaternion value = Quaternion.Euler(new Vector3(0, 0, 270));
                                RotationcurveX.AddKey(i, value.x);
                                RotationcurveY.AddKey(i, value.y);
                                RotationcurveZ.AddKey(i, value.z);
                                RotationcurveW.AddKey(i, value.w);
                                decomposed1.Position += new Vector3(0, SpriteDict[ls.spriteName[i]].sp.rect.width, 0);
                            }
                            if (ls.spriteMask.ContainsKey(i) && SpriteDict[ls.spriteMask[i]].rotated && SpriteDict.ContainsKey(ls.spriteMask[i]))
                            {
                                Quaternion value = Quaternion.Euler(new Vector3(0,0,90));
                                RotationcurveX.AddKey(i, value.x);
                                RotationcurveY.AddKey(i, value.y);
                                RotationcurveZ.AddKey(i, value.z);
                                RotationcurveW.AddKey(i, value.w);
                                decomposed1.Position += new Vector3(0, SpriteDict[ls.spriteMask[i]].sp.rect.width, 0);
                            }
                            PositioncurveX.AddKey(i, decomposed1.Position.x);
                            PositioncurveY.AddKey(i, -decomposed1.Position.y);
                            PositioncurveZ.AddKey(i, decomposed1.Position.z);

                            if (ls.spriteName.Count == 0 || ls.spriteMask.Count == 0)
                            {
                                Quaternion value = Quaternion.Euler(-decomposed1.Rotation * Mathf.Rad2Deg);
                                RotationcurveX.AddKey(i, value.x);
                                RotationcurveY.AddKey(i, value.y);
                                RotationcurveZ.AddKey(i, value.z);
                                RotationcurveW.AddKey(i, value.w);

                                ScalingcurveX.AddKey(i, (float)decomposed1.Scaling.x);
                                ScalingcurveY.AddKey(i, (float)decomposed1.Scaling.y);
                                ScalingcurveZ.AddKey(i, (float)decomposed1.Scaling.z);
                            }
                            else
                            {
                                ScalingcurveX.AddKey(i, 1);
                                ScalingcurveY.AddKey(i, 1);
                                ScalingcurveZ.AddKey(i, 1);
                            }
                            ls.SpriteEnabled[i] = true;
                        }
                        else
                        {
                            ls.SpriteEnabled[i] = false;
                        }
                    } while ((!FrameLevelDict.ContainsValue(++i) || PlayTimelineflag) && i < MainTimeline.TimelineDuration);
                    endCounter = i - 1;
                    clip.SetCurve("", typeof(Transform), "localPosition.x", PositioncurveX);
                    clip.SetCurve("", typeof(Transform), "localPosition.y", PositioncurveY);
                    clip.SetCurve("", typeof(Transform), "localPosition.z", PositioncurveZ);
                    if (ls.spriteName.Count == 0 || ls.spriteMask.Count == 0)
                    {
                        clip.SetCurve("", typeof(Transform), "localRotation.x", RotationcurveX);
                        clip.SetCurve("", typeof(Transform), "localRotation.y", RotationcurveY);
                        clip.SetCurve("", typeof(Transform), "localRotation.z", RotationcurveZ);
                        clip.SetCurve("", typeof(Transform), "localRotation.w", RotationcurveW);
                    }
                    clip.SetCurve("", typeof(Transform), "localScale.x", ScalingcurveX);
                    clip.SetCurve("", typeof(Transform), "localScale.y", ScalingcurveY);
                    clip.SetCurve("", typeof(Transform), "localScale.z", ScalingcurveZ);
                    ls.anim.AddClip(clip, framelevel);
                    ls.anim.wrapMode = WrapMode.Loop;
                }

                foreach (var child in ls.children)
                {
                    CreateGameObject(child, ls.gobj, framelevel);
                }
            }
        }
        public void PlayGameObject(string name, string framelevel)
        {
            if (LayerStateObjDICT.ContainsKey(name))
            {
                LayerStateObject ls = LayerStateObjDICT[name];
                if (ls.anim != null)
                {
                    ls.anim.Play(framelevel);
                }

                foreach (var child in ls.children)
                {
                    PlayGameObject(child, framelevel);
                }
            }
        }
       
        //Update with Time
        public void UpdateTimeline(float time)
        {
            Debug.Log(time);
            foreach (var ls in LayerStateObjDICT)
            {
                if (ls.Value.spriteName.Count != 0)
                {
                    if (ls.Value.spriteName.ContainsKey(updateCounter) && SpriteDict.ContainsKey(ls.Value.spriteName[updateCounter]))
                    {
                        SpriteObject sobj = SpriteDict[ls.Value.spriteName[updateCounter]];
                        ls.Value.gobj.GetComponent<SpriteRenderer>().sprite = sobj.sp;
                        if (sobj.rotated)
                        {
                            ls.Value.gobj.GetComponent<SpriteRenderer>().flipX = true;
                            ls.Value.gobj.GetComponent<SpriteRenderer>().flipY = true;
                        }
                    }
                    ls.Value.gobj.GetComponent<SpriteRenderer>().enabled = ls.Value.SpriteEnabled[updateCounter];
                    if (ls.Value.sortingorder.ContainsKey(updateCounter))
                        ls.Value.gobj.GetComponent<SpriteRenderer>().sortingOrder = -ls.Value.sortingorder[updateCounter];
                        if (ls.Value.color.ContainsKey(updateCounter))
                        {
                            ls.Value.gobj.GetComponent<SpriteRenderer>().material.SetColor("_ColorMultiplier", ls.Value.color[updateCounter].colorMultiplier);
                            ls.Value.gobj.GetComponent<SpriteRenderer>().material.SetColor("_ColorOffset", ls.Value.color[updateCounter].colorOffset);
                        }
                }
#if UNITY_2017
                if (ls.Value.spriteMask.Count != 0)
                {
                    if (ls.Value.spriteMask.ContainsKey(updateCounter) && SpriteDict.ContainsKey(ls.Value.spriteMask[updateCounter]))
                    {
                        SpriteObject sobj = SpriteDict[ls.Value.spriteMask[updateCounter]];
                        ls.Value.gobj.GetComponent<SpriteMask>().sprite = sobj.sp;
                        ls.Value.gobj.GetComponent<SpriteMask>().enabled = ls.Value.SpriteEnabled[updateCounter];
                        ls.Value.gobj.GetComponent<SpriteMask>().isCustomRangeActive = true;
                        LayerStateObject temp = ls.Value.parent;
                        while (temp != null && temp.backLayerSortingOrder == 0)
                        {
                            temp = temp.parent;
                        }
                        //If there is only mask layer and no masked layer, all values of backLayerSortingOrder will be zero for all parents, hence temp will reach null
                        if (temp == null)
                            temp = ls.Value.parent;
                        ls.Value.gobj.GetComponent<SpriteMask>().backSortingOrder = temp.backLayerSortingOrder;
                        ls.Value.gobj.GetComponent<SpriteMask>().frontSortingOrder = temp.frontLayerSortingOrder;

                    }
                }
#endif
            }
            updateCounter = (updateCounter + 1) % MainTimeline.TimelineDuration;
            if (updateCounter > endCounter)
                updateCounter = startCounter;
        }
        public decomposedMat ComputeDecomposedMat(Matrix3 matirx3d)
        {
            Vector4 a = new Vector4(matirx3d.m00, matirx3d.m01, matirx3d.m02, matirx3d.m03);
            Vector4 b = new Vector4(matirx3d.m10, matirx3d.m11, matirx3d.m12, matirx3d.m13);
            Vector4 c = new Vector4(matirx3d.m20, matirx3d.m21, matirx3d.m22, matirx3d.m23);
            Vector4 d = new Vector4(matirx3d.m30, matirx3d.m31, matirx3d.m32, matirx3d.m33);

            Matrix4x4 matrix3D = new Matrix4x4();
            matrix3D.SetRow(0, a);
            matrix3D.SetRow(1, b);
            matrix3D.SetRow(2, c);
            matrix3D.SetRow(3, d);

            Matrix4x4 rotationMat = new Matrix4x4();

            Vector4 u = matrix3D.GetRow(0);
            float scaleX = Mathf.Sqrt(Vector4.Dot(u, u));
            u = u / scaleX;

            Vector4 v = matrix3D.GetRow(1) - (Vector4.Dot(matrix3D.GetRow(1), u) * u);
            float scaleY = Mathf.Sqrt(Vector4.Dot(v, v));
            v = v / scaleY;

            Vector4 w = matrix3D.GetRow(2) - (Vector4.Dot(matrix3D.GetRow(2), u)) * u - (Vector4.Dot(matrix3D.GetRow(2), v)) * v;
            float scaleZ = Mathf.Sqrt(Vector4.Dot(w, w));
            w = w / scaleZ;

            rotationMat.SetRow(0, u);
            rotationMat.SetRow(1, v);
            rotationMat.SetRow(2, w);

            float rotaDet = rotationMat.determinant;

            if (rotaDet == -1.0f)
            {
                scaleZ = -scaleZ;
                Vector4 x = -w;
                rotationMat.SetRow(2, x);
            }
            
            Vector3 Position = new Vector3(matirx3d.m30, matirx3d.m31, matirx3d.m32);
            Vector3 Rotation = ConvertToEulerAngles(rotationMat);
            Vector3 Scaling  = new Vector3(scaleX, scaleY, scaleZ);
            decomposedMat decomp = new decomposedMat();
            decomp.Position = Position;
            decomp.Scaling = Scaling;
            decomp.Rotation = Rotation;

            return decomp;
        }
        public Vector3 ConvertToEulerAngles(Matrix4x4 rotationMat)
        {
            Vector3 angles;
            angles.y = 0 - (Mathf.Asin(rotationMat.GetRow(0).z));

            float cosY = Mathf.Acos(angles.y);
            if (Mathf.Abs(cosY) >= 0.0f)
            {
                angles.x = Mathf.Atan2(rotationMat.GetRow(1).z, rotationMat.GetRow(2).z);
                angles.z = Mathf.Atan2(rotationMat.GetRow(0).y, rotationMat.GetRow(0).x);
            }
            else
            {
                angles.x = Mathf.Atan2(rotationMat.GetRow(1).x, rotationMat.GetRow(1).y);
                angles.z = 0;
            }
            return angles; // returns radian degree
        }
    }
}
