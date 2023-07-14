using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snaping : MonoBehaviour
{
    [SerializeField] private Vector3 gridSize = new Vector3(0.1f, 0.1f, 1);


    private void OnDrawGizmos()
    {
        if (!Application.isPlaying && this.transform.hasChanged)
        {
            SnapToGrid();
        }
    }

    private void SnapToGrid()
    {
        var position = new Vector3(
                    Mathf.RoundToInt(this.transform.position.x / this.gridSize.x) * this.gridSize.x,
                    Mathf.RoundToInt(this.transform.position.y / this.gridSize.y) * this.gridSize.y,
                    Mathf.RoundToInt(this.transform.position.z / this.gridSize.z) * this.gridSize.z);

        this.transform.position = position;
    }
}
