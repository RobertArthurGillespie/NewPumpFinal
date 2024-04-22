using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI simText;
    public Slider textSlider;
    public bool menuIsOpen = false;
    public bool soundOff = false;
    public bool isPaused = false;
    public Sprite soundOnButtonImage;
    public Sprite soundOffButtonImage;
    public GameObject soundButton;
    public AudioClip openSoundClip;
    public AudioClip closeSoundClip;
    Vector2 EMin;
    Vector2 EMax;
    Vector2 MMin;
    Vector2 MMax;
    Vector2 MinDiff;
    Vector2 MaxDiff;
    // Start is called before the first frame update
    void Start()
    {
        //0.3499
        //Expanded:
        //Min
        EMin = new Vector2(0.287f, 0.234405026f);
        //Max
        EMax = new Vector2(0.69599998f, 0.349900007f);
        //Minimized:
        MMin = new Vector2(0.287f, 0.0656127259f);
        MMax = new Vector2(0.69600004f, 0.181129113f);
        //Differences
        MaxDiff = MMax - EMax;
        MinDiff = MMin - EMin;
        Debug.Log("MinDiff is: " + MinDiff.x + "," + MinDiff.y);// 0,-0.1687923
        Debug.Log("MaxDiff is: "+MaxDiff.x+","+ MaxDiff.y);//.960464E-08,-0.1687709
        MMin -= EMin;
        Debug.Log("modified MinDiff is: "+MMin.x+","+ MMin.y);
        Debug.Log("expanding menu");
        ExpandMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CollapseMenu();
        }
    }

    public void UpdateTextSize()
    {
        simText.fontSize = textSlider.value;
    }

    public IEnumerator OpenMainMenuCoroutine()
    {
        GameObject.Find("TitratorScene").GetComponent<AudioSource>().clip = openSoundClip;
        GameObject.Find("TitratorScene").GetComponent<AudioSource>().Play();

        DOTween.To(() => GameObject.Find("MainMenuPanel").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("MainMenuPanel").GetComponent<RectTransform>().anchorMax = dest, new Vector2(0.661000013f, 0.810225546f), 1);
        yield return new WaitForSeconds(1f);
        GameObject.Find("HomeImage").GetComponent<Image>().enabled = true;
        GameObject.Find("SettingsImage").GetComponent<Image>().enabled = true;
        GameObject.Find("HomeText").GetComponent<TextMeshProUGUI>().enabled = true;
        GameObject.Find("SettingsText").GetComponent<TextMeshProUGUI>().enabled = true;
        DOTween.To(() => GameObject.Find("HomeButton").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("HomeButton").GetComponent<RectTransform>().anchorMax = dest, new Vector2(0.61644882f, 0.764741898f), 0.5f);
        DOTween.To(() => GameObject.Find("SettingsButton").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("SettingsButton").GetComponent<RectTransform>().anchorMax = dest, new Vector2(0.61644882f, 0.5633873f), 0.5f);
        
        //Vector2(0.378842503,0.649000049)
    }

    public IEnumerator CloseMainMenuCoroutine()
    {
        GameObject.Find("TitratorScene").GetComponent<AudioSource>().clip = closeSoundClip;
        GameObject.Find("TitratorScene").GetComponent<AudioSource>().Play();
        DOTween.To(() => GameObject.Find("MainMenuPanel").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("MainMenuPanel").GetComponent<RectTransform>().anchorMax = dest, new Vector2(0.329000026f, 0.810225546f), 1);
        yield return new WaitForSeconds(1f);
        GameObject.Find("HomeImage").GetComponent<Image>().enabled = false;
        GameObject.Find("SettingsImage").GetComponent<Image>().enabled = false;
        GameObject.Find("HomeText").GetComponent<TextMeshProUGUI>().enabled = false;
        GameObject.Find("SettingsText").GetComponent<TextMeshProUGUI>().enabled = false;
        DOTween.To(() => GameObject.Find("HomeButton").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("HomeButton").GetComponent<RectTransform>().anchorMax = dest, new Vector2(0.378842503f, 0.764741898f), 0.5f);
        DOTween.To(() => GameObject.Find("SettingsButton").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("SettingsButton").GetComponent<RectTransform>().anchorMax = dest, new Vector2(0.378842503f, 0.563387275f), 0.5f);
        
        //Vector2(0.378842503,0.649000049)
    }
    public void OpenMainMenu()
    {
        menuIsOpen = true;
        StartCoroutine(OpenMainMenuCoroutine());
    }

    public void CloseMainMenu()
    {
        menuIsOpen = false;
        StartCoroutine(CloseMainMenuCoroutine());
    }

    public void ToggleMainMenu()
    {
        if(menuIsOpen)
        {
            CloseMainMenu();
        }
        else
        {
            OpenMainMenu();
        }
    }

    public void ToggleSound()
    {
        if (soundOff)
        {
            GameObject.Find("EventSystem").GetComponent<AudioSource>().mute = false;
            soundOff = false;
            soundButton.GetComponent<Image>().sprite = soundOnButtonImage;
        }
        else
        {
            soundOff = true;
            GameObject.Find("EventSystem").GetComponent<AudioSource>().mute = true;
            soundButton.GetComponent<Image>().sprite = soundOffButtonImage;
        }
    }

    public void PauseAudio()
    {
        if (isPaused)
        {
            isPaused = false;
            GameObject.Find("EventSystem").GetComponent<AudioSource>().Pause();
        }
        
        
    }

    public void PlayAudio()
    {
        if (!isPaused)
        {
            
                GameObject.Find("EventSystem").GetComponent<AudioSource>().UnPause();
            isPaused = true;
            
        }
    }

    public void CollapseMenu()
    {
        DOTween.To(() => GameObject.Find("BottomPanel").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("BottomPanel").GetComponent<RectTransform>().anchorMax = dest, new Vector2(1f, 0.0897745565f), 1);
        DOTween.To(() => GameObject.Find("TextFrame").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("TextFrame").GetComponent<RectTransform>().anchorMax = dest, new Vector2(0.989000022f, 0.0936454684f), 1);
        DOTween.To(() => GameObject.Find("SimScriptText").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("SimScriptText").GetComponent<RectTransform>().anchorMax = dest, new Vector2(0.975500643f, 0.0693545416f), 1);
        DOTween.To(() => GameObject.Find("ToolPanel").GetComponent<RectTransform>().anchorMin, dest => GameObject.Find("ToolPanel").GetComponent<RectTransform>().anchorMin = dest, new Vector2(0.287f, 0.0656127259f), 1);
        DOTween.To(() => GameObject.Find("ToolPanel").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("ToolPanel").GetComponent<RectTransform>().anchorMax = dest, new Vector2(0.69600004f, 0.181129113f), 1);

        DOTween.To(() => GameObject.Find("ExpandSubmenuButton").GetComponent<RectTransform>().anchorMin, dest => GameObject.Find("ExpandSubmenuButton").GetComponent<RectTransform>().anchorMin = dest, new Vector2(0.342369974f, 0.112225451f), 1);
        DOTween.To(() => GameObject.Find("ExpandSubmenuButton").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("ExpandSubmenuButton").GetComponent<RectTransform>().anchorMax = dest, new Vector2(0.378842503f, 0.168129101f), 1);
        DOTween.To(() => GameObject.Find("SoundOnButton").GetComponent<RectTransform>().anchorMin, dest => GameObject.Find("SoundOnButton").GetComponent<RectTransform>().anchorMin = dest, new Vector2(0.578000009f, 0.112225451f), 1);
        DOTween.To(() => GameObject.Find("SoundOnButton").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("SoundOnButton").GetComponent<RectTransform>().anchorMax = dest, new Vector2(0.617000043f, 0.168129101f), 1);
        DOTween.To(() => GameObject.Find("BackButton").GetComponent<RectTransform>().anchorMin, dest => GameObject.Find("BackButton").GetComponent<RectTransform>().anchorMin = dest, new Vector2(0.389685035f, 0.112225451f), 1);
        DOTween.To(() => GameObject.Find("BackButton").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("BackButton").GetComponent<RectTransform>().anchorMax = dest, new Vector2(0.424000025f, 0.168129101f), 1);
        DOTween.To(() => GameObject.Find("ForwardButton").GetComponent<RectTransform>().anchorMin, dest => GameObject.Find("ForwardButton").GetComponent<RectTransform>().anchorMin = dest, new Vector2(0.526000023f, 0.112225451f), 1);
        DOTween.To(() => GameObject.Find("ForwardButton").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("ForwardButton").GetComponent<RectTransform>().anchorMax = dest, new Vector2(0.564999998f, 0.168129101f), 1);
        //DOTween.To(() => GameObject.Find("TextSlider").GetComponent<RectTransform>().anchorMin, dest => GameObject.Find("TextSlider").GetComponent<RectTransform>().anchorMin = dest, new Vector2(0.671000004f, 0.1121291f), 1);
        //DOTween.To(() => GameObject.Find("TextSlider").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("TextSlider").GetComponent<RectTransform>().anchorMax = dest, new Vector2(0.791000009f, 0.152999997f), 1);
        //DOTween.To(() => GameObject.Find("MenuButton").GetComponent<RectTransform>().anchorMin, dest => GameObject.Find("MenuButton").GetComponent<RectTransform>().anchorMin = dest, new Vector2(0.0610000044f, 0.103000008f), 1);
        //DOTween.To(() => GameObject.Find("MenuButton").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("MenuButton").GetComponent<RectTransform>().anchorMax = dest, new Vector2(0.0920000076f, 0.152999997f), 1);
        DOTween.To(() => GameObject.Find("PlayButton").GetComponent<RectTransform>().anchorMin, dest => GameObject.Find("PlayButton").GetComponent<RectTransform>().anchorMin = dest, new Vector2(0.474750012f, 0.0948282704f), 1);
        DOTween.To(() => GameObject.Find("PlayButton").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("PlayButton").GetComponent<RectTransform>().anchorMax = dest, new Vector2(0.520250082f, 0.170300826f), 1);
        DOTween.To(() => GameObject.Find("PauseButton").GetComponent<RectTransform>().anchorMin, dest => GameObject.Find("PauseButton").GetComponent<RectTransform>().anchorMin = dest, new Vector2(0.430551261f, 0.0991290957f), 1);
        DOTween.To(() => GameObject.Find("PauseButton").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("PauseButton").GetComponent<RectTransform>().anchorMax = dest, new Vector2(0.472250015f, 0.168129101f), 1);
        DOTween.To(() => GameObject.Find("HomeButton").GetComponent<RectTransform>().anchorMin, dest => GameObject.Find("HomeButton").GetComponent<RectTransform>().anchorMin = dest, new Vector2(0.296000004f, 0.112225451f), 1);
        DOTween.To(() => GameObject.Find("HomeButton").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("HomeButton").GetComponent<RectTransform>().anchorMax = dest, new Vector2(0.335000008f, 0.168129101f), 1);
        DOTween.To(() => GameObject.Find("TextSizeButton").GetComponent<RectTransform>().anchorMin, dest => GameObject.Find("TextSizeButton").GetComponent<RectTransform>().anchorMin = dest, new Vector2(0.631472409f, 0.112225451f), 1);
        DOTween.To(() => GameObject.Find("TextSizeButton").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("TextSizeButton").GetComponent<RectTransform>().anchorMax = dest, new Vector2(0.66900003f, 0.168129101f), 1);


        //TextSizeButton

        //Vector2(0.975500643,0.08622545)
    }

    public void ExpandMenu()
    {
        //Min: Vector2(0.287,0.234405026)
        //Max: Vector2(0.69599998,0.349900007)
        DOTween.To(() => GameObject.Find("BottomPanel").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("BottomPanel").GetComponent<RectTransform>().anchorMax = dest, new Vector2(1, 0.26642552f), 1);
        DOTween.To(() => GameObject.Find("SimScriptText").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("SimScriptText").GetComponent<RectTransform>().anchorMax = dest, new Vector2(0.975500643f, 0.25339514f), 1);
        DOTween.To(() => GameObject.Find("ToolPanel").GetComponent<RectTransform>().anchorMin, dest => GameObject.Find("ToolPanel").GetComponent<RectTransform>().anchorMin = dest, new Vector2(0.287f, 0.234405026f), 1);
        DOTween.To(() => GameObject.Find("ToolPanel").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("ToolPanel").GetComponent<RectTransform>().anchorMax = dest, new Vector2(0.69599998f, 0.349900007f), 1);

        Vector2 SubmenuMinValue = new Vector2(0.342369974f, 0.112225451f);
        Debug.Log("MinDiff is: " + MinDiff.x + "," + MinDiff.y);
        float SubmenuMinValuex = SubmenuMinValue.x;
        float SubmenuMinValuey = SubmenuMinValue.y;
        SubmenuMinValuex -= MinDiff.x;
        SubmenuMinValuey -= MinDiff.y;
        Debug.Log("SubmenuMinValuex: " + SubmenuMinValuex);
        Debug.Log("SubmenuMinValuey: " + SubmenuMinValuey);
        Vector2 SubmenuMaxValue = new Vector2(0.378842503f, 0.168129101f);
        SubmenuMaxValue.x -= MaxDiff.x;
        SubmenuMaxValue.y -= MaxDiff.y;
        SubmenuMaxValue -= MaxDiff;
        Debug.Log("SubmenuMinValue is: " + SubmenuMinValue.x + "," + SubmenuMinValue.y);
        DOTween.To(() => GameObject.Find("ExpandSubmenuButton").GetComponent<RectTransform>().anchorMin, dest => GameObject.Find("ExpandSubmenuButton").GetComponent<RectTransform>().anchorMin = dest, (new Vector2(0.342369974f, 0.112225451f)-MinDiff), 1);
        DOTween.To(() => GameObject.Find("ExpandSubmenuButton").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("ExpandSubmenuButton").GetComponent<RectTransform>().anchorMax = dest, (new Vector2(0.378842503f, 0.168129101f)-MaxDiff), 1);
        DOTween.To(() => GameObject.Find("SoundOnButton").GetComponent<RectTransform>().anchorMin, dest => GameObject.Find("SoundOnButton").GetComponent<RectTransform>().anchorMin = dest, (new Vector2(0.578000009f, 0.112225451f)-MinDiff), 1);
        DOTween.To(() => GameObject.Find("SoundOnButton").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("SoundOnButton").GetComponent<RectTransform>().anchorMax = dest, (new Vector2(0.617000043f, 0.168129101f)-MaxDiff), 1);
        DOTween.To(() => GameObject.Find("BackButton").GetComponent<RectTransform>().anchorMin, dest => GameObject.Find("BackButton").GetComponent<RectTransform>().anchorMin = dest, (new Vector2(0.389685035f, 0.112225451f) - MinDiff), 1);
        DOTween.To(() => GameObject.Find("BackButton").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("BackButton").GetComponent<RectTransform>().anchorMax = dest, (new Vector2(0.424000025f, 0.168129101f) - MaxDiff), 1);
        DOTween.To(() => GameObject.Find("ForwardButton").GetComponent<RectTransform>().anchorMin, dest => GameObject.Find("ForwardButton").GetComponent<RectTransform>().anchorMin = dest, (new Vector2(0.526000023f, 0.112225451f) - MinDiff), 1);
        DOTween.To(() => GameObject.Find("ForwardButton").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("ForwardButton").GetComponent<RectTransform>().anchorMax = dest, (new Vector2(0.564999998f, 0.168129101f) - MaxDiff), 1);
        //DOTween.To(() => GameObject.Find("TextSlider").GetComponent<RectTransform>().anchorMin, dest => GameObject.Find("TextSlider").GetComponent<RectTransform>().anchorMin = dest, new Vector2(0.671000004f, 0.1121291f), 1);
        //DOTween.To(() => GameObject.Find("TextSlider").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("TextSlider").GetComponent<RectTransform>().anchorMax = dest, new Vector2(0.791000009f, 0.152999997f), 1);
        //DOTween.To(() => GameObject.Find("MenuButton").GetComponent<RectTransform>().anchorMin, dest => GameObject.Find("MenuButton").GetComponent<RectTransform>().anchorMin = dest, new Vector2(0.0610000044f, 0.103000008f), 1);
        //DOTween.To(() => GameObject.Find("MenuButton").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("MenuButton").GetComponent<RectTransform>().anchorMax = dest, new Vector2(0.0920000076f, 0.152999997f), 1);
        DOTween.To(() => GameObject.Find("PlayButton").GetComponent<RectTransform>().anchorMin, dest => GameObject.Find("PlayButton").GetComponent<RectTransform>().anchorMin = dest, (new Vector2(0.474750012f, 0.0948282704f)-MinDiff), 1);
        DOTween.To(() => GameObject.Find("PlayButton").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("PlayButton").GetComponent<RectTransform>().anchorMax = dest, (new Vector2(0.520250082f, 0.170300826f)-MaxDiff), 1);
        DOTween.To(() => GameObject.Find("PauseButton").GetComponent<RectTransform>().anchorMin, dest => GameObject.Find("PauseButton").GetComponent<RectTransform>().anchorMin = dest, (new Vector2(0.430551261f, 0.0991290957f) - MinDiff), 1);
        DOTween.To(() => GameObject.Find("PauseButton").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("PauseButton").GetComponent<RectTransform>().anchorMax = dest, (new Vector2(0.472250015f, 0.168129101f) - MaxDiff), 1);
        DOTween.To(() => GameObject.Find("HomeButton").GetComponent<RectTransform>().anchorMin, dest => GameObject.Find("HomeButton").GetComponent<RectTransform>().anchorMin = dest, (new Vector2(0.296000004f, 0.112225451f) - MinDiff), 1);
        DOTween.To(() => GameObject.Find("HomeButton").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("HomeButton").GetComponent<RectTransform>().anchorMax = dest, (new Vector2(0.335000008f, 0.168129101f) - MaxDiff), 1);
        DOTween.To(() => GameObject.Find("TextSizeButton").GetComponent<RectTransform>().anchorMin, dest => GameObject.Find("TextSizeButton").GetComponent<RectTransform>().anchorMin = dest, (new Vector2(0.631472409f, 0.112225451f) - MinDiff), 1);
        DOTween.To(() => GameObject.Find("TextSizeButton").GetComponent<RectTransform>().anchorMax, dest => GameObject.Find("TextSizeButton").GetComponent<RectTransform>().anchorMax = dest, (new Vector2(0.66900003f, 0.168129101f) - MaxDiff), 1);

    }
}
