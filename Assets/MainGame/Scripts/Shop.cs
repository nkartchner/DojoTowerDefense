using UnityEngine;

public class Shop : MonoBehaviour
{
	public NinjaBlueprint standardTurret;
	public NinjaBlueprint Ninja1;
	public NinjaBlueprint missileLauncher;
    public NinjaBlueprint laserTurret;
	public NinjaBlueprint Donavin;

	BuildManager buildManager;

	private void Start()
	{
		buildManager = BuildManager.instance;
	}



	public void SelectStandardTurret()
    {
        Debug.Log("Standard Turret Selected");
		buildManager.SelectNinjaToBuild(standardTurret);
	}

	public void SelectMissileLauncher()
	{
		Debug.Log("Missile Launcher Selected");
		buildManager.SelectNinjaToBuild(missileLauncher);
	}

    public void SelectLaserTurret()
    {
        Debug.Log("Laser Turret Selected");
        buildManager.SelectNinjaToBuild(laserTurret);
    }

	public void SelectNinja1()
	{
		Debug.Log("Ninja1 Selected");
		buildManager.SelectNinjaToBuild(Ninja1);
	}


	public void SelectDonavin()
	{
		Debug.Log("Donavin Selected");
		buildManager.SelectNinjaToBuild(Donavin);
	}

	public void SelectNoelle()
    {
        Debug.Log("Noelle Selected");
    }

    public void SelectAllan()
    {
        Debug.Log("Allan Selected");
    }

}
