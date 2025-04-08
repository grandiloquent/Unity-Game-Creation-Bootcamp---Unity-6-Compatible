using UnityEngine;

public class SoundController: MonoBehaviour
{
   public AudioSource shot;
    
    void Start()
    {

    }
    void Update()
    {

    }
    void FixedUpdate()
    {

    }
    void Fire(){
        shot.Play();
    }
}