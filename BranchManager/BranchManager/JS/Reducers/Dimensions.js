var Reducers = Reducers || {};

Reducers.dimensions = function (state, action) {
	if (typeof state === 'undefined') {
		var margins = {
			top: 20,
			bottom: 20,
			left: 20,
			right: 100
		};

		return {
			margins,
			width: $(window).width() - $('#left-panel').width() - margins.left - margins.right,
			height: $(window).height() - margins.top - margins.bottom
		};
	}

	switch (action.type) {
		case ActionType.RESIZE:
			return Object.assign({}, state, {
				width: $(window).width() - $('#left-panel').width() - state.margins.left - state.margins.right,
				height: $(window).height() - state.margins.top - state.margins.bottom
			});
	}

	return state;
}