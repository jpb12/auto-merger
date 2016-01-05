var Components = Components || {};

Components.Node = React.createClass({
	displayName: 'Node',
	render: function () {
		return (
			React.createElement(
				'g',
				{
					transform: 'translate(' + this.props.node.y + ', ' + this.props.node.x + ')'
				},
				React.createElement(Components.Circle),
				React.createElement(Components.Text, { name: this.props.node.name })));
	}
});