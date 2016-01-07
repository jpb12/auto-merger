﻿var Reducers = Reducers || {};

Reducers.config = function (state, action) {
	if (typeof state === 'undefined') {
		return {
			data: [],
			loading: true
		};
	}

	switch (action.type) {
		case ActionType.GET_CONFIG:
			if (action.success === undefined) {
				$.ajax({ url: "api/tree" }).done(data => {
					Store.dispatch(Actions.getConfigSuccess(data));
				}).fail((jqXHR, textStatus, errorThrown) => {
					Store.dispatch(Actions.getConfigError(errorThrown));
				})
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
	}

	return state;
}