BranchManager.Actions = {
	ActionType: {
		GET_BRANCH_INFO: 'GET_BRANCH_INFO',
		GET_CONFIG: 'GET_CONFIG',
		SET_ACTIVE_NODE: 'SET_ACTIVE_NODE',
		SET_COMMITS: 'SET_COMMITS',
		SET_ORIENTATION: 'SET_ORIENTATION',
		SET_PROJECT: 'SET_PROJECT'
	},
	getBranchInfoError: function (error) {
		BranchManager.Store.dispatch({
			type: BranchManager.Actions.ActionType.GET_BRANCH_INFO,
			success: false,
			error
		});
	},
	getBranchInfoSuccess: function (response) {
		BranchManager.Store.dispatch({
			type: BranchManager.Actions.ActionType.GET_BRANCH_INFO,
			success: true,
			response
		});
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
	setActiveNode: function(node, projectUrl) {
		BranchManager.Store.dispatch({
			type: BranchManager.Actions.ActionType.SET_ACTIVE_NODE,
			node: node,
			projectUrl: projectUrl
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