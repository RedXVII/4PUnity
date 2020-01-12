using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    bool charPanelVisible = true;
    public GameObject charDawer;

    bool posePanelVisible = true;
    public GameObject poseDawer;

    bool musicPanelVisible = true;
    public GameObject musicDawer;

    int curTrack = 0;

    public void ToggleCharPanel()
    {
        RectTransform rtf = charDawer.GetComponent<RectTransform>();
        if (charPanelVisible)
        {
            rtf.offsetMin = new Vector2(rtf.offsetMin.x - 260, rtf.offsetMin.y);
            rtf.offsetMax = new Vector2(rtf.offsetMax.x - 260, rtf.offsetMax.y);
        } else
        {
            rtf.offsetMin = new Vector2(rtf.offsetMin.x + 260, rtf.offsetMin.y);
            rtf.offsetMax = new Vector2(rtf.offsetMax.x + 260, rtf.offsetMax.y);
        }
        charPanelVisible = !charPanelVisible;
    }

    public void TogglePosePanel()
    {
        RectTransform rtf = poseDawer.GetComponent<RectTransform>();
        if (posePanelVisible)
        {
            rtf.offsetMin = new Vector2(rtf.offsetMin.x + 260, rtf.offsetMin.y);
            rtf.offsetMax = new Vector2(rtf.offsetMax.x + 260, rtf.offsetMax.y);
        }
        else
        {
            rtf.offsetMin = new Vector2(rtf.offsetMin.x - 260, rtf.offsetMin.y);
            rtf.offsetMax = new Vector2(rtf.offsetMax.x - 260, rtf.offsetMax.y);
        }
        posePanelVisible = !posePanelVisible;
    }

    public void ToggleMusicPanel()
    {
        RectTransform rtf = musicDawer.GetComponent<RectTransform>();
        if (musicPanelVisible)
        {
            rtf.offsetMin = new Vector2(rtf.offsetMin.x, rtf.offsetMin.y - 80);
            rtf.offsetMax = new Vector2(rtf.offsetMax.x, rtf.offsetMax.y - 80);
        }
        else
        {
            rtf.offsetMin = new Vector2(rtf.offsetMin.x, rtf.offsetMin.y + 80);
            rtf.offsetMax = new Vector2(rtf.offsetMax.x, rtf.offsetMax.y + 80);
        }
        musicPanelVisible = !musicPanelVisible;
    }

    public void MusicChange(int track)
    {
        curTrack = track;
        AudioSource source = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        float time = GameObject.Find("Main Camera").GetComponent<AudioSource>().time;
        source.clip = ModLoader.music[track];
        source.time = (time > source.clip.length) ? 0 : time;
        source.Play();
    }

    public void MusicNext(int dir)
    {
        curTrack = (curTrack + dir == ModLoader.music.Count) ? 0 : (curTrack + dir < 0) ? ModLoader.music.Count - 1 : curTrack + dir;
        //MusicChange(curTrack);
        GameObject.Find("MusicDropdown").GetComponent<Dropdown>().value = curTrack;
        //GameObject.Find("EventSystem").GetComponent<VP>().PoseReset();
    }
}
