(function () {
	var diagonal = d3.svg.diagonal().projection(function (d) { return [d.y, d.x]; });
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
		React.createElement(Tree, { diagonal: diagonal, margins: margins }),
		document.getElementById('tree-container'));
})();