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
	var links = tree.links(nodes);

	var diagonal = d3.svg.diagonal().projection(function (d) { return [d.y, d.x]; });

	ReactDOM.render(
		React.createElement(Tree, { nodes: nodes, links: links, diagonal: diagonal }),
		document.getElementById('tree-container'));
})();