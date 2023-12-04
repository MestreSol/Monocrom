using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ConfigureController : MonoBehaviour
{
    public GameObject Jogar;
    public GameObject Acessibilidade;
    public GameObject Video;
    public GameObject Audio;
    
    private void ActiveThis(GameObject obj)
    {
        Jogar.gameObject.SetActive(false);
        Acessibilidade.gameObject.SetActive(false);
        Video.gameObject.SetActive(false);
        Audio.gameObject.SetActive(false);

        obj.SetActive(true);
    }

    public void ActiveJogar()
    {
        ActiveThis(Jogar);
    }  
    public void ActiveAcessibilidade()
    {
        ActiveThis(Acessibilidade);
    }
    public void ActiveVideo()
    {
        ActiveThis(Video);
    }
    public void ActiveAudio()
    {
        ActiveThis(Audio);
    }

}
