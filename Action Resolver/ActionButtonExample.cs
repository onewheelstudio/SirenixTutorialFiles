using UnityEngine;
using Sirenix.OdinInspector;

public class ActionButtonExample : MonoBehaviour
{
    [ActionButton("DoAction")]
    public string someString;

    [ActionButton("@UnityEngine.Debug.Log(\"I did an action without a method!!!\")")]
    public string anotherString;

    private void DoAction()
    {
        Debug.Log("I did an action!");
    }
}



