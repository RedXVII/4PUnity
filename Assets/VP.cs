using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VP : MonoBehaviour
{
    VideoPlayer vp1, vp2;
    public static VideoPlayer curVp, prevVp;
    public static Dictionary<string, List<VideoPlayer>> vps = new Dictionary<string, List<VideoPlayer>>();
    public int curAnim = 0;
    public string curChar = "";
    int charIndex = 0;
    public static List<string> charList = new List<string>();
    public Sprite sprite;
    public Font font;
    public bool prepNext = false;

    public bool first = true;
    int poseState = 0;
    int charState = 0;
    bool queued = false;

    float lastUpdateMusicTime = 0.0f;
    bool shouldPlayNextVp = false;

    public System.Random randomGen = new System.Random();

    void Start()
	{
        
    }

    private bool m_isAxisInUse = false;

    void init()
    {
        GameObject camera = GameObject.Find("Main Camera");
        vp1 = camera.AddComponent<VideoPlayer>();
        vp1.waitForFirstFrame = true;
        vp1.isLooping = true;
        vp1.aspectRatio = VideoAspectRatio.FitVertically;
        vp1.audioOutputMode = VideoAudioOutputMode.Direct;
        vp1.SetDirectAudioVolume(0, 1);
        vp1.url = ModLoader.charMods[curChar][curAnim];
        vp1.prepareCompleted += Prepared;
        vp1.Prepare();
        
        

        vp2 = camera.AddComponent<VideoPlayer>();
        vp2.waitForFirstFrame = true;
        vp2.isLooping = true;
        vp2.aspectRatio = VideoAspectRatio.FitVertically;
        vp2.audioOutputMode = VideoAudioOutputMode.Direct;
        vp2.SetDirectAudioVolume(0, 1);
        vp2.url = ModLoader.charMods[curChar][curAnim];
        vp1.prepareCompleted += Prepared;
        vp2.Prepare();

        curVp = vp2;
        prevVp = vp1;

        GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
        playCurrent();
        createButtons();

        shouldPlayNextVp = false;
        lastUpdateMusicTime = 0.0f;

        first = false;

    }

    public void Prepared(VideoPlayer vp)
    {
        //vp.SetDirectAudioMute(0, true);
        //vp.Pause();
    }

    void Update()
    {
        if (!ModLoader.loaded)
        {
            return;
        }
        int newAnim = curAnim;
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                if (m_isAxisInUse == false)
                {
                    if (Input.GetAxis("Horizontal") > 0)
                    {
                        newAnim = (curAnim == ModLoader.charMods[curChar].Count - 1) ? 0 : (curAnim + 1);
                    }
                    else if (Input.GetAxis("Horizontal") < 0)
                    {
                        newAnim = (curAnim == 0) ? ModLoader.charMods[curChar].Count - 1 : (curAnim - 1);
                    }
                    m_isAxisInUse = true;
                    PoseSwitch(curChar, newAnim);
                }
            }
            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                m_isAxisInUse = false;
            }
        }

        if (first)
        {
            init();
        }

        float currentMusicTime = GameObject.Find("Main Camera").GetComponent<AudioSource>().time % 4;
        if (lastUpdateMusicTime > currentMusicTime)
        {
            shouldPlayNextVp = true;
        }
        lastUpdateMusicTime = currentMusicTime;

       
        if (shouldPlayNextVp)
        {
            if (curVp.isPrepared)
            {
                prepNext = true;
                //playCurrent();
                calculateNextAnimation();
                shouldPlayNextVp = false;
            }
        }

    }

    void FixedUpdate() {
        
    }

    void calculateNextAnimation()
    {
        int state = charState + poseState * 10;
        Debug.Log("Loop state: " + state);
        string nxtChar = curChar;
        int nxtAnim = curAnim;
        switch (state)
        {
            case 0:
                break;
            case 1:
                nxtChar = nextChar();
                break;
            case 2:
                nxtChar = randomChar();
                break;
            case 10:
                nxtAnim = nextAnim(nxtChar);
                break;
            case 20:
                nxtAnim = randomAnim(nxtChar);
                break;
            case 11:
                nxtChar = nextChar();
                nxtAnim = nextAnim(nxtChar);
                break;
            case 22:
                nxtChar = randomChar();
                nxtAnim = randomAnim(nxtChar);
                break;
            case 12:
                nxtChar = randomChar();
                nxtAnim = nextAnim(nxtChar);
                break;
            case 21:
                nxtChar = nextChar();
                nxtAnim = randomAnim(nxtChar);
                break;
        }
        if (state != 0)
        {
            prepareNext(nxtChar, nxtAnim);
        }
    }


    public void PoseSwitch(string ch, int anim)
    {
        Debug.Log("Old: " + curChar + ":" + curAnim);
        if (ch != curChar)
        {
            curChar = ch;
            createButtons();
            if (anim != curAnim)
                curAnim = (curAnim >= ModLoader.charMods[curChar].Count) ? 0 : anim;
            playCurrent();
        }
        else
        {
            if (anim != curAnim)
            {
                curAnim = (curAnim >= ModLoader.charMods[curChar].Count) ? 0 : anim;
                playCurrent();
            }
        }
        Debug.Log("New: " + curChar + ":" + curAnim);
        
    }

    void switchVp()
    {
        if(curVp == vp1)
        {
            curVp = vp2;
            prevVp = vp1;
        } else
        {
            curVp = vp1;
            prevVp = vp2;
        }
    }

    void playCurrent()
    {
        Debug.Log("PLAY CURRENT");
        if (!prepNext)
        {
            switchVp();
            curVp.url = ModLoader.charMods[curChar][curAnim];
            curVp.Prepare();
            shouldPlayNextVp = true;
            Debug.Log("Play Current, prepnext = false");
        }
        else
        {
            Debug.Log("pausing something, anything");
            curVp.Play();
            double time = GameObject.Find("Main Camera").GetComponent<AudioSource>().time % 4.0;
            curVp.time = time;
            curVp.targetCameraAlpha = 1f;
            prevVp.Pause();
            prevVp.targetCameraAlpha = 0f;
        }

    }

    void prepareNext(string ch, int anim)
    {
        switchVp();
        curChar = ch;
        curAnim = anim;
        curVp.url = ModLoader.charMods[curChar][curAnim];
        curVp.Prepare();
        curVp.SetDirectAudioMute(0, false);
        prevVp.SetDirectAudioMute(0, true);
        prepNext = false;
    }

    public void PoseReset()
    {
        PoseSwitch(curChar, curAnim);
        poseState = 0;
        charState = 0;
    }

    public void TogglePose()
    {
        poseState = (poseState == 2) ? 0 : poseState + 1;
        queued = charState > 0 || poseState > 0;
        switch (poseState)
        {
            case 0:
                GameObject.Find("LoopPose").GetComponent<Text>().text = "Single";
                break;
            case 1:
                GameObject.Find("LoopPose").GetComponent<Text>().text = "Loop";
                break;
            case 2:
                GameObject.Find("LoopPose").GetComponent<Text>().text = "Random";
                break;
        }
    }

    public void ToggleChar()
    {
        charState = (charState == 2) ? 0 : charState + 1;
        queued = charState > 0 || poseState > 0;
        switch (charState)
        {
            case 0:
                GameObject.Find("LoopChar").GetComponent<Text>().text = "Single";
                break;
            case 1:
                GameObject.Find("LoopChar").GetComponent<Text>().text = "Loop";
                break;
            case 2:
                GameObject.Find("LoopChar").GetComponent<Text>().text = "Random";
                break;
        }
    }

    int nextAnim(string nxtChar)
    {
        return (curAnim + 1 >= ModLoader.charMods[nxtChar].Count) ? 0 : (curAnim + 1);
    }

    int randomAnim(string nxtChar)
    {
        return randomGen.Next(ModLoader.charMods[nxtChar].Count);
    }

    string nextChar()
    {
        charIndex = (charIndex == charList.Count - 1) ? 0 : charIndex + 1;
        return charList[charIndex];
    }

    string randomChar()
    {
        charIndex = randomGen.Next(charList.Count);
        return charList[charIndex];
    }


    public void createButtons()
    {
        DefaultControls.Resources uiResources = new DefaultControls.Resources();
        GameObject canvas = GameObject.Find("PoseContent");
        for (int j = 0; j < canvas.transform.childCount; j++)
        {
            Destroy(canvas.transform.GetChild(j).gameObject);
        }
        int i = 0;
        canvas.GetComponent<RectTransform>().offsetMin = new Vector2(canvas.GetComponent<RectTransform>().offsetMin.x, 880);
        foreach (string anim in ModLoader.charMods[curChar])
        {
            string anim2 = anim.Replace('\\', '/');
            GameObject animButton = DefaultControls.CreateButton(uiResources);
            animButton.name = anim2.Substring(anim2.LastIndexOf("/") + 1);
            animButton.transform.SetParent(canvas.transform);
            canvas.GetComponent<RectTransform>().offsetMin = new Vector2(canvas.GetComponent<RectTransform>().offsetMin.x, canvas.GetComponent<RectTransform>().offsetMin.y - 200);
            animButton.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1);
            animButton.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1);
            animButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -100 - i * 200, 0);
            animButton.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);
            int k = i;
            animButton.GetComponent<Button>().onClick.AddListener(() => {
                PoseSwitch(curChar, k);
                shouldPlayNextVp = true;
            });
            i += 1;
            animButton.GetComponentInChildren<Text>().text = i.ToString();
            animButton.GetComponentInChildren<Text>().fontSize = 72;
            animButton.GetComponentInChildren<Text>().font = font;
            animButton.GetComponentInChildren<Text>().color = Color.white;
            animButton.GetComponent<Image>().sprite = sprite;
            animButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
    }

    public void SetVolume(float volume)
    {
        vp1.SetDirectAudioVolume(0, volume);
        vp2.SetDirectAudioVolume(0, volume);
    }
}