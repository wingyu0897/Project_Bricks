using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/DATA/Stage")]
public class StageSO : ScriptableObject
{
    public int stageNumber;
    public string stageName;
    public int difficult;
    public ShapeSO shape;
    public Sprite stageImage;
    public List<BrickItem> brickItems;
}
