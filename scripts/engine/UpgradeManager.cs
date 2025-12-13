using BoldBet.Resources;
using Godot;
using Godot.Collections;
using System;

namespace BoldBet.Engine;
public partial class UpgradeManager : Node
{
	private VBoxContainer upgradeConaitner;
	private EngineLogic engineLogic;
	private Array<int> upgradesLvList;
	private Array<UpgradeItem> upgradesItemList;

	[Export] public Array<UpgradeResource> upgradeResourceList;
	[Export] public PackedScene upgradeItemScene;


	public override void _Ready()
	{
		upgradesLvList = new();
		upgradesItemList = new();

		upgradeConaitner = GetNode<VBoxContainer>("%UpgradeConaitner");
		engineLogic = GetNode<EngineLogic>("%EngineLogic");
		
		
		//Used if in editor, upgrades is no clear
		ClearUpgrade();

		SpawnUpgrade(upgradeResourceList);
	}


	private void SpawnUpgrade(Array<UpgradeResource> upgradesList)
	{
		foreach(UpgradeResource upgradeResource in upgradesList)
		{
			SpawnUpgrade(upgradeResource, false);
		}
	}

	private void SpawnUpgrade(UpgradeResource upgradeResource, bool animation = true)
	{
		UpgradeItem newUpgrade = upgradeItemScene.Instantiate<UpgradeItem>();
		newUpgrade.CustomMinimumSize = new Vector2(0f, 50f);
		newUpgrade.CallDeferred("Init", upgradeResource, upgradeResource.BaseCost >= engineLogic.BoldPoints);

		upgradeConaitner.AddChild(newUpgrade);
		
		//Connect les signaux
		engineLogic.boldPointsChange += newUpgrade.OnBoldPointsChange;
		GD.Print(upgradesLvList.Count);
		int index = upgradesLvList.Count;
		newUpgrade.GetNode<Button>("%Button").Pressed += () => OnUpgradeBuy(index);

		//Ajoute dans les listes
		upgradesLvList.Add(0);
		upgradesItemList.Add(newUpgrade);
		

		if(animation)
		{
			// add Tween
		}
	}

	private void ClearUpgrade()
	{
		foreach(var child in upgradeConaitner.GetChildren())
		{
			upgradeConaitner.RemoveChild(child);
		}
	}

	private void OnUpgradeBuy(int upgradeNumber)
	{
		var upgradeResource = upgradeResourceList[upgradeNumber];
		int lv = upgradesLvList[upgradeNumber];

		upgradesLvList[upgradeNumber] = lv;
		int cost = (int)(upgradeResource.BaseCost + upgradeResource.BaseCost * lv * upgradeResource.CostPerLevel);

		if(engineLogic.BoldPoints >= cost)
		{
			engineLogic.BoldPoints -= cost;

			lv += 1;
			upgradesLvList[upgradeNumber] = lv;
			int newCost = (int)(upgradeResource.BaseCost + upgradeResource.BaseCost * lv * upgradeResource.CostPerLevel);
			upgradesItemList[upgradeNumber].SetLv(lv, newCost);

			// Modification des valeurs de EngineLogic
		}
	}
	
}
