extends CharacterBody2D

const SPEED := 100

@export var state_machine: StateMachine

func _ready():
    pass

func _process(_delta):
    state_machine.draw_gui('Player')
