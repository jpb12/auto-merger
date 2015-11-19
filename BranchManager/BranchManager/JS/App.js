(function () {
	var data = {
		name: "1.0",
		enabled: true,
		children: [
			{
				name: "trunk",
				enabled: true,
				children: [
					{
						name: "child-one",
						enabled: true,
						children: []
					},
					{
						name: "child-two",
						enabled: false,
						children: []
					}
				]
			}
		]
	};

	var tree = d3.layout.tree().size([500, 500]);

	var nodes = tree.nodes(data);

	console.log(nodes);

	ReactDOM.render(
		React.createElement(TreeRoot, { nodes: nodes }),
		document.getElementById('tree-container'));
})();