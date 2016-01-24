BranchManager.Reducers.activeNode = function (state, action) {
	if (typeof state === 'undefined') {
		return {};
	} else {
		switch (action.type) {
			case BranchManager.Actions.ActionType.GET_BRANCH_INFO:
				if (action.success) {
					if (action.response.name != state.node.name || action.response.projectUrl != state.node.projectUrl) {
						return state;
					}

					return Object.assign({}, state, {
						info: action.response,
						loading: false
					});
				}
				// TODO: handle failure
				return Object.assign({}, state, {
					loading: false
				});
			case BranchManager.Actions.ActionType.SET_ACTIVE_NODE:
				if (action.node) {
					var url = 'api/branch/' + action.node.name + '?projectUrl=' + action.projectUrl;

					$.ajax({ url: url }).done(data => {
						BranchManager.Actions.getBranchInfoSuccess(data);
					}).fail((jqXHR, textStatus, errorThrown) => {
						BranchManager.Actions.getBranchInfoError(errorThrown);
					})

					action.node.projectUrl = action.projectUrl;

					return Object.assign({}, state, {
						node: action.node,
						loading: true
					});
				}

				return {};
			case BranchManager.Actions.ActionType.SET_PROJECT:
				return {};
		}
	}

	return state;
}