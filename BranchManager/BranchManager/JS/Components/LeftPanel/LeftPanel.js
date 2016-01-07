BranchManager.Components.LeftPanel = React.createClass({
	displayName: 'LeftPanel',
	componentDidMount: function () {
		BranchManager.Actions.resize();
	},
	render: function () {
		return (
			React.createElement(
				'div',
				{
					id: 'left-panel'
				},
				React.createElement('h3', {}, 'Projects'),
				React.createElement(BranchManager.Components.ProjectList),
				React.createElement(BranchManager.Components.Refresh),
				React.createElement(BranchManager.Components.Gradient),
				React.createElement(BranchManager.Components.Orientation)));
	}
})