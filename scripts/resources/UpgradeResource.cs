using BoldBet.Enum;
using Godot;
using System;

namespace BoldBet.Resources;

[GlobalClass]
public partial class UpgradeResource : Resource
{
    [ExportSubgroup("Upgrade Values")]
    [Export] public string Property;
    [Export] public int BaseCost;
    [Export] public float CostPerLevel;
    [Export] public Operation CostOperation;

    [Export] public float BaseUpgrade;
    [Export] public float UpgradePerLevel;
    [Export] public DecimalPlacesEnum DecimalPlaces;
    [Export] public int LevelMax;

    [ExportSubgroup("Upgrade Info")]
    [Export] public string Title;
    [Export(PropertyHint.MultilineText)] public string Description;
}
