BranchManager.Reducers.settings = function (state, action) {
	if (typeof state === 'undefined') {
		state = JSON.parse(localStorage.getItem('BranchManager')) || { commits: 30, horizontal: true };
	} else {
		switch (action.type) {
			case BranchManager.Actions.ActionType.SET_COMMITS:
				state = Object.assign({}, state, {
					commits: action.commits
				});
				break;
			case BranchManager.Actions.ActionType.SET_PROJECT:
				state = Object.assign({}, state, {
					projectUrl: action.projectUrl
				});
				break;
			case BranchManager.Actions.ActionType.SET_ORIENTATION:
				state = Object.assign({}, state, {
					horizontal: action.horizontal
				});
				break;
		}
	}

	localStorage.setItem('BranchManager', JSON.stringify(state));

	return state;
}