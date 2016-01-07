BranchManager.Reducers.activeNode = function (state, action) {
	if (typeof state === 'undefined') {
		return null;
	} else {
		switch (action.type) {
			case BranchManager.Actions.ActionType.SET_ACTIVE_NODE:
				return action.node;
			case BranchManager.Actions.ActionType.SET_PROJECT:
				return null;
		}
	}

	return state;
}