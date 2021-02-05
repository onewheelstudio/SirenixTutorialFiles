using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ButtonShortExamples : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject gridItem;
    private List<GameObject> gridItemsList = new List<GameObject>();

    [ButtonGroup("GridButton")]
    private void GenerateGrid()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject newGriditem 
                    = Instantiate(gridItem);
                newGriditem.transform.position 
                    = new Vector3(i, 0.25f, j) * 2;
                gridItemsList.Add(newGriditem);
            }
        }
    }

    [ButtonGroup("GridButton")]
    private void ClearGrid()
    {
        for (int i = 0; i < gridItemsList.Count; i++)
        {
            if (Application.isEditor)
                DestroyImmediate(gridItemsList[i]);
            else
                Destroy(gridItemsList[i]);
        }
    }

    private void SomeGeneric<T>() where T : MonoBehaviour
    {
        
    }

    private void AnotherGeneric<T>() where T : Component
    {

    }
}
