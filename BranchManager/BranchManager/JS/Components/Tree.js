function getTreeData(margins, data) {
	var width = $(window).width() - margins.left - margins.right;
	var height = $(window).height() - margins.top - margins.bottom;

	var tree = d3.layout.tree()
		.size([height, width])
		.children(node => node.branches.map(item => item.child));

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
		var $this = this;
		$.ajax({url: "api/tree"}).done(function (data) {
			var treeData = getTreeData($this.props.margins, data[0].roots[0]);
			$this.setState({
				nodes: treeData.nodes,
				links: treeData.links
			});
		});
		return {
			nodes: [],
			links: []
		}
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