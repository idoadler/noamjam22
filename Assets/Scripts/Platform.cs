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
        counter.text = $"{_currentCollisions}/{maximumAllowed}";
        GameManager.SignPlatform(this);
    }

    private void OnTriggerEnter(Collider col)
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
        GameManager.TestCorrect();
        SoundManager.PlayThump();
        if (!_playingAnim)
        {
            _playingAnim = true;            
            shinyObject.Play();
            Invoke(nameof(AnimDone), shinyObject.effectPlayer.duration + shinyObject.effectPlayer.initialPlayDelay);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        var character = col.GetComponent<Character>();
        if (character != null)
        {
            if (character.colors.All(testCol => color != testCol))
            {
                _currentCollisions--;
                counter.text = $"{_currentCollisions}/{maximumAllowed}";
            }
        }
    }

    private void AnimDone()
    {
        _playingAnim = false;
    }

    public bool IsCorrect()
    {
        return _currentCollisions == maximumAllowed;
    }
}
