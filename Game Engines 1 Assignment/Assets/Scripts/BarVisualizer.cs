using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarVisualizer : MonoBehaviour
{
    private List<List<GameObject>> barStrips = new List<List<GameObject>>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject roadBarSpawner = GameObject.FindWithTag("GenerateRoadBars");
        barStrips = roadBarSpawner.GetComponent<GenerateRoadBars>().barStrips;
    }

    // Update is called once per frame
    void Update()
    {
        float scale = 50.0f;
        
        for (int stripNum = 0; stripNum < barStrips.Count; stripNum++) {
            List<GameObject> strip = barStrips[stripNum];
            for (int i = strip.Count -1; i > -1; i--) {
                GameObject bar = strip[i];

                int pos = (int) ((i / 2) % AudioAnalyzer.bands.Length);

                Vector3 ls = bar.transform.localScale;
                ls.y = Mathf.Lerp(ls.y, 1 + (AudioAnalyzer.bands[pos] * scale), Time.deltaTime * 5.0f);
                bar.transform.localScale = ls;
            }
        }

    }
}
