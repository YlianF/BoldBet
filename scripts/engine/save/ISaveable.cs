using Godot.Collections;
using Godot;



namespace BoldBet.Engine.Save;

public interface ISaveable
{
	public StringName Name {get;}
	public Dictionary<string, Variant> SaveObject();


	public Dictionary<string, Dictionary<string, Variant>> Save()
    {
        return new Dictionary<string, Dictionary<string, Variant>>()
        {
			
            { Name, SaveObject() }
        };
    }

	

}
/**				
				new Dictionary<string, Variant>()
                {
                    { "Gold", Gold },
                    { "BoldPoints", BoldPoints },
                    { "CylinderSize", CylinderSize },
                }
*/