ReactDOM.render(
	React.createElement(
		ReactRedux.Provider,
		{ store: Store },
		React.createElement(Components.Main)),
	document.getElementById('app'));