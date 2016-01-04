(function () {
	ReactDOM.render(
		React.createElement(ProjectList),
		document.getElementById('left-panel'));

	ReactDOM.render(
		React.createElement(Tree),
		document.getElementById('tree-container'));
})();