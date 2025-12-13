using BoldBet.Resources;
using Godot;
using Godot.Collections;
using System;

namespace BoldBet.Engine;
public partial class UpgradeManager : Node
{
	private VBoxContainer upgradeConaitner;
	private EngineLogic engineLogic;

	[Export] public Array<UpgradeResource> upgradesList;
	[Export] public PackedScene upgradeItemScene;


	public override void _Ready()
	{
		upgradeConaitner = GetNode<VBoxContainer>("%UpgradeConaitner");
		engineLogic = GetNode<EngineLogic>("%EngineLogic");
		
		
		//Used if in editor, upgrades is no clear
		ClearUpgrade();

		SpawnUpgrade(upgradesList);
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
		newUpgrade.CallDeferred("Init", upgradeResource);

		upgradeConaitner.AddChild(newUpgrade);
		//Connect au signal
		

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
}
