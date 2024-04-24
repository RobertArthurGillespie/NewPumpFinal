using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class TitratorFinalManager : MonoBehaviour
{
    
    public GameObject TimelineObject;
    public GameObject EpisodeObject;
    PlayableDirector titrationDirector;
    public List<TimelineEpisode> TitratorEpisodes = new List<TimelineEpisode>();
    public AudioSource audioSource;
    public int TitratorEpisodeIndex = 0;
    public AudioClip introClip;
    public Material RegularMaterial;
    public Material TransMaterial;
    public Material HighlightMaterial;
    public bool introAudioFinished = false;
    public bool hasSkinnedMeshRenderer = false;
    public UIManager uiManager;
    public bool rewindChapter = false;
    public bool ffChapter = false;
    public bool ccMenuOpen = false;
    public bool ccMenuIsMoving = false;
    public bool simHasBegun = false;
    TimelineEpisode currentTitratorEpisode;
    TimelineEpisode previousTitratorEpisode;
    TimelineEpisode nextTitratorEpisode;

    // Start is called before the first frame update
    void Start()
    {
        foreach(TimelineEpisode episode in TitratorEpisodes)
        {
            episode.hasPassed = false;
        }
        titrationDirector = TimelineObject.GetComponent<PlayableDirector>();
        titrationDirector.time = titrationDirector.time;
        titrationDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
        /*PlayNextEpisode();
        //uiManager.ExpandMenu();
        StartCoroutine(WaitThenCollapseMenu());
        Debug.Log(titrationDirector.playableAsset + "is playable asset");
        Debug.Log(titrationDirector.playableAsset.duration);*/
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("button color should change");
            //ColorBlock newColorBlock = GameObject.Find("BackButton").GetComponent<Button>().colors;
            //newColorBlock.normalColor = Color.gray; 
            //GameObject.Find("BackButton").GetComponent<Image>().color = new Color(0.5754717f, 0.5754717f, 0.5754717f, 1f);
        }
    }

    public void PlayIntroAudio()
    {
        audioSource.clip = introClip;
        audioSource.Play();
    }

    public IEnumerator WaitThenCollapseMenu()
    {
        ccMenuOpen = true;
        yield return new WaitForSeconds(20f);
        ccMenuOpen = false;
        uiManager.CollapseMenu();
    }

    public void ToggleCCMenu()
    {
        if (!ccMenuIsMoving)
        {
            StartCoroutine(ToggleCCMenuCoroutine());
        }
       
    }

    public IEnumerator ToggleCCMenuCoroutine()
    {
        if (!ccMenuIsMoving)
        {
            if (ccMenuOpen)
            {
                ccMenuIsMoving = true;
                uiManager.CollapseMenu();
                yield return new WaitForSeconds(1f);
                ccMenuOpen = false;
                GameObject.Find("SimScriptText").GetComponent<TextMeshProUGUI>().text = currentTitratorEpisode.EpisodeText;
                ccMenuIsMoving = false;
            }
            else if (!ccMenuOpen)
            {
                ccMenuIsMoving = true;
                uiManager.ExpandMenu();
                yield return new WaitForSeconds(1f);
                ccMenuOpen = true;
                GameObject.Find("SimScriptText").GetComponent<TextMeshProUGUI>().text = currentTitratorEpisode.CloseCaptionText;
                ccMenuIsMoving = false;
            }
        }
    }

    public void ChangeFontSize()
    {
        float FontSize = GameObject.Find("SimScriptText").GetComponent<TextMeshProUGUI>().fontSize;
        if (FontSize == 18)
        {
            GameObject.Find("SimScriptText").GetComponent<TextMeshProUGUI>().fontSize = 12;
        }
        else if (FontSize == 16)
        {
            GameObject.Find("SimScriptText").GetComponent<TextMeshProUGUI>().fontSize = 18;
        }
        else if (FontSize == 14)
        {
            GameObject.Find("SimScriptText").GetComponent<TextMeshProUGUI>().fontSize = 16;
        }
        else if (FontSize == 12)
        {
            GameObject.Find("SimScriptText").GetComponent<TextMeshProUGUI>().fontSize = 14;
        }
    }

    public void PlayNextEpisode()
    {
        //StartCoroutine(PlayTimelineEpisode());
        StartCoroutine(PlayScene());
    }

    public IEnumerator PlayScene()
    {
        currentTitratorEpisode = TitratorEpisodes[TitratorEpisodeIndex];
        int prevEpisodeIndex = TitratorEpisodeIndex;
        int nextEpisodeIndex = TitratorEpisodeIndex;
        prevEpisodeIndex -= 1;
        nextEpisodeIndex += 1;
        if (prevEpisodeIndex > -1)
        {
            previousTitratorEpisode = TitratorEpisodes[prevEpisodeIndex];
        }
        else
        {
            previousTitratorEpisode = null;
        }
        if (nextEpisodeIndex <= TitratorEpisodes.Count)
        {
            nextTitratorEpisode = TitratorEpisodes[nextEpisodeIndex];
        }
        else
        {
            nextTitratorEpisode = null;
        }
        titrationDirector.time = currentTitratorEpisode.StartFrame;
        titrationDirector.Evaluate();
        if (TitratorEpisodeIndex != 0)
        {
            uiManager.simText.text = currentTitratorEpisode.EpisodeText;
        }
        
        titrationDirector.time = titrationDirector.time;
        if (previousTitratorEpisode!=null)
        {
            if ((previousTitratorEpisode.hasPassed)&&(TitratorEpisodeIndex != 0)){
                GameObject.Find("BackButton").GetComponent<Button>().interactable = true;
                GameObject.Find("BackButton").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            }
            
        }
        else
        {
            Debug.Log("back button should be disabled");
            GameObject.Find("BackButton").GetComponent<Button>().interactable = false;
            
            GameObject.Find("BackButton").GetComponent<Image>().color = new Color(0.3301887f, 0.3301887f, 0.3301887f, 1f);
        }
        Debug.Log("past first check");
        if(nextTitratorEpisode!=null)
        {
            if (nextTitratorEpisode.hasPassed)
            {

                GameObject.Find("ForwardButton").GetComponent<Button>().interactable = true;
                GameObject.Find("ForwardButton").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

            }
            else
            {
                GameObject.Find("ForwardButton").GetComponent<Button>().interactable = false;
                GameObject.Find("ForwardButton").GetComponent<Image>().color = new Color(0.3301887f, 0.3301887f, 0.3301887f, 1f);
            }

        }
        else
        {
            GameObject.Find("ForwardButton").GetComponent<Button>().interactable = false;
            GameObject.Find("ForwardButton").GetComponent<Image>().color = new Color(0.3301887f, 0.3301887f, 0.3301887f, 1f);
        }
        
        titrationDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
        while (true)
        {
            if (titrationDirector.time>=currentTitratorEpisode.EndFrame)
            {
                titrationDirector.time = currentTitratorEpisode.EndFrame;
                titrationDirector.Evaluate();
                titrationDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
                break;
            }
            else if (ffChapter)
            {
                break;
            }
            else if (rewindChapter)
            {
                break;
            }
            yield return null;
        }
        if (ffChapter)
        {
            titrationDirector.time = titrationDirector.time;
            titrationDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
            ffChapter = false;
            TitratorEpisodeIndex += 1;
            if (TitratorEpisodeIndex < TitratorEpisodes.Count)
            {
                PlayNextEpisode();
            }

        }
        else if (rewindChapter)
        {
            titrationDirector.time = titrationDirector.time;
            titrationDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
            rewindChapter = false;
            TitratorEpisodeIndex -= 1;
            if (TitratorEpisodeIndex < TitratorEpisodes.Count)
            {
                PlayNextEpisode();
            }

        }
        else
        {
            while (true)
            {
                if (Input.GetMouseButtonDown(0))
                {



                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 100))
                    {
                        if (hit.transform.tag == currentTitratorEpisode.CorrectObjectTag)
                        {
                            currentTitratorEpisode.hasPassed = true;
                            break;
                        }
                        else if (rewindChapter)
                        {
                            break;
                        }
                        else if (ffChapter)
                        {
                            break;
                        }
                    }
                }
                yield return null;
            }
            if (ffChapter)
            {
                titrationDirector.time = titrationDirector.time;
                titrationDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
                ffChapter = false;
                TitratorEpisodeIndex += 2;
                if (TitratorEpisodeIndex < TitratorEpisodes.Count)
                {
                    PlayNextEpisode();
                }

            }
            else if (rewindChapter)
            {
                titrationDirector.time = titrationDirector.time;
                titrationDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
                rewindChapter = false;
                TitratorEpisodeIndex -= 1;
                if (TitratorEpisodeIndex < TitratorEpisodes.Count)
                {
                    PlayNextEpisode();
                }
            }
            else
            {
                titrationDirector.time = titrationDirector.time;
                titrationDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
                TitratorEpisodeIndex += 1;
                if (TitratorEpisodeIndex < TitratorEpisodes.Count)
                {
                    PlayNextEpisode();
                }
            }
        }
        
        
        yield return null;
    }

    /*public IEnumerator PlayTimelineEpisode()
    {
        Material[] MatArray = new Material[1];

        Debug.Log("at intro");
        if (!introAudioFinished)
        {
            audioSource.clip = introClip;
            audioSource.Play();
            //GameObject.Find("SimScriptText").GetComponent<TextMeshProUGUI>().text = "Let's start by looking at the basic parts of a pump; click each item to put together the pump.";
            while (true)
            {
                if (!audioSource.isPlaying)
                {
                    uiManager.CollapseMenu();
                    GameObject.Find("SimScriptText").GetComponent<TextMeshProUGUI>().text = "Click the titrator knob to turn it from \"off\" to \"standby\"";
                    introAudioFinished = true;
                    titrationDirector.Play();
                    break;
                }
                yield return null;
            }
        }

        TimelineEpisode currentTitratorEpisode = TitratorEpisodes[TitratorEpisodeIndex];
        Debug.Log("start frame is: " + currentTitratorEpisode.StartFrame);
        Debug.Log("index is: " + TitratorEpisodeIndex);
        titrationDirector.time = currentTitratorEpisode.StartFrame;
        titrationDirector.Evaluate();
        Debug.Log("timeline time is now: " + titrationDirector.time);
        

        EpisodeObject = GameObject.Find(currentTitratorEpisode.EpisodeObjectName);
        EpisodeObject.GetComponent<Collider>().enabled = true;
        Debug.Log("TitratorEpisode object is: " + TimelineObject.name);
        if (currentTitratorEpisode.TransparentObjectNames.Count > 0)
        {
            foreach (string name in currentTitratorEpisode.TransparentObjectNames)
            {
                GameObject transpObject = GameObject.Find(name);


                if (transpObject.GetComponent<MeshRenderer>().materials.Count() > 0)
                {
                    Material[] TransMatArray = new Material[2];
                    TransMatArray[0] = TransMaterial;
                    TransMatArray[1] = TransMaterial;
                    transpObject.GetComponent<MeshRenderer>().materials = TransMatArray;
                }
                else
                {
                    transpObject.GetComponent<MeshRenderer>().material = TransMaterial;
                }
            }

        }

        if (currentTitratorEpisode.HasSkinnedMeshRenderer)
        {
            MatArray = EpisodeObject.GetComponent<SkinnedMeshRenderer>().materials;
            RegularMaterial = MatArray[0];
            MatArray[0] = HighlightMaterial;
            EpisodeObject.GetComponent<SkinnedMeshRenderer>().materials = MatArray;
        }
        else
        {
            MatArray = EpisodeObject.GetComponent<MeshRenderer>().materials;
            RegularMaterial = MatArray[0];
            MatArray[0] = HighlightMaterial;
            EpisodeObject.GetComponent<MeshRenderer>().materials = MatArray;
        }

        if (currentTitratorEpisode.AdditionalObjectNames.Count > 0)
        {
            foreach (string name in currentTitratorEpisode.AdditionalObjectNames)
            {
                if (currentTitratorEpisode.HasSkinnedMeshRenderer)
                {
                    GameObject obj = GameObject.Find(name);
                    Material[] GlowMatArray = obj.GetComponent<SkinnedMeshRenderer>().materials;
                    GlowMatArray[1] = HighlightMaterial;
                    obj.GetComponent<SkinnedMeshRenderer>().materials = GlowMatArray;
                }
                else
                {
                    GameObject obj = GameObject.Find(name);
                    Material[] GlowMatArray = obj.GetComponent<MeshRenderer>().materials;
                    GlowMatArray[1] = HighlightMaterial;
                    obj.GetComponent<MeshRenderer>().materials = GlowMatArray;
                }

            }
        }
        Debug.Log("outline added");
        

        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {



                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100) && !uiManager.isPaused)

                {


                    Debug.Log(hit.transform.name);


                    Debug.Log("hit, tag is: " + hit.transform.tag);
                    if (hit.transform.tag == currentTitratorEpisode.CorrectObjectTag)
                    {
                        GameObject.Find("SimScriptText").GetComponent<TextMeshProUGUI>().text = currentTitratorEpisode.EpisodeText;

                        MatArray[0] = RegularMaterial;
                        if (currentTitratorEpisode.HasSkinnedMeshRenderer)
                        {
                            EpisodeObject.GetComponent<SkinnedMeshRenderer>().materials = MatArray;
                        }
                        else
                        {
                            EpisodeObject.GetComponent<MeshRenderer>().materials = MatArray;
                        }

                        if (currentTitratorEpisode.AdditionalObjectNames.Count > 0)
                        {
                            foreach (string name in currentTitratorEpisode.AdditionalObjectNames)
                            {
                                GameObject obj = GameObject.Find(name);
                                Material[] RegMatArray = obj.GetComponent<MeshRenderer>().materials;
                                RegMatArray[1] = RegularMaterial;
                                obj.GetComponent<MeshRenderer>().materials = RegMatArray;
                            }
                        }
                        //TimelineObject.GetComponent<Outline>().enabled = false;


                        /*if (currentTitratorEpisode.AdditionalObjectNames.Count > 0)
                        {
                            foreach (string name in currentTitratorEpisode.AdditionalObjectNames)
                            {
                                Debug.Log("name of additional object is: " + name);
                                GameObject subObject = GameObject.Find(name);
                                subObject.AddComponent<Outline>();
                                Outline subOutlineToEdit = TimelineObject.GetComponent<Outline>();
                                subOutlineToEdit.OutlineColor = new Color(1.60200012f, 1.57944012f, 0, 1);
                                subOutlineToEdit.OutlineWidth = 8f;
                                subObject.GetComponent<Outline>().enabled = false;
                            }
                        }
                        if(currentTitratorEpisode.TransparentObjectNames.Count > 0)
                        {
                            foreach(string name in currentTitratorEpisode.TransparentObjectNames)
                            {
                                GameObject transpObject = GameObject.Find(name);
                                transpObject.GetComponent<MeshRenderer>().material = RegularMaterial;
                            }
                        }*/
                        /*if (!currentTitratorEpisode.NoAnimation)
                        {
                            pumpAnimator.GetComponent<Animator>().SetBool(currentTitratorEpisode.AnimationBool, true);
                        }*/

                        /*audioSource.clip = currentTitratorEpisode.EpisodeAudio;
                        //audioSource.Play();
                        titrationDirector.time = titrationDirector.time;
                        titrationDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
                        Debug.Log("playing episode audio");

                        break;
                    }

                }
            }
            yield return null;
        }

        


            foreach (string name in currentTitratorEpisode.TransparentObjectNames)
            {
                GameObject transpObject = GameObject.Find(name);

                if (transpObject.GetComponent<MeshRenderer>().materials.Count() > 0)
                {
                    Material[] RegMatArray = new Material[2];
                    RegMatArray[0] = RegularMaterial;
                    RegMatArray[1] = RegularMaterial;
                    transpObject.GetComponent<MeshRenderer>().materials = RegMatArray;
                }
                else
                {
                    transpObject.GetComponent<MeshRenderer>().material = RegularMaterial;
                }

            }
            EpisodeObject.GetComponent<Collider>().enabled = false;
        Debug.Log("getting into timecheck loop");
        Debug.Log("heading into timecheck loop");
        Debug.Log("end frame is: " + currentTitratorEpisode.EndFrame + "and time is: " + titrationDirector.time);

        while (true)
        {
            Debug.Log("end frame is: " + currentTitratorEpisode.EndFrame + "and time is: " + Math.Round((float)titrationDirector.time,2));
            if (Math.Round((float)titrationDirector.time,2) == currentTitratorEpisode.EndFrame)
            {
                Debug.Log("end time reached, stopping timeline");
                titrationDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
                break;
            }



            yield return null;
        }
        if (ffChapter)
        {
            Debug.Log("fast forwarding");
            TitratorEpisodeIndex += 2;
            ffChapter = false;
            rewindChapter = false;
        }
        else if (rewindChapter)
        {
            Debug.Log("rewinding an episode");
            TitratorEpisodeIndex -= 1;
            ffChapter = false;
            rewindChapter = false;
        }
        else
        {
            TitratorEpisodeIndex += 1;
        }
        
            Debug.Log("playing next episode, index is " + TitratorEpisodeIndex);
            if (TitratorEpisodeIndex < (TitratorEpisodes.Count))
            {
                PlayNextEpisode();
            }


    }*/

    public void RewindOneEpisode()
    {
        Debug.Log("set to rewind");
        rewindChapter=true;
    }

    public void FastForwardOneEpisode()
    {
        Debug.Log("set to fast forward");
        ffChapter = true;
    }

    public void PauseScene()
    {
        
        titrationDirector.time = titrationDirector.time;
        titrationDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }

    public void UnpauseScene()
    {
        if (!simHasBegun)
        {
            simHasBegun = true;
            PlayNextEpisode();
            //uiManager.ExpandMenu();
            StartCoroutine(WaitThenCollapseMenu());
            Debug.Log(titrationDirector.playableAsset + "is playable asset");
            Debug.Log(titrationDirector.playableAsset.duration);
        }
        titrationDirector.time = titrationDirector.time;
        titrationDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }
}

