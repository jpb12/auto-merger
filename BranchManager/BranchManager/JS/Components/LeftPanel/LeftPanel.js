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
				React.createElement(BranchManager.Components.ProjectList),
				React.createElement(BranchManager.Components.Refresh),
				React.createElement(BranchManager.Components.Gradient)));
	}
})