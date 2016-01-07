var ActionType = {
	GET_CONFIG: 'GET_CONFIG',
	RESIZE: 'RESIZE',
	SET_ACTIVE_NODE: 'SET_ACTIVE_NODE',
	SET_COMMITS: 'SET_COMMITS',
	SET_PROJECT: 'SET_PROJECT'
}

var Actions = {
	getConfig: function () {
		return {
			type: ActionType.GET_CONFIG
		};
	},
	getConfigError: function (error) {
		return {
			type: ActionType.GET_CONFIG,
			success: false,
			error
		};
	},
	getConfigSuccess: function (response) {
		return {
			type: ActionType.GET_CONFIG,
			success: true,
			response
		};
	},
	resize: function() {
		return {
			type: ActionType.RESIZE
		};
	},
	setActiveNode: function(node) {
		return {
			type: ActionType.SET_ACTIVE_NODE,
			node: node
		};
	},
	setCommits: function (commits) {
		return {
			type: ActionType.SET_COMMITS,
			commits
		};
	},
	setProject: function (projectUrl) {
		return {
			type: ActionType.SET_PROJECT,
			projectUrl
		};
	}
};