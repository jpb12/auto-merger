BranchManager.Components.Circle = React.createClass({
	displayName: 'Circle',
	render: function () {
		// Firefox bug: r is not read from css
		return (React.createElement('circle', { r: 4.5 }));
	}
})