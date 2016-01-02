var Node = React.createClass({
	displayName: 'Node',
	render: function () {
		return (
			React.createElement(
				'g',
				{
					transform: 'translate(' + (this.props.node.y + this.props.margins.top) + ', ' + (this.props.node.x + this.props.margins.left) + ')'
				},
				React.createElement(Circle),
				React.createElement(Text, { name: this.props.node.name })));
	}
});