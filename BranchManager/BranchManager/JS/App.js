(function () {
	var margins = {
		top: 20,
		bottom: 20,
		left: 20,
		right: 100
	};
	
	ReactDOM.render(
		React.createElement(ProjectList),
		document.getElementById('left-panel'));

	ReactDOM.render(
		React.createElement(Tree, { margins: margins }),
		document.getElementById('tree-container'));
})();