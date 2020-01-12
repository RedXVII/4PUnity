using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

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
[System.Serializable]
public class Sprite1
{
    public SubSprite SPRITE;
}
[System.Serializable]
public class Atlas
{
    public Sprite1[] SPRITES;
}
[System.Serializable]
public class Size
{
    public float w;
    public float h;
}
[System.Serializable]
public class meta
{
    public float framerate;
    public Size size;
}
[System.Serializable]
public class ParsedSpriteMap
{
    public Atlas ATLAS;
    public meta meta;
}

public class AnimateEditor : MonoBehaviour
{
    void Start()
    {
        SpriteSlicerAll();
    }

    public void SpriteSlicerSingle(int index)
    {
        ParsedSpriteMap spritemapjson;
        Texture2D texture = (Texture2D)Resources.Load<Texture2D>("spritemap" + index) as Texture2D;
        string path = AssetDatabase.GetAssetPath(texture);
        TextureImporter ti = AssetImporter.GetAtPath(path) as TextureImporter;
        ti.isReadable = true;
        ti.textureType = TextureImporterType.Sprite;

        List<SpriteMetaData> newData = new List<SpriteMetaData>();
        using (StreamReader r = new StreamReader(@"Assets/Resources/spritemap" + index + ".json"))
        {
            string json = r.ReadToEnd();
            spritemapjson = JsonUtility.FromJson<ParsedSpriteMap>(json);
        }

        foreach (var sprite in spritemapjson.ATLAS.SPRITES)
        {
            int SliceWidth = sprite.SPRITE.w;
            int SliceHeight = sprite.SPRITE.h;
            float pivotX = Mathf.Abs((SliceWidth - 0) / SliceWidth);
            float pivotY = Mathf.Abs((SliceHeight - 0) / SliceHeight);
            SpriteMetaData smd = new SpriteMetaData();
            smd.pivot = new Vector2((1 - pivotX), pivotY);
            smd.alignment = 9;
            smd.name = sprite.SPRITE.name;
            smd.rect = new Rect(sprite.SPRITE.x, spritemapjson.meta.size.h - sprite.SPRITE.y - SliceHeight, SliceWidth, SliceHeight);
            newData.Add(smd);
        }

        ti.spritesheet = newData.ToArray();
        ti.spriteImportMode = SpriteImportMode.Multiple;
        ti.filterMode = FilterMode.Point;
        ti.spritePixelsPerUnit = 1;
        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        texture.Apply(true);

    }
    public void SpriteSlicerAll()
    {
        DirectoryInfo root = new DirectoryInfo("Assets/Resources/");
        for (int index = 1; index <= SpriteCount(root); index++)
        {
            SpriteSlicerSingle(index);
        }
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
}

