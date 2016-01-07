ReactDOM.render(
	React.createElement(
		ReactRedux.Provider,
		{ store: BranchManager.Store },
		React.createElement(BranchManager.Components.Main)),
	document.getElementById('app'));