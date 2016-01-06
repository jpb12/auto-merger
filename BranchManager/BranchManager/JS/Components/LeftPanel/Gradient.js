var Components = Components || {};

Components.Gradient = React.createClass({
	displayName: 'Gradient',
	render: function () {
		return (
			React.createElement(
				'div',
				{ id: 'gradient' },
				React.createElement('span', { id : 'gradient-label' }, '0'),
				React.createElement(Components.GradientInput),
				React.createElement('div', { id : 'gradient-display' })));
	}
});