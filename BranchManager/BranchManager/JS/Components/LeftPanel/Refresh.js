BranchManager.Components.Refresh = React.createClass({
	displayName: 'Refresh',
	handleClick: function () {
		BranchManager.Actions.getConfig();
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
						className: 'fa fa-refresh'
					})));
	}
});