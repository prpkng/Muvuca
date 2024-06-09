extends Node

class_name PlayerInput

@export var player: PlayerController

func _input(event: InputEvent):
    if event is InputEventMouseButton:
        if event.button_index != MOUSE_BUTTON_LEFT:
            return
        print('pressed')
        var dir = (event.position - player.position).normalized()
        print(dir)

        player.jump_pressed.emit(dir)
