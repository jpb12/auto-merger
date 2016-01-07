var Components = Components || {};

Components.RightPanel = React.createClass({
	displayName: 'RightPanel',
	componentDidMount: function () {
		Store.dispatch(Actions.resize());
	},
	componentWillUnmount: function () {
		// TODO: Massive hack
		$(ReactDOM.findDOMNode(this)).width(0);
		Store.dispatch(Actions.resize());
	},
	render: function () {
		return (
			React.createElement(
				'div',
				{
					id: 'right-panel',
					className: this.props.activeNode ? '' : 'hidden'
				},
				React.createElement('h2', {}, this.props.activeNode.name)));
	}
})

Components.RightPanel = ReactRedux.connect(state => ({ activeNode: state.activeNode }))(Components.RightPanel);