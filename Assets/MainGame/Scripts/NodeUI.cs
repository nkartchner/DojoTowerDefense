using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;

	public Text upgradeCost;
	public Button upgradeButton;
	public Text sellAmount;


	private Node target;


    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

		if (!target.isUpgraded)
		{
			upgradeCost.text = $"${target.ninjaBlueprint.upgradeCost}";
			upgradeButton.interactable = true;
			sellAmount.text = $"${target.ninjaBlueprint.GetSellAmt()}";
		}
		else
		{
			sellAmount.text = $"${target.ninjaBlueprint.GetSellAmtUpgraded()}";
			upgradeCost.text = $"MAX";
			upgradeButton.interactable = false;
		}


		ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }



    public void Upgrade()
    {
        target.UpgradeNinja();
        BuildManager.instance.DeselectNode();
    }

	public void Sell()
	{
		target.SellNinja();
		BuildManager.instance.DeselectNode();
	}

}
