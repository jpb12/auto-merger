function getTreeData(margins) {
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

	var width = $(window).width() - margins.left - margins.right;
	var height = $(window).height() - margins.top - margins.bottom;

	var tree = d3.layout.tree().size([height, width]);

	var nodes = tree.nodes(data);
	var links = tree.links(nodes);

	return {
		nodes: nodes,
		links: links
	}
}

var Tree = React.createClass({
	displayName: 'Tree',
	getInitialState: function () {
		return getTreeData(this.props.margins)
	},
	render: function () {
		return (
			React.createElement(
				'svg',
				{},
				this.state.links.map(link => React.createElement(
					Link,
					{
						key: link.target.name,
						link: link,
						diagonal: this.props.diagonal,
						margins: this.props.margins
					})),
				this.state.nodes.map(node =>
					React.createElement(
						Node,
						{
							key: node.name,
							node: node,
							margins: this.props.margins
						}))));
	}
});