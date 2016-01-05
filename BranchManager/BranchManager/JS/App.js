ReactDOM.render(
	React.createElement(
		ReactRedux.Provider,
		{ store: Store },
		React.createElement(Main)),
	document.body);