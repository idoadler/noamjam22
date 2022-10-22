using Test;
using TMPro;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameColors color;
    public TMPro.TextContainer text;

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("PLATFORM TRIGGER");
    }
}
