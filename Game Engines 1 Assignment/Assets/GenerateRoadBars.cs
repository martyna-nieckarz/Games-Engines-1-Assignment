using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRoadBars : MonoBehaviour
{
    public int roadWidth = 10;
    public int initalRoadLength = 150;
    public float spaceBetweenBlock = 1.0f;
    public int barSectionNum = 10;
    public float spaceBetweenBarSections = 5.0f;
    public GameObject audioBar;
    // Start is called before the first frame update
    public List<GameObject> barPieces = new List<GameObject>();
    void Start()
    {
        int[] sides = {-1, 1};

        for (int sideIndex = 0; sideIndex < sides.Length; sideIndex++) {
            int side = sides[sideIndex];
            float x = side * (roadWidth/2);
            for (int sectionNum = 0; sectionNum < barSectionNum/2; sectionNum++) {
                float z = 0;
                for (int i = 0; i < initalRoadLength; i++) {
                    GameObject bar = GameObject.Instantiate<GameObject>(audioBar);
                        bar.transform.position = new Vector3(x, 0, z);
                    
                    float hue = (i / (float) initalRoadLength * 10) % 1.0f;

                    bar.GetComponent<Renderer>().material.color =
                        Color.HSVToRGB(hue, 1, 1);

                    float barScaleZ = bar.transform.localScale.z;
                    
                    barPieces.Add(bar);

                    z = z + spaceBetweenBlock + barScaleZ;
                }
                x = x + spaceBetweenBarSections * side;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 30.0f;
        for (int i = 0; i < barPieces.Count; i++) {
            GameObject bar = barPieces[i];
            Vector3 barPos = bar.transform.position;
            bar.transform.position = new Vector3(barPos.x, barPos.y, barPos.z - Time.deltaTime * speed);
        }
    }
}
