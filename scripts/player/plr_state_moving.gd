extends PlayerState

func enter(data: Array):
    if data[0] == null:
        printerr('ERR: The "moving" state must take at LEAST one parameter, which is the direction where the player wants to move at.')
        player.get_tree().paused = true
        return
    
    player.velocity = data[0] * player.SPEED

func process():
    pass