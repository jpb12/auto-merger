BranchManager.Components.Tree = React.createClass({
	displayName: 'Tree',
	render: function () {
		return (
			React.createElement(
				'svg',
				{
					width: this.props.width
				},
				this.props.links.map(link =>
					React.createElement(
						BranchManager.Components.Link,
						{
							key: link.target.name,
							link: link
						})),
				this.props.nodes.map(node =>
					React.createElement(
						BranchManager.Components.Node,
						{
							key: node.name,
							node: node
						}))));
	}
});