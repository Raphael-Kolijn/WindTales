using System.Collections;
using UnityEngine;
public class Settings_BB : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;
    public void OpenSettings()
    {
        panel.SetActive(true);
        GameMaster.instance.getBall().SetActive(false);
    }
}
