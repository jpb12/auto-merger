var Main = React.createClass({
	displayName: 'Main',
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