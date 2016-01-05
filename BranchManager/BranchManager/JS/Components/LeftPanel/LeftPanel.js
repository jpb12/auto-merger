var Components = Components || {};

Components.LeftPanel = React.createClass({
	displayName: 'LeftPanel',
	render: function () {
		return (
			React.createElement(
				'div',
				{
					id: 'left-panel'
				},
				React.createElement(Components.ProjectList),
				React.createElement(Components.Refresh)));
	}
})