using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEntrance : MonoBehaviour
{
    private void Awake()
    {
        SceneManagerScript.OnEntranceDetailsGotten(this.gameObject);
    }
}
