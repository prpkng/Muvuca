extends PlayerState

func enter(data: Array):
    if data[0] == null:
        printerr('ERR: The "idle" state must take at LEAST one parameter, which is the surface where the player is landing.')
        player.get_tree().paused = true
        return
    
    player.jump_pressed.connect(jump)

func jump(direction: Vector2):
    machine.switch('moving', [direction])

func exit():
    player.jump_pressed.disconnect(jump)

func process():
    pass