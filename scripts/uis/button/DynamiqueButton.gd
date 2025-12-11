@tool
extends DynamiqueText

#@export var : String
signal cliked(name : String)

func _on_gui_input(event: InputEvent) -> void:
	if event is InputEventMouseButton and event.button_index == MOUSE_BUTTON_LEFT and event.pressed:
		
		cliked.emit(self.name)

