using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class ScenePreview : MonoBehaviour {

    public GameObject blockLevel;
    public SceneObject preview;
    public bool createPreview;
    public bool removePreview;

   

    void Update()
    {

        if (createPreview)
        {

            removeElements();

            Vector3 position = transform.position;

            for (int j = 0; j < preview.lanes.Length; j++)
            {
                GameObject linesBlocks = new GameObject();
            linesBlocks.transform.position = transform.position;
            linesBlocks.transform.parent = transform;
            
                for (int i = 0; i < 8; i++)
                {
                    GameObject blockNew = Instantiate(blockLevel, new Vector3(position.x + (blockLevel.GetComponent<SpriteRenderer>().sprite.bounds.size.x * i), position.y, position.z), Quaternion.identity) as GameObject;
                    blockNew.transform.parent = linesBlocks.transform;
                    blockNew.GetComponent<SceneBlock>().setBlock(preview, j, i);
                    blockNew.name = "Block -" + j + "," + i;
                }
                linesBlocks.name = "LineBlock - Preview";
                position = new Vector3(position.x, position.y + blockLevel.GetComponent<SpriteRenderer>().sprite.bounds.size.y, position.y);
            }
            
            
            createPreview = false;
        }

        if (removePreview)
        {
            removeElements();
            removePreview = false;
        }
    }

    void removeElements() {
        var tempArray = new GameObject[transform.childCount];

        for (int i = 0; i < tempArray.Length; i++)
        {
            tempArray[i] = transform.GetChild(i).gameObject;
        }

        foreach (var child in tempArray)
        {
            DestroyImmediate(child);
        }
    }
}
