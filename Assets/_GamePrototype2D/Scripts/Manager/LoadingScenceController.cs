using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadingSceneController : MonoBehaviour
{

    public float FadeOut()
    {
        GetComponent<Animator>().Play("Disappear");
        return 1;
    }
}