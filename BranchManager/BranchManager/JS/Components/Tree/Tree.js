var Components = Components || {};

Components.Tree = React.createClass({
	displayName: 'Tree',
	render: function () {
		return (
			React.createElement(
				'svg',
				{},
				this.props.links.map(link => React.createElement(
					Components.Link,
					{
						key: link.target.name,
						link: link
					})),
				this.props.nodes.map(node =>
					React.createElement(
						Components.Node,
						{
							key: node.name,
							node: node
						}))));
	}
});