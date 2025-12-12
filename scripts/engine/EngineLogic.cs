using Godot;
using System;
using BoldBet.Engine.Save;
using BoldBet.Enum;


namespace BoldBet.Engine;
public partial class EngineLogic : Node, ISaveable
{
    private float Gold = 0;


    private float boldPoints = 0;
    public float BoldPoints
    {
        get => boldPoints;
        set
        {
            boldPoints = value;
            EmitSignal(SignalName.boldPointsChange, value);
        }
    }
    [Signal] public delegate void boldPointsChangeEventHandler(int value);

    [Export] private int CylinderSize = 4;
    private Godot.Collections.Array<BulletType> Cylinder = [];
    [Export] private int SpecialBulletsNb = 1;
    private Godot.Collections.Array<BulletType> SpecialBullets = [BulletType.Death, BulletType.Combo];
    private int ShotsTaken = 0;
    [Export] private float BaseCombo = 1.0f;
    [Export] private float ComboMult = 0.2f;
    private float CurrentCombo = 1.0f;


    // Bool pour savoir si la save doit etre charger à Instantiate
    public bool needLoad = false;
    private SaveManager saveManager;
    
    
    public override void _Ready() {

        saveManager = GetNode<SaveManager>("%SaveManager");
        if (needLoad)
		{
			saveManager.LoadGame();
            GD.Print("Load game complete");
		}
        
        CurrentCombo = BaseCombo;
        LoadRevolver();
    }

    public override void _PhysicsProcess(double delta) {
        if (Input.IsActionJustPressed("shoot")) {
            Shoot();
        }

        if (Input.IsActionJustPressed("reload")) {
            LoadRevolver();
        }
    }

    private void LoadRevolver() {
        Cylinder.Resize(CylinderSize);
        Cylinder.Fill(BulletType.Blank);
        LoadBullet(SpecialBullets);
        Shuffle(Cylinder);
        GD.Print(Cylinder);
    }

    private void LoadBullet(Godot.Collections.Array<BulletType> Bullets) {
        for (int i=0; i<Bullets.Count; i++) {
            Cylinder[i] = Bullets[i];
        }
    }

    private void Shuffle (Godot.Collections.Array<BulletType> Cylinder) {
        Random rnd = new Random();
        for (int i = Cylinder.Count; i > 1; i--) {
            int pos = rnd.Next(i);
            var x = Cylinder[i - 1];
            Cylinder[i - 1] = Cylinder[pos];
            Cylinder[pos] = x;
        }
    }

    private void Shoot() {
        if (ShotsTaken < CylinderSize) {
            BulletHandler(Cylinder[ShotsTaken]);
        }
    }

    private void Die() {
        CalculateCombo();
        LoadRevolver();
        CurrentCombo = BaseCombo;
        ShotsTaken = 0;
    }

    private void BulletHandler(BulletType bullet) {
        if (bullet == BulletType.Death) {
            GD.Print("die");
            Die();
        } else if (bullet == BulletType.Blank) {
            ShotsTaken++;
            CurrentCombo = CurrentCombo + ComboMult * ShotsTaken;
        } else if (bullet == BulletType.Combo) {
            ShotsTaken++;
            CurrentCombo = CurrentCombo * 2;
        }
    }

    private void CalculateCombo() {
        float NewGold =  ShotsTaken * CurrentCombo;
        float NewBoldPoints =  ShotsTaken * CurrentCombo;

        Gold = Gold + NewGold;
        BoldPoints = BoldPoints + NewBoldPoints;
        GD.Print(CurrentCombo);
        GD.Print(BoldPoints);
    }







    public Godot.Collections.Dictionary<string, Variant> SaveObject()
    {
        return new Godot.Collections.Dictionary<string, Variant>()
        {
            { "Gold", Gold },
            { "BoldPoints", BoldPoints },
            { "CylinderSize", CylinderSize },
        };
            
        
    }

    public void OnButtonSavePressed()
	{
        GD.Print("Save start");
		saveManager.SaveGame();
        GD.Print("Save game complete");
	}
}
