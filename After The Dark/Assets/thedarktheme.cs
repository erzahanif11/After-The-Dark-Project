using UnityEngine;

public class thedarktheme : MonoBehaviour
{
    public AudioSource thedarkthemeaudio;



    public void queTheMusic (){
        thedarkthemeaudio.Play();
    }

    public void stopTheMusic()
    {
        thedarkthemeaudio.Stop();
    }   
}
