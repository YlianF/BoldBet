using Godot;
using System;

namespace BoldBet.Resources;

[GlobalClass]
public partial class UpgradeResource : Resource
{
    [Export] public string Property;
    [Export] public int BaseCost;
    [Export] public int CostPerLevel;
    [Export] public float UpgradeValue;
    [Export] public int LevelMax;
    [Export] public string Title;
    [Export(PropertyHint.MultilineText)] public string Description;
}
