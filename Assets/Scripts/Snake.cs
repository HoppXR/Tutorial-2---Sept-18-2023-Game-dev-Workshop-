using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Snake : MonoBehaviour
{
    [SerializeField] private int _headTile;
    [SerializeField] private int _tailTile;

    public Snake(int headTile, int tailTile)
    {
        _headTile = headTile;
        _tailTile = tailTile;
    }

    public int GetHeadTile()
    {
        return _headTile;
    }

    public int GetTailTile()
    {
        return _tailTile;
    }
}
