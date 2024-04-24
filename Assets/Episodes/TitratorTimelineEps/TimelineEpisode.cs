using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New TimelineEpisode", menuName = "TimelineEpisodes", order = 76)]
public class TimelineEpisode : ScriptableObject
{
    public float EndFrame;
    public float StartFrame;
    public string EpisodeObjectName;
    /*public List<string> AdditionalObjectNames = new List<string>();
    public List<string> TransparentObjectNames = new List<string>();
    public AudioClip EpisodeAudio;*/
    public string CorrectObjectTag;
    /*public bool NoAnimation = false;
    public bool OtherAnimator = false;
    public string OtherAnimatorName;
    public List<string> ExtensionMethods = new List<string>();
    public List<string> OpeningExtensionMethods = new List<string>();
    public bool HasSkinnedMeshRenderer = false;*/
    public string EpisodeText;
    public string CloseCaptionText;
    public bool hasPassed = false;
}
