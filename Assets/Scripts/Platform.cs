using System;
using System.Linq;
using Coffee.UIEffects;
using Test;
using TMPro;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public int maximumAllowed = 1;
    public GameColors color;
    public TextMeshProUGUI counter;
    public UIShiny shinyObject;
    
    private bool _playingAnim = false;
    private int _currentCollisions = 0;

    private void Start()
    {
        GameManager.SignPlatform(maximumAllowed);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var character = col.GetComponent<Character>();
        if ( character != null)
        {
            if (character.colors.Any(testCol => color == testCol))
            {
                // Same color
                SoundManager.PlayThumpSame();
                return;
            }
        }
        // Different color
        _currentCollisions++;
        counter.text = $"{_currentCollisions}/{maximumAllowed}";
        GameManager.AddCorrect();
        SoundManager.PlayThump();
        if (!_playingAnim)
        {
            _playingAnim = true;            
            shinyObject.Play();
            Invoke(nameof(animDone), shinyObject.effectPlayer.duration + shinyObject.effectPlayer.initialPlayDelay);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        var character = col.GetComponent<Character>();
        if (character != null)
        {
            if (character.colors.All(testCol => color != testCol))
            {
                _currentCollisions--;
                counter.text = $"{_currentCollisions}/{maximumAllowed}";
                GameManager.RemoveCorrect();
            }
        }
    }

    private void printTime(float time)
    {
        Debug.Log(time);
    }
    private void animDone()
    {
        _playingAnim = false;
    }
}
