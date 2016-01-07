BranchManager.Components.Gradient = React.createClass({
	displayName: 'Gradient',
	render: function () {
		return (
			React.createElement(
				'div',
				{ id: 'gradient' },
				React.createElement('span', { id : 'gradient-label' }, '0'),
				React.createElement(BranchManager.Components.GradientInput),
				React.createElement('div', { id : 'gradient-display' })));
	}
});