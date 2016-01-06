var Components = Components || {};

Components.LeftPanel = React.createClass({
	displayName: 'LeftPanel',
	componentDidMount: function () {
		Store.dispatch(Actions.resize());
	},
	render: function () {
		return (
			React.createElement(
				'div',
				{
					id: 'left-panel'
				},
				React.createElement(Components.ProjectList),
				React.createElement(Components.Refresh),
				React.createElement(Components.Gradient)));
	}
})