var Tree = React.createClass({
	displayName: 'Tree',
	render: function () {
		return (
			React.createElement(
				'svg',
				{},
				this.props.links.map(link => React.createElement(
					Link,
					{
						key: link.target.name,
						link: link
					})),
				this.props.nodes.map(node =>
					React.createElement(
						Node,
						{
							key: node.name,
							node: node
						}))));
	}
});