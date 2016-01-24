BranchManager.Components.RightPanel = React.createClass({
	displayName: 'RightPanel',
	render: function () {
		return (
			React.createElement(
				'div',
				{
					id: 'right-panel',
					className: this.props.activeNode ? '' : 'hidden'
				},
				React.createElement('h2', {}, this.props.activeNode.name),
				React.createElement(BranchManager.Components.Close)));
	}
})

BranchManager.Components.RightPanel = ReactRedux.connect(state => ({ activeNode: state.activeNode }))(BranchManager.Components.RightPanel);