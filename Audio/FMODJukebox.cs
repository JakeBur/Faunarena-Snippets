using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Wrapper for the FMOD audio engine. Includes management of the currently playing song. 
/// <remarks>There should be an 'FMOD Studio Listener' component attached to the camera.</remarks>
/// </summary>
public class FMODJukebox : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string songEvent;
    public bool playOnStart;
    public bool skipSelectionSound;

    private FMOD.Studio.EventInstance songEventInstance;

    void Start()
    {
        if(playOnStart)
        {
            FMODJukebox[] jukeboxes = FindObjectsOfType<FMODJukebox>();

            foreach(FMODJukebox jukebox in jukeboxes)
            {
                if(jukebox != this && name == jukebox.name && jukebox.playOnStart)
                {
                    Destroy(gameObject);
                    return;
                }
            }
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            songEventInstance = FMODUnity.RuntimeManager.CreateInstance(songEvent);
            PlaySong();
        }

        skipSelectionSound = true;
    }

    /// <summary>
    /// Stops playback of the current song and starts playback of the song pointed to by 'songEvent'.
    /// </summary>
    public void PlaySong()
    {
        songEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        songEventInstance.release();
        songEventInstance = FMODUnity.RuntimeManager.CreateInstance(songEvent);
        songEventInstance.start();
    }

    /// <summary>
    /// Stops playback
    /// </summary>
    /// <param name="songEvent">The FMODUnity.EventRef for the desired song.</param>
    public void StartNewSong(string songEvent)
    {
        this.songEvent = songEvent;
        PlaySong();
    }

    /// <summary>
    /// Pauses playback of the current song.
    /// </summary>
    public void PauseSong()
    {
        songEventInstance.setPaused(true);
    }

    public void StopSong()
    {
        songEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    /// <summary>
    /// Resumes playback of the current song.
    /// </summary>
    public void ResumeSong()
    {
        songEventInstance.setPaused(false);
    }

    /// <summary>
    /// Sets the given FMOD parameter to the given value. For use in branching to different sections of the song.
    /// </summary>
    /// <param name="parameterName">The name of the FMOD parameter to be set.</param>
    /// <param name="value">The value to set the given parameter to.</param>
    public void SetSongParameter(string parameterName, float value)
    {
        songEventInstance.setParameterValue(parameterName, value);
    }

    /// <summary>
    /// Play the given FMOD event once through.
    /// </summary>
    /// <param name="path">The FMODUnity.EventRef for the desired event.</param>
    public void PlayOneshot(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path);
    }

    public void SkipOneTime(string path)
    {
        if (skipSelectionSound)
        {
            skipSelectionSound = false;
            return;
        }
        PlayOneshot(path);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        skipSelectionSound = true;
        FMODJukebox[] jukeboxes = FindObjectsOfType<FMODJukebox>();

        foreach (FMODJukebox jukebox in jukeboxes)
        {
            if (jukebox != this && name != jukebox.name && jukebox.playOnStart)
            {
                StopSong();
                Destroy(gameObject);
                return;
            }
        }
    }


    /// <summary>
    /// Play the given FMOD event once through from the given location in the game world.
    /// </summary>
    /// <param name="path">The FMODUnity.EventRef for the desired event.</param>
    /// <param name="location">The location the event should emit from.</param>
    public void PlayOneshotAtLocation(string path, Vector3 location)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, location);
    }

    /// <summary>
    /// Play the given FMOD event once through. The position of the emitted sound will follow the given GameObject.
    /// </summary>
    /// <param name="path">The FMODUnity.EventRef for the desired event.</param>
    /// <param name="gameObject">The GameObject the event should emit from.</param>
    public void PlayOneshotFromObject(string path, GameObject gameObject)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(path, gameObject);
    }
}
