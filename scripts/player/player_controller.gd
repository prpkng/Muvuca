class_name PlayerController

extends CharacterBody2D

# In m/s (x100)
const SPEED := 5_00

@export var state_machine: StateMachine

signal jump_pressed(direction: Vector2)

func _ready():
    for v in state_machine.states_database.values():
        v.player = self
    state_machine.switch('idle', [''])

func _physics_process(delta):
    move_and_slide()

func _process(_delta):
    state_machine.draw_gui('Player')
