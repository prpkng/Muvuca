extends PlayerState

func enter(data: Array):
    assert(len(data) > 0, 'The "idle" state must take at LEAST one parameter, which is the surface where the player is landing')

    player.velocity = data[0] * player.SPEED

func process():
    pass