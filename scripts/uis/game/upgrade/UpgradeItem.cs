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


	public void OnMouseEntered()
	{
		descriptionPanel.Show();
	}

	public void OnMouseExited()
	{
		descriptionPanel.Hide();
	}

}
