using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualKeyBoard : MonoBehaviour
{
    public void InputKey()
    {
        string name = this.name;

        char aux = char.Parse(name);

        FindObjectOfType<keyInput>().CheckLetter(aux);
    }
}
