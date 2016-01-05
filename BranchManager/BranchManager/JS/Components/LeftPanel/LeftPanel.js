var LeftPanel = React.createClass({
	displayName: 'LeftPanel',
	render: function () {
		return (
			React.createElement(
				'div',
				{
					id: 'left-panel'
				},
				React.createElement(ProjectList),
				React.createElement(Refresh)));
	}
})