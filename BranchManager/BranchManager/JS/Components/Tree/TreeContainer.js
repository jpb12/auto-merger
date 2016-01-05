var TreeContainer = React.createClass({
	displayName: 'TreeContainer',
	render: function () {
		return (
			React.createElement(
				'div',
				{
					id: 'tree'
				},
				React.createElement(Tree)));
	}
})