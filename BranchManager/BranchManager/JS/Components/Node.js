var Node = React.createClass({
	displayName: 'Node',
	render: function () {
		return (
			React.createElement(
				'g',
				{
					transform: 'translate(' + (this.props.node.y + 50) + ', ' + (this.props.node.x + 50) + ')'
				},
				React.createElement(Circle),
				React.createElement(Text, { name: this.props.node.name })));
	}
});