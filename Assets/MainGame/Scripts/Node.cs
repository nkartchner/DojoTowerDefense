using UnityEngine;
using UnityEngine.EventSystems;
public class Node : MonoBehaviour
{
    public Color hoverColor;
	public Color NotEnoughMoneyColor;
	public Vector3 positionOffset;

	[HideInInspector]
    public GameObject ninja;
    [HideInInspector]
    public NinjaBlueprint ninjaBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;


    private Renderer rend;
    private Color startColor;

	BuildManager BM;


    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

		BM = BuildManager.instance;
    }

	public Vector3 GetBuildPosition()
	{
		return transform.position + positionOffset;
	}

	private void OnMouseDown()
	{
		if (EventSystem.current.IsPointerOverGameObject()) return;


		if (ninja != null)
		{
			BM.SelectNode(this);
			return;
		}


		if (!BM.CanBuild) return;

		BuildNinja(BM.GetNinjaToBuild());
		BM.DeselectNinja();

	}

	void BuildNinja(NinjaBlueprint NinjaBp)
    {
        if (PlayerStats.Money < NinjaBp.cost)
        {
            Debug.Log("Insuffcient Funds!");
            return;
        }

        PlayerStats.Money -= NinjaBp.cost;

        GameObject _ninja = (GameObject)Instantiate(NinjaBp.prefab, GetBuildPosition(), Quaternion.identity);
        ninja = _ninja;

		ninjaBlueprint = NinjaBp;

        GameObject effect = (GameObject)Instantiate(BM.BuildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        NinjaBp = null;
    }

    public void UpgradeNinja()
    {
        if (PlayerStats.Money < ninjaBlueprint.upgradeCost)
        {
            Debug.Log("Insuffcient Funds!");
            return;
        }

        PlayerStats.Money -= ninjaBlueprint.upgradeCost;


        //Get rid of old turret to replace with updgraded on
        Destroy(ninja);

        //This will become the upgraded one
        GameObject _ninja = (GameObject)Instantiate(ninjaBlueprint.upgradePrefab, GetBuildPosition(), Quaternion.identity);
        ninja = _ninja;

        GameObject effect = (GameObject)Instantiate(BM.BuildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;

    }


	public void SellNinja()
	{
		if (isUpgraded) PlayerStats.Money += ninjaBlueprint.GetSellAmtUpgraded();
		else PlayerStats.Money += ninjaBlueprint.GetSellAmt();

		GameObject Effect = (GameObject)Instantiate(BM.SellEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(Effect, 5f);

		Destroy(ninja);

		ninjaBlueprint = null;
        isUpgraded = false;
	}


	void OnMouseEnter()
	{
		if (EventSystem.current.IsPointerOverGameObject()) return;
		
		if (!BM.CanBuild) return;

		if (BM.HasMoney)
		{
			rend.material.color = hoverColor;
		}
		else
		{
			rend.material.color = NotEnoughMoneyColor;
		}

		
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }



}
