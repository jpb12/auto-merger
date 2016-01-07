BranchManager.Components.Orientation = React.createClass({
	displayName: 'Orientation',
	handleClick: function () {
		BranchManager.Actions.setOrientation(!this.props.horizontal);
	},
	render: function () {
		return (
			React.createElement(
				'div',
				{
					className: 'button',
					onClick: this.handleClick
				},
				React.createElement(
					'span',
					{
						className: 'fa ' + (this.props.horizontal ? 'fa-arrow-right' : 'fa-arrow-down')
					})));
	}
});

BranchManager.Components.Orientation = ReactRedux.connect(state => ({ horizontal: state.settings.horizontal }))(BranchManager.Components.Orientation);