var Tree = React.createClass({
	displayName: 'Tree',
	render: function () {
		return (
			React.createElement(
				'svg',
				{
					height: '600px',
					width: '600px'
				},
				this.props.links.map(link => React.createElement(Link, { key: link.target.name, link: link, diagonal: this.props.diagonal })),
				this.props.nodes.map(node => React.createElement(Node, { key: node.name, node: node }))));
	}
});