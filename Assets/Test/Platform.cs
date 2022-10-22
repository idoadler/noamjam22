using Test;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameColors color; 

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("PLATFORM TRIGGER");
    }
}
