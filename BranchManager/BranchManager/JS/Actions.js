var ActionType = {
	GET_CONFIG: 'GET_CONFIG',
	RESIZE: 'RESIZE',
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
	setProject: function (projectUrl) {
		return {
			type: ActionType.SET_PROJECT,
			projectUrl
		};
	},
};