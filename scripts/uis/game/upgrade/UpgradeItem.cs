using BoldBet.Enum;
using BoldBet.Resources;
using Godot;
using System;

public partial class UpgradeItem : Control
{
	private Panel panel;
	private Control titleText;
	//private Control lvText;
	private Control lvValueText;
	private Control costValueText;
	private PanelContainer descriptionPanel;
	private Button button;

	private int costValue;



	public override void _Ready()
	{
		panel = GetNode<Panel>("%Panel");
		titleText = GetNode<Control>("%TitleText");
		//lvText = GetNode<Control>("%LvText");
		lvValueText = GetNode<Control>("%LvValueText");
		costValueText = GetNode<Control>("%CostValueText");
		descriptionPanel = GetNode<PanelContainer>("%DescriptionPanel");
		button = GetNode<Button>("%Button");

		descriptionPanel.Hide();
	}


	public void Init(UpgradeResource upgradeResource, bool desactiveButton)
	{
		titleText.Set("text", upgradeResource.Title);
		descriptionPanel.Call("set_title_text", upgradeResource.Title);
		descriptionPanel.Call("set_text", upgradeResource.Description);

		string currentStat = FormatDecimalPlaces.Formater(upgradeResource.BaseUpgrade, upgradeResource.DecimalPlaces);
		string previewtStat = FormatDecimalPlaces.Formater(upgradeResource.BaseUpgrade + upgradeResource.UpgradePerLevel, upgradeResource.DecimalPlaces);
		descriptionPanel.Call("set_current_stat_text", currentStat);
		descriptionPanel.Call("set_preview_stat_text", previewtStat);

		costValue = upgradeResource.BaseCost;
		costValueText.Set("text", upgradeResource.BaseCost.ToString());

		//Desactive le moyen d'achat
		if(desactiveButton)
		{
			panel.Modulate = Colors.Gray;
			button.Disabled = true;
		}
	}


	public void OnMouseEntered()
	{
		descriptionPanel.Show();
	}

	public void OnMouseExited()
	{
		descriptionPanel.Hide();
	}

	public void OnBoldPointsChange(int value)
	{
		if(costValue <= value)
		{
			panel.Modulate = Colors.White;
			button.Disabled = false;
		}
		else
		{
			panel.Modulate = Colors.Gray;
			button.Disabled = true;
		}
	}

	internal void SetLv(int lv, int newCost)
	{
		lvValueText.Set("text", lv.ToString());
		
		costValueText.Set("text", newCost.ToString());
		costValue = newCost;
	}
}
