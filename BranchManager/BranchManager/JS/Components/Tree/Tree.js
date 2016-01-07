var Components = Components || {};

Components.Tree = React.createClass({
	displayName: 'Tree',
	render: function () {
		return (
			React.createElement(
				'svg',
				{
					width: this.props.width
				},
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

Components.Tree = ReactRedux.connect(state => ({ width: state.dimensions.fullWidth }))(Components.Tree);