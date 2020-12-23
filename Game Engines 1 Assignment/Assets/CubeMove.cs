using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, AudioAnalyzer.smoothedAmplitude * Time.deltaTime * rotSpeed, 0);
        for (int i = 0; i < elements.Count; i++) {
            Vector3 ls = elements[i].transform.localScale;
            ls.y = Mathf.Lerp(ls.y, 1 + (AudioAnalyzer.bands[i] * scale), Time.deltaTime * 3.0f);
            elements[i].transform.localScale = ls;
            Vector3 pos = elements[i].transform.position;
            pos.y = startPositions[i].y + (ls.y / 2);
            elements[i].transform.position = pos;
        
    }
}
