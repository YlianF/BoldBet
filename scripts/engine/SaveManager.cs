using Godot;
using System;

public partial class SaveManager : Node
{
	public const string savePath = "user://game.save";
    public void SaveGame()
    {
        using var saveFile = FileAccess.Open(savePath, FileAccess.ModeFlags.Write);

		var saveNodes = GetTree().GetNodesInGroup("Saveable");
		foreach (Node saveNode in saveNodes)
		{
			// Check the node is an instanced scene so it can be instanced again during load.
			if (string.IsNullOrEmpty(saveNode.SceneFilePath))
			{
				GD.Print($"persistent node '{saveNode.Name}' is not an instanced scene, skipped");
				continue;
			}

			// Check the node has a save function.
			if (!saveNode.HasMethod("Save"))
			{
				GD.Print($"persistent node '{saveNode.Name}' is missing a Save() function, skipped");
				continue;
			}

			// Call the node's save function.
			var nodeData = saveNode.Call("Save");

			// Json provides a static method to serialized JSON string.
			var jsonString = Json.Stringify(nodeData);

			// Store the save dictionary as a new line in the save file.
			saveFile.StoreLine(jsonString);
		}
    }

    public void LoadGame()
    {
		if (!FileAccess.FileExists(savePath))
		{
			return; // Error! We don't have a save to load.
		}

		// Load the file line by line and process that dictionary to restore the object
		// it represents.
		using var saveFile = FileAccess.Open(savePath, FileAccess.ModeFlags.Read);

		while (saveFile.GetPosition() < saveFile.GetLength())
		{
			var jsonString = saveFile.GetLine();

			// Creates the helper class to interact with JSON.
			var json = new Json();
			var parseResult = json.Parse(jsonString);
			if (parseResult != Error.Ok)
			{
				GD.Print($"JSON Parse Error: {json.GetErrorMessage()} in {jsonString} at line {json.GetErrorLine()}");
				continue;
			}

			// Get the data from the JSON object.
			var nodeDico = new Godot.Collections.Dictionary<string, Godot.Collections.Dictionary<string, Variant>>((Godot.Collections.Dictionary)json.Data);
			
			foreach (var (name, nodeData) in nodeDico)
			{
				GD.Print("aa");
				if(name == "EngineLogic")
				{
					GD.Print(name);
					Node node = GetNode("%EngineLogic");
					foreach (var (key, value) in nodeData)
					{
						GD.Print(key + " - " + value);
						node.Set(key, value);
						
					}
				}
			}
		}
    }

}
