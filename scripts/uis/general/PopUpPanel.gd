@tool
extends PanelContainer

class_name PopUpPanel

@export var have_stat_zone: bool = false:
	set(value):
		if stat_zone:
			have_stat_zone = value
			stat_zone.visible = value

@export_subgroup("Don't touch")
@export var description_title_text : DynamiqueText
@export var description_text : RichTextLabel
@export var stat_zone : Control
@export var current_stat_text : Label
@export var preview_stat_text : Label


# --- Title Dynamique Text properties ---
@export_category("Title Dynamique Text")
@export_multiline var text: String:
	get:
		return description_title_text.text
	set(value):
		if description_title_text:
			description_title_text.text = value


@export var uppercase: bool:
	get:
		return description_title_text.uppercase
	set(value):
		print("Bite")
		if description_title_text:
			description_title_text.uppercase = value

@export var horizontal_alignment: HorizontalAlignment:
	get:
		return description_title_text.horizontal_alignment
	set(value):
		if description_title_text:
			description_title_text.horizontal_alignment = value


@export_subgroup("Displayed Text")
@export_range(0, 1) var visible_ratio: float = 1:
	get:
		return description_title_text.visible_ratio
	set(value):
		if description_title_text:
			description_title_text.visible_ratio = value

@export_subgroup("Theme")
@export var title_theme: Theme:
	get:
		return description_title_text.theme
	set(value):
		if description_title_text:
			description_title_text.theme = value

@export var title_theme_type_variation: String = &"":
	get:
		return description_title_text.theme_type_variation
	set(value):
		if description_title_text:
			description_title_text.theme_type_variation = value




func _ready():
	pass


# --- Description Text fonction ---
func set_text(s: String):
	description_text.text = s

func set_font_color(color: Color):
	description_text.add_theme_color_override("font_color", color)


# --- Title Dynamique Text fonction ---
func set_title_text(s: String):
	description_title_text.text = s

func set_title_font_color(color: Color):
	description_title_text.add_theme_color_override("font_color", color)


# --- Current Stat Text fonction ---
func set_current_stat_text(s: String):
	current_stat_text.text = s

func set_current_stat_text_from_int(s: int):
	current_stat_text.text = str(s)

func set_current_stat_text_color(color: Color):
	current_stat_text.add_theme_color_override("font_color", color)


# --- Current Stat Text fonction ---
func set_preview_stat_text(s: String):
	preview_stat_text.text = s

func set_preview_stat_text_from_int(s: int):
	preview_stat_text.text = str(s)

func set_preview_stat_text_color(color: Color):
	preview_stat_text.add_theme_color_override("font_color", color)