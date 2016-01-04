var Node = React.createClass({
	displayName: 'Node',
	render: function () {
		return (
			React.createElement(
				'g',
				{
					transform: 'translate(' + this.props.node.y + ', ' + this.props.node.x + ')'
				},
				React.createElement(Circle),
				React.createElement(Text, { name: this.props.node.name })));
	}
});