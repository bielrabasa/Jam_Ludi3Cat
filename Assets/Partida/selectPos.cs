using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectPos : MonoBehaviour
{
    public void changePos()
    {
        TextUIGenerator tUIG;

        tUIG = FindObjectOfType<TextUIGenerator>();

        tUIG.SetPos(this.gameObject);
    }
}
