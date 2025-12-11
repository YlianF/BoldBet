@tool
extends Node
class_name DynamiqueText

@export var autoResizeOnTextChange: bool = true;

@export_subgroup("Don't touch")
@export var texture_rect: TextureRect
@export var label: Label


# --- TextureRect properties ---
@export_category("TextureRect")
@export var expand_mode: TextureRect.ExpandMode:
	get:
		return texture_rect.expand_mode
	set(value):
		if texture_rect:
			texture_rect.expand_mode = value

@export var stretch_mode: TextureRect.StretchMode:
	get:
		return texture_rect.stretch_mode
	set(value):
		if texture_rect:
			texture_rect.stretch_mode = value


# --- Label properties ---
@export_category("Label")
@export var text: String:
	get:
		return label.text
	set(value):
		if label:
			label.text = value


@export var uppercase: bool:
	get:
		return label.uppercase
	set(value):
		if label:
			label.uppercase = value

@export var horizontal_alignment: HorizontalAlignment:
	get:
		return label.horizontal_alignment
	set(value):
		if label:
			label.horizontal_alignment = value


@export_subgroup("Displayed Text")
@export_range(0, 1) var visible_ratio: float = 1:
	get:
		return label.visible_ratio
	set(value):
		if label:
			label.visible_ratio = value

@export_subgroup("Theme")
@export var theme: Theme:
	get:
		return label.theme
	set(value):
		if label:
			label.theme = value

@export var theme_type_variation: String = &"":
	get:
		return label.theme_type_variation
	set(value):
		if label:
			label.theme_type_variation = value





func _ready():
	# label = %TextLabel  # Décommente si tu veux l'assigner via le nom du nœud
	pass


func set_text(s: String):
	label.text = s



func set_font_color(color: Color):
	label.add_theme_color_override("font_color", color)



func onTextDisplayResized():
	if autoResizeOnTextChange :
		setMinSizeX()



func onTextLabelResized():
	if autoResizeOnTextChange :
		setMinSizeX()


func setMinSizeX():
	self.custom_minimum_size = Vector2(label.size.x / 1.35 * texture_rect.size.y / 108, 0)