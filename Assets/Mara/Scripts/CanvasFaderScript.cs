using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/*
 * A Simple Canvas Fader Script (Unity 5.6)
 * 
 * 
 * Uses : Fade out or Fade In a UI Canvas. Not for transitioning a whole Scene with elements. 
 * 
 * Usage : Add a Canvas Group Component to the UI Canvas. Add this script to the canvas. Add the function for onComplete.
 * 
 * modified : 15/5/2017
 * by Monster Brain Games (monsterbraininc.com)
 * 
 */

public class CanvasFaderScript : MonoBehaviour {
    enum eFadeType {
        fade_in,
        fade_out
    }

    private float _alpha = 1.0f;

    private bool _isFading = false;

    [SerializeField]
    private eFadeType fadeType = eFadeType.fade_in;
    [SerializeField]
    private float fadingDuration = 1.0f; //Time for fading
    private float fadeSpeed = 1.0f;

    [SerializeField]
    private bool _startFadeOnAwake = false;

    [SerializeField]
    private UnityEvent onCompleteEvent;

    private CanvasGroup _canvasGroup;

    void Awake () {
        if (fadeType == eFadeType.fade_in)
            _alpha = 0;
        else if (fadeType == eFadeType.fade_out)
            _alpha = 1.0f;

        _canvasGroup = GetComponent<CanvasGroup> ();
        if (_canvasGroup == null)
            Debug.LogError ("[warning] CanvasFadeScript: please add a Canvas Group to the Canvas");

        if (fadingDuration > 0)
            fadeSpeed = 1 / fadingDuration;

        if (_startFadeOnAwake)
            StartFading ();
    }

    // Use this for initialization
    void Start () {

    }

    public void StartFading () {
        setCanvasAlpha ();
        _isFading = true;
    }

    void setCanvasAlpha () {
        if (_canvasGroup != null) _canvasGroup.alpha = _alpha;
    }

    // Update is called once per frame
    void Update () {
        if (_isFading) {
            if (fadeType == eFadeType.fade_in) {
                FadeIn ();
            }
        } else if (fadeType == eFadeType.fade_out) {
            FadeOut ();
        }

        setCanvasAlpha ();
    }

    private void FadeIn () {
        _alpha += Time.deltaTime * fadeSpeed;
        if (_alpha > 0.95f) {
            onFadeCompleted ();

            FadeOut ();
        }

    }

    private void FadeOut () {

        _alpha -= Time.deltaTime * fadeSpeed;
        if (_alpha < 0.05f) {
            onFadeCompleted ();
            SceneManager.LoadScene ("AllPuzzles-moon");
        }
        fadeType = eFadeType.fade_out;
        setCanvasAlpha ();
    }

    public void onFadeCompleted () {
        _isFading = false;

        //Call On Complete Here
        if (onCompleteEvent != null)
            onCompleteEvent.Invoke ();
    }
}