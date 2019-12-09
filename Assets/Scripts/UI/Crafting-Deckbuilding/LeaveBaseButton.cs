using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveBaseButton : MonoBehaviour
{
    public void LeaveBase()
    {
        SceneManager.LoadScene("GPSMobileGame");
    }
}
