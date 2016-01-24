BranchManager.Actions = {
	ActionType: {
		GET_CONFIG: 'GET_CONFIG',
		SET_ACTIVE_NODE: 'SET_ACTIVE_NODE',
		SET_COMMITS: 'SET_COMMITS',
		SET_ORIENTATION: 'SET_ORIENTATION',
		SET_PROJECT: 'SET_PROJECT'
	},
	getConfig: function () {
		BranchManager.Store.dispatch({
			type: BranchManager.Actions.ActionType.GET_CONFIG
		});
	},
	getConfigError: function (error) {
		BranchManager.Store.dispatch({
			type: BranchManager.Actions.ActionType.GET_CONFIG,
			success: false,
			error
		});
	},
	getConfigSuccess: function (response) {
		BranchManager.Store.dispatch({
			type: BranchManager.Actions.ActionType.GET_CONFIG,
			success: true,
			response
		});
	},
	setActiveNode: function(node) {
		BranchManager.Store.dispatch({
			type: BranchManager.Actions.ActionType.SET_ACTIVE_NODE,
			node: node
		});
	},
	setCommits: function (commits) {
		BranchManager.Store.dispatch({
			type: BranchManager.Actions.ActionType.SET_COMMITS,
			commits
		});
	},
	setOrientation: function (horizontal) {
		BranchManager.Store.dispatch({
			type: BranchManager.Actions.ActionType.SET_ORIENTATION,
			horizontal
		});
	},
	setProject: function (projectUrl) {
		BranchManager.Store.dispatch({
			type: BranchManager.Actions.ActionType.SET_PROJECT,
			projectUrl
		});
	}
};