extends PlayerState

func enter(data: Array):
	assert(len(data) > 0, 'The "idle" state must take at LEAST one parameter, which is the surface where the player is landing')
	
	player.jump_pressed.connect(jump)

func jump(direction: Vector2):
	machine.switch('moving', [direction])

func exit():
	player.jump_pressed.disconnect(jump)

func process():
	pass
