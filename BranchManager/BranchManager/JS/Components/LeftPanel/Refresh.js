var Components = Components || {};

Components.Refresh = React.createClass({
	displayName: 'Refresh',
	handleClick: function () {
		Store.dispatch(Actions.getConfig());
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