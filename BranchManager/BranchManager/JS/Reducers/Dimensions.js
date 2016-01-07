var Reducers = Reducers || {};

Reducers.dimensions = function (state, action) {
	function getContentWidth() {
		return getFullWidth() - margins.left - margins.right;
	}

	function getFullWidth() {
		return $(window).width() - $('#left-panel:visible').width() - $('#right-panel:visible').width();
	}

	function getHeight() {
		return $(window).height() - margins.top - margins.bottom;
	}

	var margins = {
		top: 20,
		bottom: 20,
		left: 20,
		right: 100
	};

	if (typeof state === 'undefined') {
		return {
			margins,
			contentWidth: getContentWidth(),
			fullWidth: getFullWidth(),
			height: getHeight()
		};
	}

	switch (action.type) {
		case ActionType.RESIZE:
			return Object.assign({}, state, {
				contentWidth: getContentWidth(),
				fullWidth: getFullWidth(),
				height: getHeight()
			});
	}

	return state;
}