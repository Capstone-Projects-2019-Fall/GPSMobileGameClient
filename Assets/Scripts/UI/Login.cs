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
    private InputField address;
    // Start is called before the first frame update
    void Start()
    {
        loginCanvas = GameObject.Find("LoginUI");
        username = loginCanvas.transform.Find("Username").GetComponent<InputField>();
        username.text = PlayerPrefs.GetString(Player.usernameKey, "");
        address = loginCanvas.transform.Find("Address").GetComponent<InputField>();
        address.text = PlayerPrefs.GetString(Player.addressKey, "");
        password = loginCanvas.transform.Find("Password").GetComponent<InputField>();
        loginCanvas.transform.Find("LoginButton").GetComponent<Button>().onClick.AddListener(StartLogin);
    }

    private void StartLogin()
    {
        if(username.text != "" && address.text != "" && password.text != "")
        {
            // Debug.LogFormat("Username: {0}\nAddress: {1}\nPassword: {2}", username.text, address.text, password.text);
            
            StartCoroutine(APIWrapper.getPlayer(username.text, (existingPlayerQuery) => {
                if(existingPlayerQuery == null)
                {
                    // Debug.LogFormat("Create new player");

                    StartCoroutine(APIWrapper.geocodeAddress(address.text, (geocodeResponse) => {
                        // If address was successfully geocoded as GPS coordinates.
                        if(geocodeResponse != null && geocodeResponse["message"] == null && geocodeResponse["features"].Count > 0)
                        {
                            // Debug.LogFormat("Geocoding: {0}", geocodeResponse.ToString());
                            double lat = geocodeResponse["features"][0]["center"][1];
                            double lon = geocodeResponse["features"][0]["center"][0];
                            StartCoroutine(APIWrapper.createPlayer(username.text, password.text, lat, lon, (createPlayerQuery) => {
                                if(createPlayerQuery != null)
                                {
                                    SavePlayerPrefsAndLoadOverworld();
                                }
                            }));
                        }
                    }));
                }
                else
                {
                    SavePlayerPrefsAndLoadOverworld();
                }            
            }));
        }
    }

    private void SavePlayerPrefsAndLoadOverworld()
    {
        PlayerPrefs.SetString(Player.usernameKey, username.text);
        PlayerPrefs.SetString(Player.addressKey, address.text);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GPSMobileGame");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
