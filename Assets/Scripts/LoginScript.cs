using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void changeScene()
    {
        GameObject nameInput = GameObject.FindGameObjectWithTag("nameInput");
       
        print("HOLA" + nameInput.GetComponent<InputField>().text);
    }
}
