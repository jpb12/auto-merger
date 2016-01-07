var Components = Components || {};

Components.Node = React.createClass({
	displayName: 'Node',
	handleClick: function() {
		Store.dispatch(Actions.setActiveNode(this.props.node));
	},
	render: function() {
		return (
			React.createElement(
				'g',
				{
					className: this.props.node.exists ? 'exists' : 'not-exists',
					transform: 'translate(' + this.props.node.y + ', ' + this.props.node.x + ')',
					onClick: this.handleClick
				},
				React.createElement(Components.Circle),
				React.createElement(Components.Text, { name: this.props.node.name })));
	}
});