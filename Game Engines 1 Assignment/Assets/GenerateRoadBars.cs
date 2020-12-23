﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRoadBars : MonoBehaviour
{
    public int roadWidth = 10;
    public int initalRoadLength = 150;
    public float spaceBetweenBlock = 1.0f;
    public int barSectionNum = 10;
    public float spaceBetweenBarSections = 5.0f;

    private int rowCount = 0;
    public GameObject audioBar;

    private float lastBarZ;
    // Start is called before the first frame update
    public List<GameObject> barPieces = new List<GameObject>();
    void Start()
    {
        int[] sides = {-1, 1};

        // float barScaleZ = 0;
        float barScaleZ = audioBar.transform.localScale.z;
        // Iterate over sides (left & right)
        for (int sideIndex = 0; sideIndex < sides.Length; sideIndex++) {
            int side = sides[sideIndex];

            float z = 0;
            for (int i = 0; i < initalRoadLength; i++) {
                z = z + spaceBetweenBlock + barScaleZ;
                lastBarZ = z;

                spawnBarRow(side, 0, z, barScaleZ);
            }
        }
    }

    void spawnBarRow(int side, float y, float z, float barScaleZ) {
        float x = side * (roadWidth/2);

        // barSection is the "column" of each side
        for (int sectionNum = 0; sectionNum < barSectionNum/2; sectionNum++) { 
            GameObject bar = GameObject.Instantiate<GameObject>(audioBar);
                bar.transform.position = new Vector3(x, 0, z);
            
            float hue = (rowCount / (float) initalRoadLength * 10) % 1.0f;

            bar.GetComponent<Renderer>().material.color =
                Color.HSVToRGB(hue, 1, 1);

            barPieces.Add(bar);

            x = x + spaceBetweenBarSections * side;
        }

        rowCount = (rowCount + 1) % initalRoadLength;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 20.0f;
        for (int i = 0; i < barPieces.Count; i++) {
            GameObject bar = barPieces[i];
            Vector3 barPos = bar.transform.position;
            bar.transform.position = new Vector3(barPos.x, barPos.y, barPos.z - Time.deltaTime * speed);
        }
        float maxGap = audioBar.transform.localScale.z + spaceBetweenBlock;

        print("LBZ: " + lastBarZ);

        float lastMovingBarPieceZ = barPieces[barPieces.Count - 1].transform.position.z;
        print("BAR PICE: " + lastMovingBarPieceZ);

        if (lastBarZ - lastMovingBarPieceZ > maxGap) {
            int[] sides = {-1, 1};

            float barScaleZ = audioBar.transform.localScale.z;
            // Iterate over sides (left & right)
            for (int sideIndex = 0; sideIndex < sides.Length; sideIndex++) {
                int side = sides[sideIndex];

                spawnBarRow(side, 0, lastBarZ, barScaleZ);
            }
        }
    }
}
