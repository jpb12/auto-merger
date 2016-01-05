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
				React.createElement(Components.TreeContainer)));
	}
})