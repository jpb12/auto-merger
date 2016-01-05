var Main = React.createClass({
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
				React.createElement(LeftPanel),
				React.createElement(TreeContainer)));
	}
})