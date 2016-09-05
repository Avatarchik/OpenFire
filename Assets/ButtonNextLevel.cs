using UnityEngine;
using System.Collections;

public class ButtonNextLevel : MonoBehaviour
{
    public void NextLevelButton(int index)
    {
        Application.LoadLevel(index);
    }

    public void NextLevelButton(string Customizer)
    {
        Application.LoadLevel(Customizer);
    }
}