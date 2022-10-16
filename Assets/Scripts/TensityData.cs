using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TensityData
{
    public float sumTensity;
    public float deathPositionTensity;
    public float playerHealthTensity;
    public float enemyCountTensity;

    public TensityData(float sumTensity, float deathPositionTensity, float playerHealthTensity, float enemyCountTensity)
    {
        this.sumTensity = sumTensity;
        this.deathPositionTensity = deathPositionTensity;
        this.playerHealthTensity = playerHealthTensity;
        this.enemyCountTensity = enemyCountTensity;
    }
}
