var Refresh = React.createClass({
	displayName: 'Refresh',
	handleClick: function () {
		Store.dispatch(Actions.getConfig());
	},
	render: function () {
		return (
			React.createElement(
				'input',
				{
					type: 'button',
					className: 'fa refresh',
					onClick: this.handleClick,
					value: 'Refresh'
				}));
	}
});