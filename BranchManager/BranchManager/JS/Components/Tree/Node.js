BranchManager.Components.Node = React.createClass({
	displayName: 'Node',
	handleClick: function() {
		BranchManager.Actions.setActiveNode(this.props.node);
	},
	getTransform: function() {
		return this.props.horizontal
			? 'translate(' + this.props.node.y + ', ' + this.props.node.x + ')'
			: 'translate(' + this.props.node.x + ', ' + this.props.node.y + ')';
	},
	render: function() {
		return (
			React.createElement(
				'g',
				{
					className: this.props.node.exists ? 'exists' : 'not-exists',
					transform: this.getTransform(),
					onClick: this.handleClick
				},
				React.createElement(BranchManager.Components.Circle),
				React.createElement(BranchManager.Components.Text, { name: this.props.node.name })));
	}
});

BranchManager.Components.Node = ReactRedux.connect(state => ({ horizontal: state.settings.horizontal }))(BranchManager.Components.Node);