using System.Collections;
using System.Collections.Generic;
using IMDb;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class MenuOptions : MonoBehaviour
{
    [SerializeField] private Image Logo;
    public void QuitMenu()
    {
        Debug.Log("Quiting...");
        Application.Quit();
    }

}
