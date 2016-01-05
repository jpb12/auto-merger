var Components = Components || {};

Components.Spinner = React.createClass({
	displayName: 'Spinner',
	render: function () {
		return (
			React.createElement(
				'span',
				{
					className: 'spinner fa fa-refresh fa-spin fa-3x',
				}));
	}
});