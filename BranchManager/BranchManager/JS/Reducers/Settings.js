BranchManager.Reducers.settings = function (state, action) {
	if (typeof state === 'undefined') {
		state = JSON.parse(localStorage.getItem('BranchManager')) || { commits: 30 };
	} else {
		switch (action.type) {
			case BranchManager.Actions.ActionType.SET_PROJECT:
				state = Object.assign({}, state, {
					projectUrl: action.projectUrl
				});
				break;
			case BranchManager.Actions.ActionType.SET_COMMITS:
				state = Object.assign({}, state, {
					commits: action.commits
				});
				break;
		}
	}

	localStorage.setItem('BranchManager', JSON.stringify(state));

	return state;
}