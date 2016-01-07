var Components = Components || {};

Components.Main = React.createClass({
	displayName: 'Main',
	componentDidMount: function () {
		Store.dispatch(Actions.getConfig());
	},
	render: function () {
		return (
			React.createElement(
				'div',
				{
					id: 'container'
				},
				React.createElement(Components.LeftPanel),
				React.createElement(Components.TreeContainer),
				this.props.hasActiveNode ? React.createElement(Components.RightPanel) : undefined));
	}
})

Components.Main = ReactRedux.connect(state => ({ hasActiveNode: !!state.activeNode }))(Components.Main);