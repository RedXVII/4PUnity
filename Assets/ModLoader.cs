using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;

public class ModLoader : MonoBehaviour
{
    //public static Dictionary<string, List<string>> charMods = new Dictionary<string, List<string>>();
    public static Dictionary<string, List<string>> charMods = new Dictionary<string, List<string>>();
    public static List<AudioClip> music = new List<AudioClip>();
    public static bool loaded = false;
    public static int totalMods = 0;
    public static int loadedMods = 0;
    static string logFile;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        string[] charDirs;
        string modsDir;

        if (Application.platform != RuntimePlatform.Android)
        {
            modsDir = Application.dataPath + "/../mods/";
            charDirs = Directory.GetDirectories(modsDir, "CHAR_*");
            if (!Directory.Exists(modsDir + "/../logs"))
            {
                Directory.CreateDirectory(modsDir + "/../logs");
            }
            logFile = modsDir + "../logs/" + DateTime.Now.ToString("ddMMyy_HHmmss") + ".txt";
        } else
        {
            if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite) && !Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
            {
                // Ask for permission or proceed without the functionality enabled.
                Permission.RequestUserPermission(Permission.ExternalStorageRead);
                Permission.RequestUserPermission(Permission.ExternalStorageWrite);
            }
                        
            if(!Directory.Exists(GetAndroidInternalFilesDir() + "/.4PUnity"))
            {
                Directory.CreateDirectory(GetAndroidInternalFilesDir() + "/.4PUnity");
                
                File.Create(GetAndroidInternalFilesDir() + "/.4PUnity/.nomedia");
            }
            if (!Directory.Exists(GetAndroidInternalFilesDir() + "/.4PUnity/music"))
                Directory.CreateDirectory(GetAndroidInternalFilesDir() + "/.4PUnity/music");
            if (!Directory.Exists(GetAndroidInternalFilesDir() + "/.4PUnity/logs"))
                Directory.CreateDirectory(GetAndroidInternalFilesDir() + "/.4PUnity/logs");
            modsDir = GetAndroidInternalFilesDir() + "/.4PUnity/";
            charDirs = Directory.GetDirectories(modsDir, "CHAR_*");
            logFile = modsDir + "logs/" + DateTime.Now.ToString("ddMMyy_HHmmss") + ".txt";
        }

        List<string> musicFiles = new List<string>(Directory.GetFiles(modsDir + "music", "*.ogg"));
        musicFiles.AddRange(Directory.GetFiles(modsDir + "music", "*.mp3"));

        Dropdown mdd = GameObject.Find("MusicDropdown").GetComponent<Dropdown>();
        List<string> options = new List<string>();
        foreach(string track in musicFiles)
        {
            string track2 = track.Replace('\\', '/');
            UnityWebRequest rq = UnityWebRequestMultimedia.GetAudioClip("file://" + track2, AudioType.OGGVORBIS);
            rq.SendWebRequest();
            while (rq.downloadProgress < 1) ;
            yield return rq;
            string trackName = track2.Substring(track2.LastIndexOf("/") + 1);
            trackName = trackName.Substring(0, trackName.Length - 4);
            music.Add(((DownloadHandlerAudioClip)rq.downloadHandler).audioClip);
            options.Add(trackName);
        }
        mdd.ClearOptions();
        mdd.AddOptions(options);
        mdd.onValueChanged.AddListener((int a) => { GameObject.Find("EventSystem").GetComponent<UI>().MusicChange(a); });

        GameObject cam = GameObject.Find("Main Camera");
        cam.GetComponent<AudioSource>().clip = music[0];
        cam.GetComponent<AudioSource>().loop = true;

        GameObject canvas = GameObject.Find("CharContent");
        DefaultControls.Resources uiResources = new DefaultControls.Resources();
        var i = 0;

        VP vpClass = GameObject.Find("EventSystem").GetComponent<VP>();

        foreach (string ch in charDirs)
        {
            string charName = ch.Replace('\\','/').Substring(ch.LastIndexOf('_') + 1);
            GameObject charButton = DefaultControls.CreateButton(uiResources);
            charButton.name = charName;
            charButton.transform.SetParent(canvas.transform);
            canvas.GetComponent<RectTransform>().offsetMin = new Vector2(canvas.GetComponent<RectTransform>().offsetMin.x, canvas.GetComponent<RectTransform>().offsetMin.y + 200);
            charButton.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0);
            charButton.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0);
            charButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -100 - i * 200, 0);
            charButton.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);
            Destroy(charButton.transform.GetChild(0).gameObject);
            Debug.Log(Directory.GetFiles(ch, charName + ".png")[0]);
            charButton.GetComponent<Image>().sprite = LoadNewSprite(Directory.GetFiles(ch, charName + ".png")[0]);
            charButton.GetComponent<Button>().onClick.AddListener(() => { vpClass.PoseSwitch(charName, vpClass.curAnim); });
            charButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            i++;
            List<string> files = new List<string>(Directory.GetFiles(ch, "*.mp4"));
            files.Sort();
            charMods.Add(charName, files);
        }

        GameObject camera = GameObject.Find("Main Camera");

        bool firstVp = true;
        foreach (KeyValuePair<string, List<string>> charAnims in ModLoader.charMods)
        {
            List<VideoPlayer> players = new List<VideoPlayer>();
            foreach (string file in charAnims.Value)
            {
                //VideoPlayer vp = camera.AddComponent<VideoPlayer>();
                //vp.waitForFirstFrame = true;
                //vp.isLooping = true;
                //vp.aspectRatio = VideoAspectRatio.FitVertically;
                //vp.loopPointReached += vpClass.Looped;
                //vp.prepareCompleted += vpClass.Prepared;
                //vp.audioOutputMode = VideoAudioOutputMode.Direct;
                //vp.SetDirectAudioVolume(0, 1);
                //vp.url = file;
                //vp.Prepare();
                //while (!vp.isPrepared) yield return null;
                //loadedMods++;
                //currentLoaded -= fraction;
                //loadingBar.offsetMax = new Vector2(-currentLoaded, 10f);
                //loadingText.text = loadedMods + "/" + totalMods;
                //players.Add(vp);
                if (firstVp)
                {
                    firstVp = false;
                    vpClass.curChar = charAnims.Key;
                    vpClass.curAnim = 0;
                }
            }
            VP.vps.Add(charAnims.Key, players);
            VP.charList.Add(charAnims.Key);
            Debug.Log(VP.vps);
        }
        
        Application.logMessageReceived += LogHandler;
        loaded = true;
    }

    static void LogHandler(string logString, string stackTrace, LogType type)
    {
        File.AppendAllText(logFile, 
            logString + "\r\n" + 
            stackTrace + 
            "------------------------------------------------------------\r\n");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Sprite LoadNewSprite(string FilePath, float PixelsPerUnit = 100.0f)
    {

        // Load a PNG or JPG image from disk to a Texture2D, assign this texture to a new sprite and return its reference

        Sprite NewSprite;
        Texture2D SpriteTexture = LoadTexture(FilePath);
        NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), PixelsPerUnit);

        return NewSprite;
    }

    public static Texture2D LoadTexture(string FilePath)
    {

        // Load a PNG or JPG file from disk to a Texture2D
        // Returns null if load fails

        Texture2D Tex2D;
        byte[] FileData;

        if (File.Exists(FilePath))
        {
            FileData = File.ReadAllBytes(FilePath);
            Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
            if (Tex2D.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
                return Tex2D;                 // If data = readable -> return texture
        }
        return null;                     // Return null if load failed
    }

    public static string GetAndroidInternalFilesDir()
    {
        string[] potentialDirectories = new string[]
        {
        "/mnt/sdcard",
        "/sdcard",
        "/storage/sdcard0",
        "/storage/sdcard1",
        "/storage/emulated/0"
        };

        if (Application.platform == RuntimePlatform.Android)
        {
            for (int i = 0; i < potentialDirectories.Length; i++)
            {
                if (Directory.Exists(potentialDirectories[i]))
                {
                    return potentialDirectories[i];
                }
            }
        }
        return "";
    }    
}
