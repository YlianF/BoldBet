using Godot;
using System;

public partial class EngineLogic : Node
{
    private int Gold = 0;
    private int BoldPoints = 0;

    [Export] private int CylinderSize = 4;
    private Godot.Collections.Array<BulletType> Cylinder = [];
    private int ShotsTaken = 0;
    private float BaseCombo = 1.0f;


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
        LoadBullet(BulletType.Death);
        GD.Print(Cylinder);
    }

    private void LoadBullet(BulletType BulletType) {
        Random rnd = new Random();
        int Bullet  = rnd.Next(0, CylinderSize);
        Cylinder[Bullet] = BulletType;
    }

    private void Shoot() {
        if (ShotsTaken < CylinderSize) {
            BulletHandler(Cylinder[ShotsTaken]);
        }
    }

    private void Die() {
        CalculateCombo();
        LoadRevolver();
        BaseCombo = 1.0f;
        ShotsTaken = 0;
    }

    private void BulletHandler(BulletType bullet) {
        if (bullet == BulletType.Death) {
            GD.Print("die");
            Die();
        } else if (bullet == BulletType.Blank)
        {
            BaseCombo = BaseCombo + 0.1f;
            ShotsTaken++;
        }
    }

    private void CalculateCombo() {
        float Combo = BaseCombo * 100;
        int NewGold =  ShotsTaken * (int) Combo;
        int NewBoldPoints =  ShotsTaken * (int) Combo;

        Gold = Gold + NewGold;
        BoldPoints = BoldPoints + NewBoldPoints;
        GD.Print(Combo);
        GD.Print(BoldPoints);
    }







    public Godot.Collections.Dictionary<string, Godot.Collections.Dictionary<string, Variant>> Save()
    {
        return new Godot.Collections.Dictionary<string, Godot.Collections.Dictionary<string, Variant>>()
        {
            { Name,
                new Godot.Collections.Dictionary<string, Variant>()
                {
                    { "Gold", Gold },
                    { "BoldPoints", BoldPoints },
                    { "CylinderSize", CylinderSize },
                }
            }
        };
    }

    public void OnButtonSavePressed()
	{
        GD.Print("Save start");
		saveManager.SaveGame();
        GD.Print("Save game complete");
	}
}
