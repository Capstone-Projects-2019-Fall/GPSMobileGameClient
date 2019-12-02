using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    private GameObject loginCanvas;
    private InputField username;
    private InputField password;
    // Start is called before the first frame update
    void Start()
    {
        loginCanvas = GameObject.Find("LoginUI");
        username = loginCanvas.transform.Find("Username").GetComponent<InputField>();
        password = loginCanvas.transform.Find("Password").GetComponent<InputField>();
        loginCanvas.transform.Find("LoginButton").GetComponent<Button>().onClick.AddListener(StartLogin);
    }

    private void StartLogin()
    {
        if(username.text != "" && password.text != "")
        {
            // Debug.LogFormat("Username: {0}\nPassword: {1}", username.text, password.text);
            StartCoroutine(APIWrapper.getPlayer(username.text, (existingPlayerQuery) => {
                if(existingPlayerQuery == null)
                {
                    Debug.LogFormat("Create new player");
                    StartCoroutine(APIWrapper.createPlayer(username.text, password.text, (createPlayerQuery) => {
                        if(createPlayerQuery != null)
                        {
                            PlayerPrefs.SetString(Player.usernameKey, createPlayerQuery["name"]);
                            PlayerPrefs.Save();
                            SceneManager.LoadScene(1);
                        }
                    }));
                }
                else
                {
                    Debug.LogFormat("Load existing data: {0}", existingPlayerQuery);
                    PlayerPrefs.SetString(Player.usernameKey, existingPlayerQuery["name"]);
                    PlayerPrefs.Save();
                    SceneManager.LoadScene(1);
                }            
            }));
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
