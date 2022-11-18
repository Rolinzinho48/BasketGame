using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pontos : MonoBehaviour
{
    public static int cont{get;set;}

    public static float Tempo{get;set;}
    public AudioSource gameover,fundo;
    public Text txt;
    public Image img;
    public GameObject panel;


    void Start()
    {
        cont = 0;
        Tempo = 40;
        fundo.Play();
        Time.timeScale = 1;
    }

    public void Update()
    {
        txt.text = cont.ToString();
        if(Tempo>1){
            Tempo-=Time.deltaTime;
            img.fillAmount = Tempo/40;
        }
        else if(Tempo < 1){
            fundo.Stop();
            gameover.Play();
            Tempo = 1;
            Time.timeScale = 0;
            panel.SetActive(true);
        }
            
    }

    public static void AddCont(){
        cont++;
        Tempo+=3;
    }
    
    public void carregarCena(){
        SceneManager.LoadScene("Scene 1");
    }
}
