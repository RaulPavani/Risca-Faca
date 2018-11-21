using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {

    public GameObject loadingScreen;
    public Image image;
    public Animator m_ImgAnim;
    public Image m_TransitionImage;

    public Button btnStart, btnCreditos, btnContinuar;

    public  void LoadLevel(string nameScene)
    {
        StartCoroutine(CanvasLoad(nameScene));  //START
    }

    public void Continue(string nameScene)
    {
        //DEIXAR BOTAO APAGADO SE NAO TIVER SAVE**********
        StartCoroutine(CanvasLoad(PlayerPrefs.GetString("lastScene")));
    }

    public void Creditos()
    {
        StartCoroutine(loadCreditos("Creditos"));       
    }

    public void BackMenu()
    {
        StartCoroutine(loadCreditos("Menu"));
    }

    public void StartGame()
    {
        if (GameObject.Find("StageManager"))
        {
            GameObject.Find("StageManager").GetComponent<StageManager>().currentLife = 20;
        }
        StartCoroutine(loadCreditos("Cs_HouseStart"));
    }

    IEnumerator loadCreditos(string m_NextSceneName)
    {
        if (btnContinuar)
        {
            btnContinuar.GetComponent<Animator>().SetBool("start", true);
        }
        if (btnCreditos)
        {
            btnCreditos.GetComponent<Animator>().SetBool("start", true);
        }
        if (btnStart)
        {
            btnStart.GetComponent<Animator>().SetBool("start", true);
        }
        m_ImgAnim.SetBool("Fade", true);
        yield return new WaitForSeconds(1.5f);
        yield return new WaitUntil(() => m_TransitionImage.color.a == 1);

        
        SceneManager.LoadScene(m_NextSceneName);
    }



    IEnumerator LoadAsyn(string nameScene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(nameScene);
    

        //while (!operation.isDone)
        //{
        //    float progress = Mathf.Clamp01(operation.progress / 0.9f);
        //    image.fillAmount = progress;

        //    operation.allowSceneActivation = false;

            yield return null;
        //}
    }


    IEnumerator CanvasLoad(string nameScene)
    { 
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(5f);
        StartCoroutine(LoadAsyn(nameScene));
    }
    

}
