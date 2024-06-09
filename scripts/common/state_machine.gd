extends Node

class_name StateMachine

var current_state_id: String

var current_state: State

var states_database := {}

func _ready():
    for c in get_children():
        if !c is PlayerState:
            continue
        print(states_database)
        add(c, c.name)

func _process(_delta):
    if current_state != null:
        current_state.process()

func add(state: State, id: String):
    state.machine = self
    if id in states_database: printerr('State "%s" already present in state machine, overwriting it...' % id)
    states_database[id] = state

func switch(new_state_id: String, data: Array=[]):

    assert(new_state_id in states_database)

    if (current_state != null):
        current_state.exit()
    
    current_state = states_database[new_state_id]
    current_state_id = new_state_id

    current_state.enter(data)

func draw_gui(label: String):
    ImGui.Begin('%s - State Machine' % label)

    for id in states_database.keys():
        if id == current_state_id:
            ImGui.BulletText(id)
        else:
            ImGui.BulletText('%s - disabled' % id)

    ImGui.End()
