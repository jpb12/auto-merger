BranchManager.Components.Main = React.createClass({
	displayName: 'Main',
	componentWillMount: function () {
		BranchManager.Actions.getConfig();
	},
	render: function () {
		return (
			React.createElement(
				'div',
				{
					id: 'container'
				},
				React.createElement(BranchManager.Components.LeftPanel),
				React.createElement(BranchManager.Components.TreeContainer),
				this.props.hasActiveNode ? React.createElement(BranchManager.Components.RightPanel) : undefined));
	}
})

BranchManager.Components.Main = ReactRedux.connect(state => ({ hasActiveNode: !!state.activeNode }))(BranchManager.Components.Main);