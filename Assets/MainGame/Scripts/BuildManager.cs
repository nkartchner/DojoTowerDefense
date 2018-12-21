using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("More than one BuildManager in scene. Which is a problem");
        }
        instance = this;
    }


    public GameObject BuildEffect;
	public GameObject SellEffect;

    private NinjaBlueprint ninjaToBuild;
    private Node selectedNode;

    public NodeUI nodeUI;
    
    public bool CanBuild { get { return ninjaToBuild != null; } }
	public bool HasMoney { get { return PlayerStats.Money >= ninjaToBuild.cost; } }




    public void SelectNode(Node node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }
        selectedNode = node;
        ninjaToBuild = null;
        
        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

	public void SelectNinjaToBuild(NinjaBlueprint ninja)
	{
		ninjaToBuild = ninja;
        selectedNode = null;


        DeselectNode();
	}

	public void DeselectNinja()
	{
		ninjaToBuild = null;
	}


    public NinjaBlueprint GetNinjaToBuild()
    {
        return ninjaToBuild;
    }

}
