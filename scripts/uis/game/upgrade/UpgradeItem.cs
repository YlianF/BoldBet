using BoldBet.Enum;
using BoldBet.Resources;
using Godot;
using System;

public partial class UpgradeItem : Control
{
	private Panel panel;
	private Control titleText;
	private Control lvText;
	private Control lvValueText;
	private PanelContainer descriptionPanel;



	public override void _Ready()
	{
		panel = GetNode<Panel>("%Panel");
		titleText = GetNode<Control>("%TitleText");
		lvText = GetNode<Control>("%LvText");
		lvValueText = GetNode<Control>("%LvValueText");
		descriptionPanel = GetNode<PanelContainer>("%DescriptionPanel");

		descriptionPanel.Hide();
	}


	public void Init(UpgradeResource upgradeResource)
	{
		titleText.Set("text", upgradeResource.Title);
		descriptionPanel.Call("set_title_text", upgradeResource.Title);
		descriptionPanel.Call("set_text", upgradeResource.Description);

		string currentStat = FormatDecimalPlaces.Formater(upgradeResource.BaseUpgrade, upgradeResource.DecimalPlaces);
		string previewtStat = FormatDecimalPlaces.Formater(upgradeResource.BaseUpgrade + upgradeResource.UpgradePerLevel, upgradeResource.DecimalPlaces);
		descriptionPanel.Call("set_current_stat_text", currentStat);
		descriptionPanel.Call("set_preview_stat_text", (upgradeResource.BaseUpgrade + upgradeResource.UpgradePerLevel).ToString("0.0"));

	}


	public void OnMouseEntered()
	{
		descriptionPanel.Show();
	}

	public void OnMouseExited()
	{
		descriptionPanel.Hide();
	}

}
