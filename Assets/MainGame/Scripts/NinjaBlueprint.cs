using UnityEngine;
using System.Collections;

[System.Serializable]
public class NinjaBlueprint
{
	public GameObject prefab;
	public int cost;

    public int upgradeCost;
    public GameObject upgradePrefab;

	public int GetSellAmt()
	{
		return cost / 3;
	}

	public int GetSellAmtUpgraded()
	{
		
		return Mathf.FloorToInt((cost + upgradeCost) / 2.7f);
	}

}
