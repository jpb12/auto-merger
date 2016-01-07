var Reducers = Reducers || {};

Reducers.activeNode = function (state, action) {
	if (typeof state === 'undefined') {
		return null;
	} else {
		switch (action.type) {
			case ActionType.SET_ACTIVE_NODE:
				return action.node;
			case ActionType.SET_PROJECT:
				return null;
		}
	}

	return state;
}