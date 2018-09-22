using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour {

	IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        yield break;
    }
}
