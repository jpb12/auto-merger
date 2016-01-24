BranchManager.Components.RightPanel = React.createClass({
	displayName: 'RightPanel',
	render: function () {
		return (
			React.createElement(
				'div',
				{
					id: 'right-panel'
				},
				React.createElement('h2', {}, this.props.node.name),
				React.createElement(BranchManager.Components.Close),
				React.createElement(this.props.loading ? BranchManager.Components.Spinner : BranchManager.Components.Summary)));
	}
})

BranchManager.Components.RightPanel = ReactRedux.connect(
	state => (
		{
			loading: state.activeNode.loading,
			node: state.activeNode.node
		}))(BranchManager.Components.RightPanel);