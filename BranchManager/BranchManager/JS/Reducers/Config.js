var Reducers = Reducers || {};

Reducers.config = function (state, action) {
	if (typeof state === 'undefined') {
		return {
			data: []
		};
	}

	switch (action.type) {
		case ActionType.GET_CONFIG:
			if (action.success === undefined) {
				$.ajax({ url: "api/tree" }).done(data => {
					Store.dispatch(Actions.getConfigSuccess(data));
				});
				return Object.assign({}, state, {
					loading: true
				});;
			}
			if (action.success) {
				return Object.assign({}, state, {
					data: action.response,
					loading: false
				});
			}
			// TODO: handle failure
			return Object.assign({}, state, {
				loading: false
			});
			window.alert(action.error);
	}

	return state;
}