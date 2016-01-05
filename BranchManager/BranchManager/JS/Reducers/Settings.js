var Reducers = Reducers || {};

Reducers.settings = function (state, action) {
	if (typeof state === 'undefined') {
		return JSON.parse(localStorage.getItem('BranchManager'));
	}

	switch (action.type) {
		case ActionType.SET_PROJECT:
			state = Object.assign({}, state, {
				projectUrl: action.projectUrl
			});
	}

	localStorage.setItem('BranchManager', JSON.stringify(state));

	return state;
}